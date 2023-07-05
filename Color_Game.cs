using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ColorGame
{
    class Program
    {
        static readonly List<string> Colors = new List<string> { "Red", "Orange", "White", "Black", "Green", "Blue", "Brown", "Purple", "Cyan", "Yellow", "Pink", "Magenta" };

        static int score = 0;
        static string displayedWordColor = string.Empty;
        static bool gameRunning = false;

        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            while (true)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Spacebar)
                {
                    StartGame();
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.Write("Enter the color: ");
                    string userInput = Console.ReadLine();
                    CheckWord(userInput);
                }
            }
        }

        static void StartGame()
        {
            if (!gameRunning)
            {
                gameRunning = true;
                Console.Clear();
                PrintGameDescription();
                PrintScore();
                PrintTimeLeft(60);
                displayedWordColor = Colors[new Random().Next(Colors.Count)];
            }
        }

        static void StopGame()
        {
            gameRunning = false;
            Console.Clear();
            Console.WriteLine("Game Over!");
        }

        static void NextWord()
        {
            if (gameRunning)
            {
                string displayedWordText = Colors[new Random().Next(Colors.Count)];
                Console.SetCursorPosition(0, 5);
                Console.WriteLine(displayedWordText);
                Console.SetCursorPosition(0, 6);
                Console.Write("Enter the color: ");
                displayedWordColor = Colors[new Random().Next(Colors.Count)];
            }
        }

        static void CheckWord(string userInput)
        {
            if (gameRunning)
            {
                userInput = userInput.ToLower();
                if (userInput == displayedWordColor.ToLower())
                {
                    score++;
                    PrintScore();
                }
                NextWord();
            }
        }

        static void PrintScore()
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Your Score: " + score);
        }

        static void PrintTimeLeft(int secondsLeft)
        {
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("Game Ends in: " + secondsLeft + "s");
        }

        static void PrintGameDescription()
        {
            Console.WriteLine("Game Description: Enter the color of the words displayed below.");
            Console.WriteLine("And keep in mind not to enter the word text itself");
        }
    }
}
