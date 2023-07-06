using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string inputString = "helloworldmylovelypython";
        FindMostOccurringCharacter(inputString);
    }

    static void FindMostOccurringCharacter(string str)
    {
        // Create a dictionary using LINQ's GroupBy method
        // which will have characters as keys and their
        // frequencies as values
        var charCount = str.GroupBy(c => c)
                           .ToDictionary(g => g.Key, g => g.Count());

        // Finding the maximum occurrence of a character
        // and getting its count
        int maxCount = charCount.Values.Max();

        // Finding the characters with the maximum count
        var mostOccurringChars = charCount.Where(kv => kv.Value == maxCount)
                                          .Select(kv => kv.Key);

        // Printing the most occurring character(s) and its count
        foreach (char ch in mostOccurringChars)
        {
            Console.WriteLine($"Character: {ch}, Count: {maxCount}");
        }
    }
}
