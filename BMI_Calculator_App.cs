using System;

namespace BMICalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your height in centimeters: ");
            double height = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter your weight in Kg: ");
            double weight = Convert.ToDouble(Console.ReadLine());

            height = height / 100;
            double bmi = weight / (height * height);

            Console.WriteLine($"Your Body-Mass index is: {bmi:F2}");

            if (bmi > 0)
            {
                if (bmi <= 16)
                {
                    Console.WriteLine("You are severely under-weight.");
                }
                else if (bmi <= 18.5)
                {
                    Console.WriteLine("You are under-weight.");
                }
                else if (bmi <= 25)
                {
                    Console.WriteLine("You are Healthy.");
                }
                else if (bmi <= 30)
                {
                    Console.WriteLine("You are overweight.");
                }
                else
                {
                    Console.WriteLine("You are severely overweight.");
                }
            }
            else
            {
                Console.WriteLine("Please enter valid details.");
            }
        }
    }
}
