/*
    You are given a 0-indexed array nums consisting of positive integers. You can choose two indices i and j, such that i != j, and the sum of digits of the number nums[i] is equal to that of nums[j].

    Return the maximum value of nums[i] + nums[j] that you can obtain over all possible indices i and j that satisfy the conditions. If no such pair of indices exists, return -1.

    Example 1:
        Input: nums = [18,43,36,13,7]
        Output: 54
        Explanation: The pairs (i, j) that satisfy the conditions are:
        - (0, 2), both numbers have a sum of digits equal to 9, and their sum is 18 + 36 = 54.
        - (1, 4), both numbers have a sum of digits equal to 7, and their sum is 43 + 7 = 50.
        So the maximum sum that we can obtain is 54.

    Example 2:
        Input: nums = [10,12,19,14]
        Output: -1
        Explanation: There are no two numbers that satisfy the conditions, so we return -1.

    Constraints:
        1 <= nums.length <= 10^5
        1 <= nums[i] <= 10^9
*/

public class Solution3 
{
    /*
        Analysis
            - Since each pair of satisfying nums[i], nums[j] have the same sum of digits, we can have more than two 
              numbers having the same sum of digits, and there we will choose lagrest two. 
              For example out of 18, 36, 45, 126, having all the sum = 9, we chose 45, 126.
            - Following this idea we group input numbers by their sum of digits, and we keep largest two in each 
              group. Next we check each group and find max of each group.

            - I optimized this in couple of stages:
                1. Solution: use dictionary with List, and sort list with OrderBy or Array.Sort to validate results.
                2. Keeping all the numbers in a group is not needed as we need largest two only. 
                   This way we reduce memory and remove sorting. I'm fine with this achivement, 16 ms, 75,3 MB.
                3. Out of curiosity, I used the hint from task description "What is the largest possible sum of 
                   digits a number can have?" and that gave me an idea to replace dictonary with max 90 x 2 matrix, 
                   because nums[i] <= 10^9, thus 9 digits with max sum of 81, and this have me 10ms, 72.18 MB solution so lets keep that.
    */
    public int MaximumSum(int[] nums) 
    {
        var store = new int[81, 2];

        int sumOfDigits, tempN, currentPair, maxPair = -1;

        foreach (var n in nums)
        {
            tempN = n;
            sumOfDigits = 0;

            while(tempN > 0)
            {
                sumOfDigits += tempN % 10;
                tempN /= 10;
            }

            sumOfDigits -= 1;

            if (store[sumOfDigits, 0] == 0)
            {
                store[sumOfDigits, 0] = n;
                continue;
            }

            if (store[sumOfDigits, 0] < n)
            {
                store[sumOfDigits, 1] = store[sumOfDigits, 0];
                store[sumOfDigits, 0] = n;
                continue;
            }
        
            if (store[sumOfDigits, 1] < n)
            {
                store[sumOfDigits, 1] = n;
            }
        }

        for(int i = 0; i < 81; i++)
        {
            if (store[i, 0] != 0 && store[i, 1] != 0)
            {
                currentPair = store[i, 0] + store[i, 1];

                if (currentPair > maxPair)
                {
                    maxPair = currentPair;
                }
            }
        }

        return maxPair;
    }

    public void Run()
    {
        Console.WriteLine(MaximumSum([18, 43, 36, 13, 7]));
        Console.WriteLine(MaximumSum([10, 12, 19, 14]));
    }
}