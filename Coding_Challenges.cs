//----------------------------------------------------------------C# Coding Challenges---------------------------------------------------------

// C# Coding Challenges on Numbers 
// Write a program in C# to -

// 1. Convert decimal numbers to octal numbers.
// 2. Reverse an integer.
// 3. Print the Fibonacci series using the recursive method.
// 4. Return the Nth value from the Fibonacci sequence.
// 5. Find the average of numbers (with explanations).
// 6. Convert Celsius to Fahrenheit.

//----------------------------------------------------------------Solution of Problem:----------------------------------------------------------

// 1. Converting Decimal Numbers to Octal Numbers:

using System;

class Program {
    static void Main() {
        int decimal_number = 25;
        int[] octal_number = new int[100];
        int i = 0;

        while (decimal_number > 0) {
            octal_number[i] = decimal_number % 8;
            decimal_number = decimal_number / 8;
            i++;
        }

        Console.Write("Octal number: ");

        for (int j = i - 1; j >= 0; j--) {
            Console.Write(octal_number[j]);
        }

        return;
    }
}

//----------------------------------------------------------------------------------------------------------------------------------------------

// 2. Reversing an Integer:

//----------------------------------------------------------------------------------------------------------------------------------------------

// 3.
