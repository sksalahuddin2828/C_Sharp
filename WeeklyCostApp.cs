using System;
using System.Collections.Generic;

namespace WeeklyCostApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> week = new List<int>();
            List<int> cost = new List<int>();
            int totalCost = 0;
            int outputWeek = 0;
            List<int> outputDay = new List<int>();

            for (int i = 1; i <= 7; i++)
            {
                Console.Write("Enter your working minutes: ");
                int minute = Convert.ToInt32(Console.ReadLine());
                week.Add(minute);
            }

            Console.Write("Send Money from Client: ");
            int sendMoney = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= 7; i++)
            {
                Console.Write("Enter your daily costs: ");
                int everydayCost = Convert.ToInt32(Console.ReadLine());
                cost.Add(everydayCost);
                totalCost += everydayCost;
            }

            foreach (int minutes in week)
            {
                if (minutes >= 4 * 60)
                {
                    int day = 4 * 60;
                    outputWeek += day;
                    outputDay.Add(day);
                }
                else if (minutes > 0)
                {
                    int day = minutes * (50 / 60);
                    outputWeek += day;
                    outputDay.Add(day);
                }
            }

            Console.WriteLine(outputWeek);
            Console.WriteLine(string.Join(", ", outputDay));
            Console.WriteLine(string.Join(", ", cost));
            int smtc = sendMoney - totalCost;
            Console.WriteLine(smtc);
            Console.WriteLine(smtc - outputWeek);
        }
    }
}
