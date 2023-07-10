using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DigitalAssistant
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sk. Salahuddin - Khulna");

            while (true)
            {
                Console.WriteLine("How may I assist you?");
                string userCommand = Console.ReadLine().ToLower();

                if (userCommand.Contains("exit") || userCommand.Contains("close") || userCommand.Contains("off") ||
                    userCommand.Contains("good bye") || userCommand.Contains("bye") || userCommand.Contains("ok bye") ||
                    userCommand.Contains("turn off") || userCommand.Contains("shutdown") || userCommand.Contains("no thanks") ||
                    userCommand.Contains("stop"))
                {
                    Console.WriteLine("Assistant Shut Down");
                    Console.WriteLine("Take care and see you later");
                    break;
                }

                Console.WriteLine("Please wait");
                PerformAction(userCommand);
            }
        }

        static void PerformAction(string userCommand)
        {
            if (userCommand.Contains("weather") || userCommand.Contains("weather report") || userCommand.Contains("today's weather report"))
            {
                Console.WriteLine("Sure, which city?");
                string city = Console.ReadLine().ToLower();
                OpenWeatherReport(city);
            }
            else if (userCommand.Contains("bangabandhu sheikh mujibur rahman") || userCommand.Contains("bangabandhu") ||
                     userCommand.Contains("sheikh mujibur rahman") || userCommand.Contains("father of the nation of bangladesh") ||
                     userCommand.Contains("father of the nation"))
            {
                FatherOfTheNationOfBangladesh();
            }
            else if (userCommand.Contains("ip address") || userCommand.Contains("internet protocol") || userCommand.Contains("ip"))
            {
                GetIPAddress();
            }
            else if (userCommand.Contains("opening wikipedia"))
            {
                OpenWikipedia();
            }
            else if (userCommand.Contains("search on wikipedia"))
            {
                SearchOnWikipedia();
            }
            else if (userCommand.Contains("search on youtube"))
            {
                SearchOnYouTube();
            }
            else if (userCommand.Contains("play on youtube") || userCommand.Contains("play from youtube") ||
                     userCommand.Contains("play a song from youtube") || userCommand.Contains("play a movie from youtube") ||
                     userCommand.Contains("play something on youtube") || userCommand.Contains("play something from youtube"))
            {
                PlayOnYouTube();
            }
            else if (userCommand.Contains("open youtube") || userCommand.Contains("opening youtube"))
            {
                OpenYouTube();
            }
            else if (userCommand.Contains("date and time"))
            {
                GetDateAndTime();
            }
            else if (userCommand.Contains("today's time") || userCommand.Contains("local time") || userCommand.Contains("time"))
            {
                GetLocalTime();
            }
            else if (userCommand.Contains("today's date") || userCommand.Contains("today date") || userCommand.Contains("date"))
            {
                GetTodayDate();
            }
            else if (userCommand.Contains("opening facebook"))
            {
                OpenFacebook();
            }
            else if (userCommand.Contains("facebook profile"))
            {
                OpenFacebookProfile();
            }
            else if (userCommand.Contains("facebook settings"))
            {
                OpenFacebookSettings();
            }
            else if (userCommand.Contains("facebook reels"))
            {
                OpenFacebookReel();
            }
            else if (userCommand.Contains("facebook messenger"))
            {
                OpenFacebookMessenger();
            }
            else if (userCommand.Contains("facebook video"))
            {
                OpenFacebookVideo();
            }
            else if (userCommand.Contains("facebook notification"))
            {
                OpenFacebookNotification();
            }
            else if (userCommand.Contains("opening google"))
            {
                OpenGoogleBrowser();
            }
            else if (userCommand.Contains("opening gmail"))
            {
                OpenGoogleMail();
            }
            else if (userCommand.Contains("google earth"))
            {
                OpenGoogleEarth();
            }
            else if (userCommand.Contains("google city") || userCommand.Contains("city on google") ||
                     userCommand.Contains("city from earth") || userCommand.Contains("city on earth"))
            {
                GoogleEarthSpecifyCity();
            }
            else if (userCommand.Contains("google map") || userCommand.Contains("map") || userCommand.Contains("map on google"))
            {
                OpenGoogleMap();
            }
            else if (userCommand.Contains("city from map") || userCommand.Contains("map city") ||
                     userCommand.Contains("city on map") || userCommand.Contains("google map city"))
            {
                GoogleMapSpecifyCity();
            }
            else if (userCommand.Contains("translate to english") || userCommand.Contains("translate into english") ||
                     userCommand.Contains("word translate") || userCommand.Contains("translate a sentence"))
            {
                GoogleTranslateSpecifyWord();
            }
            else if (userCommand.Contains("listen a joke") || userCommand.Contains("tell me a joke"))
            {
                TellJoke();
            }
            else if (userCommand.Contains("translation between two language") || userCommand.Contains("translated language") ||
                     userCommand.Contains("translate from google") || userCommand.Contains("language translation") ||
                     userCommand.Contains("language"))
            {
                TranslateLanguages();
            }
            else if (userCommand.Contains("what can you do") || userCommand.Contains("available commands") || userCommand.Contains("help"))
            {
                AvailableCommands();
            }
            else if (userCommand.Contains("who made you"))
            {
                WhoMadeYou();
            }
            else if (userCommand.Contains("what is your name") || userCommand.Contains("your name"))
            {
                WhatIsYourName();
            }
            else if (userCommand.Contains("ask"))
            {
                ComputationalGeographicalQuestion();
            }
            else
            {
                Console.WriteLine("Sorry, I didn't understand that command. Please try again!");
            }
        }

        static void OpenWeatherReport(string city)
        {
            Console.WriteLine("Opening the weather report for " + city + ".");
            try
            {
                WebClient client = new WebClient();
                string weatherUrl = "https://www.weather-forecast.com/locations/" + city + "/forecasts/latest";
                string weatherHtml = client.DownloadString(weatherUrl);

                Regex regex = new Regex(@"<span class=""phrase"">(.+?)<\/span>");
                MatchCollection matches = regex.Matches(weatherHtml);

                foreach (Match match in matches)
                {
                    Console.WriteLine(match.Groups[1].Value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to fetch weather information. Please try again later.");
            }
        }

        static void FatherOfTheNationOfBangladesh()
        {
            Console.WriteLine("The Father of the Nation Bangabandhu Sheikh Mujibur Rahman is the architect of independent Bangladesh.");
            Console.WriteLine("He played a vital role in the liberation movement and is revered as a national hero.");
        }

        static void GetIPAddress()
        {
            try
            {
                WebClient client = new WebClient();
                string ipAddress = client.DownloadString("https://checkip.amazonaws.com").Trim();
                Console.WriteLine("Your IP address is: " +ipAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to retrieve IP address. Please try again later.");
            }
        }

        static void OpenWikipedia()
        {
            try
            {
                Process.Start("https://www.wikipedia.org/");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Wikipedia. Please try again later.");
            }
        }

        static void SearchOnWikipedia()
        {
            Console.WriteLine("What would you like to search on Wikipedia?");
            string query = Console.ReadLine();
            try
            {
                Process.Start("https://en.wikipedia.org/wiki/" + HttpUtility.UrlEncode(query));
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to search on Wikipedia. Please try again later.");
            }
        }

        static void SearchOnYouTube()
        {
            Console.WriteLine("What would you like to search on YouTube?");
            string query = Console.ReadLine();
            try
            {
                Process.Start("https://www.youtube.com/results?search_query=" + HttpUtility.UrlEncode(query));
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to search on YouTube. Please try again later.");
            }
        }

        static void PlayOnYouTube()
        {
            Console.WriteLine("What would you like to play on YouTube?");
            string query = Console.ReadLine();
            try
            {
                Process.Start("https://www.youtube.com/results?search_query=" + HttpUtility.UrlEncode(query));
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to play on YouTube. Please try again later.");
            }
        }

        static void OpenYouTube()
        {
            try
            {
                Process.Start("https://www.youtube.com/");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open YouTube. Please try again later.");
            }
        }

        static void GetDateAndTime()
        {
            DateTime currentTime = DateTime.Now;
            string dateTime = currentTime.ToString("dd-MM-yyyy HH:mm:ss");
            Console.WriteLine("The current date and time is: " + dateTime);
        }

        static void GetLocalTime()
        {
            DateTime currentTime = DateTime.Now;
            string localTime = currentTime.ToString("HH:mm:ss");
            Console.WriteLine("The current local time is: " + localTime);
        }

        static void GetTodayDate()
        {
            DateTime currentDate = DateTime.Now;
            string todayDate = currentDate.ToString("dd-MM-yyyy");
            Console.WriteLine("Today's date is: " + todayDate);
        }

        static void OpenFacebook()
        {
            try
            {
                Process.Start("https://www.facebook.com/");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Facebook. Please try again later.");
            }
        }

        static void OpenFacebookProfile()
        {
            try
            {
                Process.Start("https://www.facebook.com/profile.php");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Facebook profile. Please try again later.");
            }
        }

        static void OpenFacebookSettings()
        {
            try
            {
                Process.Start("https://www.facebook.com/settings");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Facebook settings. Please try again later.");
            }
        }

        static void OpenFacebookReel()
        {
            try
            {
                Process.Start("https://www.facebook.com/reels");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Facebook Reels. Please try again later.");
            }
        }

        static void OpenFacebookMessenger()
        {
            try
            {
                Process.Start("https://www.messenger.com/");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Facebook Messenger. Please try again later.");
            }
        }

        static void OpenFacebookVideo()
        {
            try
            {
                Process.Start("https://www.facebook.com/videos");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Facebook videos. Please try again later.");
            }
        }

        static void OpenFacebookNotification()
        {
            try
            {
                Process.Start("https://www.facebook.com/notifications");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Facebook notifications. Please try again later.");
            }
        }

        static void OpenGoogleBrowser()
        {
            try
            {
                Process.Start("https://www.google.com/");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Google. Please try again later.");
            }
        }

        static void OpenGoogleMail()
        {
            try
            {
                Process.Start("https://mail.google.com/");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Google Mail. Please try again later.");
            }
        }

        static void OpenGoogleEarth()
        {
            try
            {
                Process.Start("https://www.google.com/earth/");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Google Earth. Please try again later.");
            }
        }

        static void GoogleEarthSpecifyCity()
        {
            Console.WriteLine("Which city do you want to see on Google Earth?");
            string city = Console.ReadLine().ToLower();
            try
            {
                Process.Start("https://www.google.com/earth/geo/" + HttpUtility.UrlEncode(city) + "/");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Google Earth for the specified city. Please try again later.");
            }
        }

        static void OpenGoogleMap()
        {
            try
            {
                Process.Start("https://www.google.com/maps/");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Google Map. Please try again later.");
            }
        }

        static void GoogleMapSpecifyCity()
        {
            Console.WriteLine("Which city do you want to see on Google Map?");
            string city = Console.ReadLine().ToLower();
            try
            {
                Process.Start("https://www.google.com/maps/place/" + HttpUtility.UrlEncode(city) + "/");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Google Map for the specified city. Please try again later.");
            }
        }

        static void GoogleTranslateSpecifyWord()
        {
            Console.WriteLine("Which word or sentence do you want to translate to English?");
            string text = Console.ReadLine();
            try
            {
                Process.Start("https://translate.google.com/#auto/en/" + HttpUtility.UrlEncode(text));
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Google Translate. Please try again later.");
            }
        }

        static void TellJoke()
        {
            try
            {
                WebClient client = new WebClient();
                string jokeHtml = client.DownloadString("https://www.jokes4us.com/miscellaneousjokes/cleanjokes.html");

                Regex regex = new Regex(@"<span class=""joke"">(.+?)<\/span>");
                MatchCollection matches = regex.Matches(jokeHtml);

                foreach (Match match in matches)
                {
                    Console.WriteLine(match.Groups[1].Value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to fetch a joke. Please try again later.");
            }
        }

        static void TranslateLanguages()
        {
            Console.WriteLine("Whichlanguage do you want to translate from?");
            string fromLanguage = Console.ReadLine().ToLower();
            Console.WriteLine("Which language do you want to translate to?");
            string toLanguage = Console.ReadLine().ToLower();
            Console.WriteLine("What do you want to translate?");
            string text = Console.ReadLine();
            try
            {
                Process.Start("https://translate.google.com/#" + fromLanguage + "/" + toLanguage + "/" + HttpUtility.UrlEncode(text));
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open Google Translate. Please try again later.");
            }
        }

        static void AvailableCommands()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("- Weather: Get the weather report of a city");
            Console.WriteLine("- Bangabandhu Sheikh Mujibur Rahman: Learn about the Father of the Nation of Bangladesh");
            Console.WriteLine("- IP address: Get your IP address");
            Console.WriteLine("- Opening Wikipedia: Open the Wikipedia homepage");
            Console.WriteLine("- Search on Wikipedia: Search for a specific topic on Wikipedia");
            Console.WriteLine("- Search on YouTube: Search for a video on YouTube");
            Console.WriteLine("- Play on YouTube: Search and play a video on YouTube");
            Console.WriteLine("- Open YouTube: Open the YouTube homepage");
            Console.WriteLine("- Date and Time: Get the current date and time");
            Console.WriteLine("- Today's Time: Get the current local time");
            Console.WriteLine("- Today's Date: Get today's date");
            Console.WriteLine("- Opening Facebook: Open the Facebook homepage");
            Console.WriteLine("- Facebook Profile: Open your Facebook profile");
            Console.WriteLine("- Facebook Settings: Open the Facebook settings page");
            Console.WriteLine("- Facebook Reels: Open Facebook Reels");
            Console.WriteLine("- Facebook Messenger: Open Facebook Messenger");
            Console.WriteLine("- Facebook Video: Open Facebook videos");
            Console.WriteLine("- Facebook Notification: Open Facebook notifications");
            Console.WriteLine("- Opening Google: Open the Google homepage");
            Console.WriteLine("- Opening Gmail: Open Google Mail");
            Console.WriteLine("- Google Earth: Open Google Earth");
            Console.WriteLine("- Google City: View a city on Google Earth");
            Console.WriteLine("- Google Map: Open Google Map");
            Console.WriteLine("- City from Map: View a city on Google Map");
            Console.WriteLine("- Translate to English: Translate a word or sentence to English");
            Console.WriteLine("- Listen a Joke: Listen to a joke");
            Console.WriteLine("- Translation between two languages: Translate text between two languages");
            Console.WriteLine("- What can you do: Get the list of available commands");
            Console.WriteLine("- Who made you: Know who made this digital assistant");
            Console.WriteLine("- What is your name: Know the name of this digital assistant");
            Console.WriteLine("- Ask: Ask a computational or geographical question");
        }

        static void WhoMadeYou()
        {
            Console.WriteLine("I was created by Sk. Salahuddin from Khulna, Bangladesh.");
        }

        static void WhatIsYourName()
        {
            Console.WriteLine("My name is Digital Assistant.");
        }

        static void ComputationalGeographicalQuestion()
        {
            Console.WriteLine("Please ask your question:");
            string question = Console.ReadLine();
            Console.WriteLine("Sorry, I'm unable to answer computational or geographical questions at the moment.");
        }
    }
}
