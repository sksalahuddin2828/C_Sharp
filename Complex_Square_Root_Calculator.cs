using System;

class SquareRoot {
    static void Main() {
        Console.Write("Enter a number: ");
        double num = double.Parse(Console.ReadLine());

        // Find the square root of the number
        double realPart = Math.Sqrt(Math.Abs(num));
        double imaginaryPart = Math.Sqrt(-num);

        // Display the result
        if (imaginaryPart == 0) {
            Console.WriteLine("The square root of {0} is {1:0.###}", num, realPart);
        } else {
            Console.WriteLine("The square root of {0} is {1:0.###}+{2:0.###}i", num, realPart, imaginaryPart);
        }
    }
}
