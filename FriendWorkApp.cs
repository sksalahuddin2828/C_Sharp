using System;
using System.Collections.Generic;

namespace FriendWorkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> friendOne = new List<int>();
            List<int> friendTwo = new List<int>();
            List<int> friendThree = new List<int>();

            for (int i = 15; i < 150; i++)
            {
                if (i % 3 == 0)
                {
                    friendOne.Add(i);
                }
                if (i % 5 == 0)
                {
                    friendTwo.Add(i);
                }
                if (i % 8 == 0)
                {
                    friendThree.Add(i);
                }
            }

            Console.WriteLine("1st friend I are working:");
            foreach (int number in friendOne)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("2nd Friend II are working:");
            foreach (int number in friendTwo)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("3rd Friend III are working:");
            foreach (int number in friendThree)
            {
                Console.WriteLine(number);
            }

            List<int> calculateTogetherWorks = new List<int>();

            foreach (int number in friendOne)
            {
                if (friendTwo.Contains(number) && friendThree.Contains(number))
                {
                    calculateTogetherWorks.Add(number);
                }
            }

            Console.WriteLine("All friends are working:");
            foreach (int number in calculateTogetherWorks)
            {
                Console.WriteLine(number);
            }
        }
    }
}
