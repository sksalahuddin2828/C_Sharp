using System;

class IncomeSort
{
    static void Main()
    {
        int[] incomeList = new int[10];

        Console.WriteLine("Enter the income of 10 people:");
        for (int person = 0; person < 10; person++)
        {
            Console.Write("Enter income: ");
            int income = Convert.ToInt32(Console.ReadLine());
            incomeList[person] = income;
        }

        for (int firstIndex = 0; firstIndex < 9; firstIndex++)
        {
            int swapCount = 0;
            int minIncome = incomeList[firstIndex];
            int minIndex = firstIndex;

            for (int secondIndex = firstIndex + 1; secondIndex < 10; secondIndex++)
            {
                if (minIncome > incomeList[secondIndex])
                {
                    minIncome = incomeList[secondIndex];
                    swapCount++;
                    minIndex = secondIndex;
                }
            }

            if (swapCount != 0)
            {
                int temp = incomeList[firstIndex];
                incomeList[firstIndex] = minIncome;
                incomeList[minIndex] = temp;
            }
        }

        Console.WriteLine("Sorted income list:");
        foreach (int income in incomeList)
        {
            Console.WriteLine(income);
        }
    }
}
