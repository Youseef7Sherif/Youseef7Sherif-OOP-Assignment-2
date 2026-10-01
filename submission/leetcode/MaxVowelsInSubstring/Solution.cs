namespace MaxVowelsInSubstring;

public class Solution
{
    public int MaxVowels(string s, int k)
    {
        int count = 0;
        int max = 0;

        // Count vowels in the first window
        for (int i = 0; i < k; i++)
        {
            if (IsVowel(s[i]))
                count++;
        }

        max = count;

        // Slide the window
        for (int i = k; i < s.Length; i++)
        {
            // Remove the character leaving the window
            if (IsVowel(s[i - k]))
                count--;

            // Add the character entering the window
            if (IsVowel(s[i]))
                count++;

            max = Math.Max(max, count);
        }

        return max;
    }

    private bool IsVowel(char c)
    {
        return c == 'a' ||
               c == 'e' ||
               c == 'i' ||
               c == 'o' ||
               c == 'u';
    }
}

