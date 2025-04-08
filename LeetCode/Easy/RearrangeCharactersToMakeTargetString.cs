/*

You are given two 0-indexed strings s and target. You can take some letters from s and rearrange them to form new strings.
Return the maximum number of copies of target that can be formed by taking letters from s and rearranging them.

Example 1:
    Input: s = "ilovecodingonleetcode", target = "code"
    Output: 2
    Explanation:
    For the first copy of "code", take the letters at indices 4, 5, 6, and 7.
    For the second copy of "code", take the letters at indices 17, 18, 19, and 20.
    The strings that are formed are "ecod" and "code" which can both be rearranged into "code".
    We can make at most two copies of "code", so we return 2.

Example 2:
    Input: s = "abcba", target = "abc"
    Output: 1
    Explanation:
    We can make one copy of "abc" by taking the letters at indices 0, 1, and 2.
    We can make at most one copy of "abc", so we return 1.
    Note that while there is an extra 'a' and 'b' at indices 3 and 4, we cannot reuse the letter 'c' at index 2, so we cannot make a second copy of "abc".
    
Example 3:
    Input: s = "abbaccaddaeea", target = "aaaaa"
    Output: 1
    Explanation:
    We can make one copy of "aaaaa" by taking the letters at indices 0, 3, 6, 9, and 12.
    We can make at most one copy of "aaaaa", so we return 1.
 
Constraints:
    1 <= s.length <= 100
    1 <= target.length <= 10
    s and target consist of lowercase English letters.
*/

public class Solution0 
{
    /*
        Analysis:
            - If target can be formed in n copies, then each character is present in s, n times.
            - There are two cases:
                # Target has each character reapeating only once in itself.
                # Target has some characters repeating multiple times in itself.
            - Solution is to count how many words each char can form and to find minimum of those counts.
            - For example, if we have s = "ilovecodingonleetcode", target = "code"
              we have:
              c o d e
              2 4 2 4  //counts of each char in source
              2 4 2 4  //final words count
            - For example if we have s = "abbaccaddaeea", target = "aaaaa"
              we have:
              aaaaa
              55555     //we process only first a and skip the rest
              11111     //final words count
    */

    private const int noCountSet = -1;

    public int RearrangeCharacters(string s, string target) //first idea
    {
        var finalWordsCount = noCountSet;

        for (int i = 0; i < target.Length; i++)
        {
            var targetChar = target[i];

            if (!s.Contains(targetChar))
            {
                //target char is not present at all in source string, so we can't build target string
                return 0;
            }

            var targetCharIndex = target.IndexOf(targetChar);
            if (targetCharIndex < i && targetCharIndex != -1)
            {
                //we are checking char which we already processed, so we skip it
                continue;
            }

            //char is being processed for the first time

            var charCountInTarget = target.Count(x => x == targetChar);
            var charCountInSource = s.Count(x => x == targetChar);
            var currentWordsCount = charCountInSource / charCountInTarget;
            
            if (finalWordsCount == noCountSet)
            {
                //we are setting finalWordsCount for the first time
                finalWordsCount = currentWordsCount;
                continue;
            }

            if (currentWordsCount < finalWordsCount)
            {
                finalWordsCount = currentWordsCount;
            }
        }

        return finalWordsCount;
    }


    //second idea
    //as I was not thinking much about the complexity in first idea, leet code remined me that it works in O(n^2)
    //got hint from comunitty that good old trick [c - 'a']++ can help and did the rest
    //no nested loops this time, producing O(n) complexity
    public int RearrangeCharactersV2(string s, string target) 
    {
        var finalWordsCount = noCountSet;

        byte[] charCountInSource = new byte[26];
        foreach (var c in s)
        {
            charCountInSource[c - 'a']++;
        }

        byte[] charCountInTarget = new byte[26];
        foreach (var c in target)
        {
            charCountInTarget[c - 'a']++;
        }

        for (byte i = 0; i < target.Length; i++)
        {
            var index = target[i] - 'a';
            var currentWordsCount = charCountInSource[index] / charCountInTarget[index];

            if (currentWordsCount == 0)
            {
                return 0;
            }

            //there is no need to check if char is already processed, because it will do no harm
            //same char will report the same number of words and that is fine
            if (finalWordsCount == noCountSet)
            {
                finalWordsCount = currentWordsCount;
                continue;
            }

            if (currentWordsCount < finalWordsCount)
            {
                finalWordsCount = currentWordsCount;
            }
        }

        return finalWordsCount;
    }

    public void Run()
    {
        Console.WriteLine(RearrangeCharactersV2("ilovecodingonleetcode", "code"));
        Console.WriteLine(RearrangeCharactersV2("abcba", "abc"));
        Console.WriteLine(RearrangeCharactersV2("abbaccaddaeea", "aaaaa"));
    }
}