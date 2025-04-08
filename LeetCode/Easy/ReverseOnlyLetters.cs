/*

    Given a string s, reverse the string according to the following rules:

    All the characters that are not English letters remain in the same position.
    All the English letters (lowercase or uppercase) should be reversed.
    Return s after reversing it.

    Example 1:
        Input: s = "ab-cd"
        Output: "dc-ba"

    Example 2:
        Input: s = "a-bC-dEf-ghIj"
        Output: "j-Ih-gfE-dCba"

    Example 3:
        Input: s = "Test1ng-Leet=code-Q!"
        Output: "Qedo1ct-eeLg=ntse-T!"
    
    Constraints:
        1 <= s.length <= 100
        s consists of characters with ASCII values in the range [33, 122].
        s does not contain '\"' or '\\'.
*/

public class Solution2 {

    /*
        Analysis:
            - We do all in one pass.
            - Start from the end of s, in order to have reversing in place.
            - Move letters and leave non letters in the same position.
    */

    public string ReverseOnlyLetters(string s) 
    {
        var r = new char[s.Length];
        var ri = 0;
        int si = s.Length - 1;

        char c, d;
        
        while(si >= 0 && ri < s.Length)
        {
            c = s[si]; //current char
            d = s[ri]; //destination char

            if (d >= 'a' && d <= 'z' || d >= 'A' && d <= 'Z')
            {
                //destination index can accept current char

                if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z')
                {
                    //move current letter at destination
                    r[ri] = c;
                    ri++;
                }
                else
                {
                    //leave current simbol at the same place
                    r[si] = c;
                }

                si--;
            }
            else
            {
                r[ri] = d;
                ri++;
            }
        }

        return new string(r);
    }

    public void Run()
    {
        var result = ReverseOnlyLetters("-");
        Console.WriteLine(result);

        result = ReverseOnlyLetters("a-bC-dEf-ghIj");
        Console.WriteLine(result);

        result = ReverseOnlyLetters("Test1ng-Leet=code-Q!");
        Console.WriteLine(result);
    }
}