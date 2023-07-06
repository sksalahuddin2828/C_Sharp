using System;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.Globalization;

namespace AIAssistant
{
    class Program
    {
        static void Main(string[] args)
        {
            ClockTime();
            Speak("हैलो मेरा नाम शेख सलाहुद्दीन है, बताइये में आपकी क्या मदद कर सक्ती हूं");

            while (true)
            {
                Console.WriteLine("Listening...");
                string command = GetVoiceInput();

                if (command.ToLower().Contains("time") || command.ToLower().Contains("date"))
                {
                    DateTime now = DateTime.Now;
                    string date = now.ToString("yyyy-MM-dd");
                    string clock = now.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                    Speak($"आज की तिथि है {date} और वर्तमान समय है {clock}");
                }
                else if (command.ToLower().Contains("ip address"))
                {
                    string ipAddress = GetIpAddress();
                    Speak($"आपका इंटरनेट प्रोटोकोल (आई पी) है: {ipAddress}");
                }
                else if (command.ToLower().Contains("youtube"))
                {
                    OpenWebsite("https://www.youtube.com/");
                }
                else if (command.ToLower().Contains("google"))
                {
                    OpenWebsite("https://www.google.com/");
                }
                else if (command.ToLower().Contains("wikipedia"))
                {
                    OpenWebsite("https://wikipedia.org/");
                }
                else if (command.ToLower().Contains("who made you") || command.ToLower().Contains("creator"))
                {
                    string details = "Full Name: Sk. Salahuddin,\nAddress: House/Holding No. 173,\nVillage/Road: Maheshwar Pasha Kalibari,\nPost Office: KUET, \nPostal Code: 9203,\nPolice Station: Daulatpur,\nDistrict: Khulna, \nCountry: Bangladesh, \nMobile No. +8801767902828\nEmail: sksalahuddin2828@gmail.com";
                    Console.WriteLine(details);
                    Speak("नाम: शेख सलाहुद्दीन ने मुझे बनाया। वह जिला स्तर (12 जिला) परियोजना में आईटी/हाई-टेक पार्क की स्थापना का छात्र है। बांग्लादेश हाई-टेक पार्क अथॉरिटी। आईसीटी मंत्रालय, आईसीटी टावर, अगरगांव, ढाका। उनके कोर्स का नाम है: पायथन प्रोग्रामिंग का परिचय।```csharp
                    Console.WriteLine("उन्होंने इसे सिटी आईटी पार्क, खलीशपुर, खुलना से पूरा किया। उसका विवरण स्क्रीन पर छपा हुआ है। चलिए मैं आपको उसका गिठूब अकाउंट पर लेकर जा रहा हूं, ताकि आप उसको पहचान सके।");
                    OpenWebsite("https://github.com/sksalahuddin2828");
                }
                else if (command.ToLower().Contains("close") || command.ToLower().Contains("exit") || command.ToLower().Contains("good bye") || command.ToLower().Contains("ok bye") || command.ToLower().Contains("turn off") || command.ToLower().Contains("shut down"))
                {
                    Speak("अपना ध्यान रखना, बाद में मिलते हैं! धन्यवाद");
                    Console.WriteLine("Stopping Program");
                    break;
                }
                else
                {
                    Speak("में आपकी आवाज समझ नहीं पा रहा हूं। कृपा फिर से बोलिए");
                    Console.WriteLine("Unrecognized Voice, Say that again please.");
                }
            }
        }

        static void ClockTime()
        {
            int hour = DateTime.Now.Hour;

            if (hour >= 0 && hour < 12)
                Speak("शुभ प्रभात");
            else if (hour >= 12 && hour < 18)
                Speak("अभी दोपहर");
            else if (hour >= 18 && hour < 20)
                Speak("अभी शाम");
            else
                Speak("शुभ रात्रि");
        }

        static void Speak(string text)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Rate = 2;
            synthesizer.Speak(text);
        }

        static string GetVoiceInput()
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();

                process.StandardInput.WriteLine("chcp 65001");  // Set CMD encoding to UTF-8
                process.StandardInput.WriteLine("set /p=Speak: ");
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();

                string output = process.StandardOutput.ReadToEnd();
                return output.Trim();
            }
        }

        static string GetIpAddress()
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();

                process.StandardInput.WriteLine("chcp 65001");  // Set CMD encoding to UTF-8
                process.StandardInput.WriteLine("curl ifconfig.me");
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();

                string output = process.StandardOutput.ReadToEnd();
                return output.Trim();
            }
        }

        static void OpenWebsite(string url)
        {
            Process.Start(url);
        }
    }
}
