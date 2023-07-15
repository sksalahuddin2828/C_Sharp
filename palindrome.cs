using System;

class Program {
    static bool IsPalindrome(string s) {
        s = new string(s.Where(char.IsLetterOrDigit).Select(char.ToLower).ToArray());
        return s == new string(s.Reverse().ToArray());
    }

    static void Main() {
        Console.Write("Enter a string: ");
        string str = Console.ReadLine();
        if (IsPalindrome(str)) {
            Console.WriteLine($"'{str}' is a palindrome.");
        } else {
            Console.WriteLine($"'{str}' is not a palindrome.");
        }
    }
}

//------------------------------------------------------------------------------------

using System;
using System.Linq;

class Program {
    static bool IsPalindrome(string s) {
        s = new string(s.Where(char.IsLetterOrDigit).Select(char.ToLower).ToArray());
        return s == new string(s.Reverse().ToArray());
    }

    static void Main() {
        string str = "A man, a plan, a canal: Panama";
        Console.WriteLine(IsPalindrome(str) ? "True" : "False");  
    }
}

// Output: True
