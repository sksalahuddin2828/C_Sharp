using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace WifiProfileApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = "netsh";
            processInfo.Arguments = "wlan show profiles";
            processInfo.RedirectStandardOutput = true;
            processInfo.UseShellExecute = false;

            Process process = new Process();
            process.StartInfo = processInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            List<string> profiles = new List<string>(Regex.Matches(output, "All User Profile\\s+: (.*)\r\n"));

            List<Dictionary<string, string>> wifiList = new List<Dictionary<string, string>>();

            foreach (string profile in profiles)
            {
                ProcessStartInfo profileProcessInfo = new ProcessStartInfo();
                profileProcessInfo.FileName = "netsh";
                profileProcessInfo.Arguments = $"wlan show profile \"{profile}\" key=clear";
                profileProcessInfo.RedirectStandardOutput = true;
                profileProcessInfo.UseShellExecute = false;

                Process profileProcess = new Process();
                profileProcess.StartInfo = profileProcessInfo;
                profileProcess.Start();

                string profileOutput = profileProcess.StandardOutput.ReadToEnd();
                profileProcess.WaitForExit();

                if (Regex.IsMatch(profileOutput, "Security key           : Absent"))
                {
                    continue;
                }
                else
                {
                    Dictionary<string, string> wifiProfile = new Dictionary<string, string>();
                    wifiProfile["ssid"] = profile;

                    Match passwordMatch = Regex.Match(profileOutput, "Key Content            : (.*)\r\n");
                    if (passwordMatch.Success)
                    {
                        wifiProfile["password"] = passwordMatch.Groups[1].Value;
                    }
                    else
                    {
                        wifiProfile["password"] = null;
                    }

                    wifiList.Add(wifiProfile);
                }
            }

            foreach (Dictionary<string, string> wifiProfile in wifiList)
            {
                Console.WriteLine($"SSID: {wifiProfile["ssid"]}");
                Console.WriteLine($"Password: {wifiProfile["password"]}");
                Console.WriteLine();
            }
        }
    }
}
