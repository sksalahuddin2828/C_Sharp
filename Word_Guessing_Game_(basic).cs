using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WordGuessingGame
{
    public partial class Form1 : Form
    {
        private Random random;
        private List<string> randomWords;
        private string word;
        private string hint;
        private List<char> letters;
        private int score;
        private int level;
        
        private Timer timer;
        private PictureBox wordFrame;
        private PictureBox scoreButton;
        private PictureBox levelButton;
        private PictureBox heart;
        private PictureBox inputText;
        private PictureBox percent;
        private PictureBox icon;
        
        private Font wordGuessFont;
        private Font wordType;
        private Font scoreFont;
        private Font hintFont;
        private Font helpFont;
        private Font ratioFont;
        private Font msgFont;
        private Font answerFont;
        
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            random = new Random();
            randomWords = new List<string>();
            letters = new List<char>();
            score = 0;
            level = 1;
            
            timer = new Timer();
            timer.Interval = 1000 / 15;
            timer.Tick += Timer_Tick;
            
            wordFrame = new PictureBox();
            wordFrame.Image = Image.FromFile("backframe.jpg");
            // Set the properties of wordFrame
            
            scoreButton = new PictureBox();
            scoreButton.Image = Image.FromFile("score.png");
            // Set the properties of scoreButton
            
            levelButton = new PictureBox();
            levelButton.Image = Image.FromFile("level.png");
            // Set the properties of levelButton
            
            heart = new PictureBox();
            heart.Image = Image.FromFile("heart.jpg");
            // Set the properties of heart
            
            inputText = new PictureBox();
            inputText.Image = Image.FromFile("input.png");
            // Set the properties of inputText
            
            percent = new PictureBox();
            percent.Image = Image.FromFile("percents.png");
            // Set the properties of percent
            
            icon = new PictureBox();
            icon.Image = Image.FromFile("icon.png");
            // Set the properties of icon
            
            wordGuessFont = new Font("CopperPlate Gothic", 50);
            wordType = new Font("Calibri", 50);
            scoreFont = new Font("Comic Sans MS", 25);
            hintFont = new Font("Comic Sans MS", 25);
            helpFont = new Font("Comic Sans MS", 25);
            ratioFont = new Font("CopperPlate Gothic", 25);
            msgFont = new Font("CopperPlate Gothic", 25);
            answerFont = new Font("Juice ITC", 50);
            
            pictureBoxIcon.Image = icon.Image;
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Game logic
        }
        
        private void StartGame()
        {
            timer.Start();
        }
        
        private void StopGame()
        {
            timer.Stop();
        }
        
        private void ChooseWord()
        {
            // Choose a random word based on the level
        }
        
        private void DisplayWord()
        {
            // Display the word on the screen
        }
        
        private void DisplayHint()
        {
            // Display the hint on the screen
        }
        
        private void DisplayScore()
        {
            // Display the score on the screen
        }
        
        private void DisplayMessage()
        {
            // Display a message on the screen
        }
        
        private void GuessLetter(char letter)
        {
            // Handle the guessed letter
        }
        
        private void ShowAnswer()
        {
            // Display theCorrect Answer and the correct answer on the screen
        }

        private void pictureBoxIcon_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void pictureBoxAnswer_Click(object sender, EventArgs e)
        {
            ShowAnswer();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GuessLetter(e.KeyCode);
        }
    }
}
