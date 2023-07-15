using System;

class Program {
    static int KaprekarConstant(int n) {
        int count = 0;
        while (n != 6174) {
            count++;
            string digits = n.ToString().PadLeft(4, '0');
            char[] digitsArray = digits.ToCharArray();
            Array.Sort(digitsArray);
            int ascending = int.Parse(new string(digitsArray));
            Array.Reverse(digitsArray);
            int descending = int.Parse(new string(digitsArray));
            n = descending - ascending;
        }
        return count;
    }

    static void Main() {
        Console.Write("Enter a number: ");
        int num = int.Parse(Console.ReadLine());
        int steps = KaprekarConstant(num);
        Console.WriteLine($"Number of steps to reach Kaprekar constant: {steps}");
    }
}
