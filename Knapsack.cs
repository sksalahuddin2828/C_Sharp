using System;

namespace KnapsackProblemApp
{
    class Program
    {
        static int Knapsack(int items, int[] weight, int[] value, int number)
        {
            int[,] knapsack = new int[number + 1, items + 1];

            // Build table for knapsack[,] in bottom up manner
            for (int i = 0; i <= number; i++)
            {
                for (int j = 0; j <= items; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        knapsack[i, j] = 0;
                    }
                    else if (weight[i - 1] <= j)
                    {
                        knapsack[i, j] = Math.Max(value[i - 1] + knapsack[i - 1, j - weight[i - 1]], knapsack[i - 1, j]);
                    }
                    else
                    {
                        knapsack[i, j] = knapsack[i - 1, j];
                    }
                }
            }

            return knapsack[number, items];
        }

        static void Main(string[] args)
        {
            int[] value = { 60, 100, 120 };
            int[] weight = { 10, 20, 30 };
            int items = 50;
            int number = value.Length;

            Console.WriteLine(Knapsack(items, weight, value, number));
        }
    }
}
