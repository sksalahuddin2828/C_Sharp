using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace MusicPlayer
{
    public class Program
    {
        private static SoundPlayer player;

        public static void Main(string[] args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio Files|*.mp3;*.wav;*.ogg";
            openFileDialog.Title = "Select an audio file";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                PlayMusic(filePath);
            }

            while (true)
            {
                Console.WriteLine("1. Select a file");
                Console.WriteLine("2. Stop music");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    result = openFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        PlayMusic(filePath);
                    }
                }
                else if (choice == "2")
                {
                    StopMusic();
                }
                else if (choice == "3")
                {
                    break;
                }
            }

            player?.Dispose();
        }

        private static void PlayMusic(string filePath)
        {
            try
            {
                player = new SoundPlayer(filePath);
                player.Play();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private static void StopMusic()
        {
            player?.Stop();
        }
    }
}
