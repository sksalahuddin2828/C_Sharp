using System;

class Program {
    static bool IsArmstrongNumber(int n) {
        int numDigits = n.ToString().Length;
        int sumOfPowers = 0;
        int temp = n;
        while (temp > 0) {
            int digit = temp % 10;
            sumOfPowers += (int)Math.Pow(digit, numDigits);
            temp /= 10;
        }
        return n == sumOfPowers;
    }

    static void Main() {
        Console.Write("Enter a number: ");
        int num = int.Parse(Console.ReadLine());
        if (IsArmstrongNumber(num)) {
            Console.WriteLine($"{num} is an Armstrong number.");
        } else {
            Console.WriteLine($"{num} is not an Armstrong number.");
        }
    }
}

//--------------------------------------------------------------------------------

using System;

class Program {
    static bool IsArmstrongNumber(int n) {
        int numDigits = n.ToString().Length;
        int sumOfPowers = 0;
        int temp = n;
        while (temp > 0) {
            int digit = temp % 10;
            sumOfPowers += (int)Math.Pow(digit, numDigits);
            temp /= 10;
        }
        return n == sumOfPowers;
    }

    static void Main() {
        Console.WriteLine(IsArmstrongNumber(153) ? "True" : "False");  
    }
}

// Output: True
