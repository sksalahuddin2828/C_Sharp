using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        string[] choices = { "rock", "paper", "scissors" };

        Console.WriteLine("Welcome to the game!");
        Console.WriteLine("Enter:");
        Console.WriteLine("r for rock");
        Console.WriteLine("p for paper");
        Console.WriteLine("s for scissors");

        Console.Write("Enter your name: ");
        string playerName = Console.ReadLine();

        int userScoreTotal = 0;
        int computerScoreTotal = 0;
        int i = 1;

        while (i == 1)
        {
            string userChoice = UserInputChecker();
            while (userChoice == "")
            {
                userChoice = UserInputChecker();
            }

            string computerChoice = choices[random.Next(choices.Length)];
            Console.WriteLine("Computer chooses: " + computerChoice);

            int[] scores = GameLogic(computerChoice, userChoice, userScoreTotal, computerScoreTotal);
            i = scores[0];
            userScoreTotal = scores[1];
            computerScoreTotal = scores[2];

            if (i == 0)
            {
                Console.WriteLine("Scores for this match are as follows:");
                Console.WriteLine(playerName + "'s score: " + userScoreTotal);
                Console.WriteLine("Computer's score: " + computerScoreTotal);
                Console.WriteLine("Thank you for playing the game.");
                Console.WriteLine("Have a nice day!");
            }
            else if (i != 0 && i != 1)
            {
                Console.WriteLine("Invalid Input!");
                Console.Write("Please enter 1 to continue or 0 to leave the game: ");
                i = Convert.ToInt32(Console.ReadLine());
            }
        }
    }

    static string UserInputChecker()
    {
        Console.Write("Enter your choice: ");
        string userChoice = Console.ReadLine();
        if (userChoice == "r" || userChoice == "p" || userChoice == "s")
        {
            return userChoice;
        }
        else
        {
            Console.WriteLine("Wrong Input!!");
            return "";
        }
    }

    static int[] GameLogic(string computerChoice, string userChoice, int userScore, int computerScore)
    {
        int[] scores = new int[3];
        if (computerChoice == "rock" && userChoice == "p")
        {
            Console.WriteLine("Player Wins");
            Console.WriteLine("Enter 1 to continue and 0 to leave the game");
            userScore += 1;
            scores[0] = Convert.ToInt32(Console.ReadLine());
            scores[1] = userScore;
            scores[2] = computerScore;
            return scores;
        }
        else if (computerChoice == "rock" && userChoice == "s")
        {
            Console.WriteLine("Computer Wins");
            Console.WriteLine("Enter 1 to continue and 0 to leave the game");
            computerScore += 1;
            scores[0] = Convert.ToInt32(Console.ReadLine());
            scores[1] = userScore;
            scores[2] = computerScore;
            return scores;
        }
        else if (computerChoice == "paper" && userChoice == "r")
        {
            Console.WriteLine("Computer Wins");
            Console.WriteLine("Enter 1 to continue and 0 to leave the game");
            computerScore += 1;
            scores[0] = Convert.ToInt32(Console.ReadLine());
            scores[1] = userScore;
            scores[2] = computerScore;
            return scores;
        }
        else if (computerChoice == "paper" && userChoice == "s")
        {
            Console.WriteLine("Player Wins");
            Console.WriteLine("Enter 1 to continue and 0 to leave the game");
            userScore += 1;
            scores[0] = Convert.ToInt32(Console.ReadLine());
            scores[1] = userScore;
            scores[2] = computerScore;
            return scores;
        }
        else if (computerChoice == "scissors" && userChoice == "r")
        {
            Console.WriteLine("Player Wins");
            Console.WriteLine("Enter 1 to continue and 0 to leave the game");
            userScore += 1;
            scores[0] = Convert.ToInt32(Console.ReadLine());
            scores[1] = userScore;
            scores[2] = computerScore;
            return scores;
        }
        else if (computerChoice == "scissors" && userChoice == "p")
        {
            Console.WriteLine("Computer Wins");
            Console.WriteLine("Enter 1 to continue and 0 to leave the game");
            computerScore += 1;
            scores[0] = Convert.ToInt32(Console.ReadLine());
            scores[1] = userScore;
            scores[2] = computerScore;
            return scores;
        }
        else if (computerChoice == userChoice)
        {
            Console.WriteLine("Draw");
            Console.WriteLine("Enter 1 to continue and 0 to leave the game");
            userScore += 1;
            computerScore += 1;
            scores[0] = Convert.ToInt32(Console.ReadLine());
            scores[1] = userScore;
            scores[2] = computerScore;
            return scores;
        }

        return scores;
    }
}
