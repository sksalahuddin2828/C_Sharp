using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WordGuessingGame
{
    public partial class Form1 : Form
    {
        private readonly Timer timer;
        private readonly Random random;
        private string word;
        private string hint;
        private int score;
        private int level;
        private readonly List<string> randomWords;
        private readonly List<char> letters;

        public Form1()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            random = new Random();
            randomWords = new List<string>();
            letters = new List<char>();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Game logic
            // Update the game state, check for game over conditions, etc.
        
            // Implement your game logic here
            // You can update the positions of game objects, handle collisions, perform scoring, etc.
        
            // Check for game over conditions
            if (gameOver)
            {
                // Perform necessary actions for game over, such as displaying the final score or ending the game
                StopGame();
                DisplayGameOver();
            }
        
            // Update the game display based on the current game state
            UpdateGameDisplay();
        }

        private void StartGame()
        {
            score = 0;
            level = 1;
            randomWords.Clear();
            letters.Clear();

            ChooseWord();

            timer.Start();
        }

        private void StopGame()
        {
            timer.Stop();
        }

        private void ChooseWord()
        {
            string[] wordList;

            switch (level)
            {
                case 1:
                    wordList = level_1_list;
                    break;
                case 2:
                    wordList = level_2_list;
                    break;
                case 3:
                    wordList = level_3_list;
                    break;
                case 4:
                    wordList = level_4_list;
                    break;
                case 5:
                    wordList = level_5_list;
                    break;
                default:
                    wordList = level_1_list;
                    break;
            }

            word = wordList[random.Next(wordList.Length)];
            hint = GetHintForWord(word);
        }

        private string GetHintForWord(string word)
        {
            if (lst1.Contains(word))
                return "Fruit";
            else if (lst2.Contains(word))
                return "Flower";
            else if (lst3.Contains(word))
                return "Animal";
            else if (lst4.Contains(word))
                return "Bird";
            else if (lst5.Contains(word))
                return "Insect";
            else if (lst6.Contains(word))
                return "Subject";
            else if (lst7.Contains(word))
                return "Sport";
            else if (lst8.Contains(word))
                return "Stationary";
            else if (lst9.Contains(word))
                return "Spice or Dry Fruit";
            else if (lst10.Contains(word))
                return "Food Item";
            else if (lst11.Contains(word))
                return "Vegetable";
            else if (lst12.Contains(word))
                return "Dairy Product";
            else if (lst13.Contains(word))
                return "Weapon or Tool";
            else if (lst14.Contains(word))
                return "Colour";
            else if (lst15.Contains(word))
                return "Body Accessory or a Gem";
            else if (lst16.Contains(word))
                return "A type of a building or a part of a building";
            else
                return "Hint";
        }

        private void DisplayWord()
        {
            string hiddenWord = "";
            foreach (char letter in word)
            {
                if (letter == ' ' || letters.Contains(letter))
                    hiddenWord += " " + letter + " ";
                else
                    hiddenWord += " * ";
            }

            var wordText = new FontRenderer(hiddenWord, wordGuessFont, Brushes.Yellow);
            gameDisplay.DrawFontRenderer(wordText, new Point(20, 200));
        }

        private void DisplayHint()
```csharp

// The triple backticks followed by csharp indicate that the code block should be treated as C# code. 
// It is a markdown syntax used to specify the programming language for syntax highlighting and proper formatting of the code snippet. 
// It helps to improve readability and distinguish the code from the surrounding text.

        {
            var hintText = new FontRenderer("Hint: " + hint, hintFont, Brushes.Blue);
            gameDisplay.DrawFontRenderer(hintText, new Point(50, 300));
        }

        private void DisplayScore()
        {
            var scoreText = new FontRenderer("Score: " + score, scoreFont, Brushes.Black);
            gameDisplay.DrawFontRenderer(scoreText, new Point(x, y));
        }

        private void DisplayMessage(string message)
        {
            var messageText = new FontRenderer(message, msgFont, Brushes.Black);
            gameDisplay.DrawFontRenderer(messageText, new Point(x, y));
        }

        private void GuessLetter(Keys key)
        {
            char letter = (char)key;
            if (char.IsLetter(letter))
            {
                letter = char.ToLower(letter);
                if (!letters.Contains(letter))
                {
                    letters.Add(letter);
                    if (word.Contains(letter))
                    {
                        score += 10;
                        DisplayMessage("Correct guess!");

                        bool wordGuessed = true;
                        foreach (char character in word)
                        {
                            if (!letters.Contains(character) && character != ' ')
                            {
                                wordGuessed = false;
                                break;
                            }
                        }

                        if (wordGuessed)
                        {
                            DisplayMessage("Congratulations! You guessed the word!");
                            level++;
                            randomWords.Clear();
                            letters.Clear();

                            if (level > 5)
                            {
                                DisplayMessage("Game Over! You completed all levels.");
                                StopGame();
                            }
                            else
                            {
                                ChooseWord();
                            }
                        }
                    }
                    else
                    {
                        // Incorrect guess
                        // Update chances left (if applicable)
                        // Display incorrect message
                        // Check if chances left are zero (game over condition)
                        // Update game state accordingly

                        // Update chances left (if applicable)
                        // Display incorrect message
                        // Check if chances left are zero (game over condition)
                        // Update game state accordingly

                        // Incorrect guess
                        DisplayMessage("Incorrect guess!");

                        if (chancesLeft == 0)
                        {
                            // No chances left, game over
                            DisplayMessage("Game Over! You ran out of chances.");
                            StopGame();
                        }
                    }

                    DisplayWord();
                    DisplayScore();
                }
            }
        }

        private void pictureBoxIcon_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void pictureBoxAnswer_Click(object sender, EventArgs e)
        {
            var answerText1 = new FontRenderer("Correct Answer", answerFont, Brushes.Black);
            gameDisplay.DrawFontRenderer(answerText1, new Point(x, y));

            var answerText2 = new FontRenderer(word, answerFont, Brushes.Black);
            gameDisplay.DrawFontRenderer(answerText2, new Point(x, y));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GuessLetter(e.KeyCode);
        }
    }
}
