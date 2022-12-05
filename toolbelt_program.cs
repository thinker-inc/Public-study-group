using System;
using System.IO;
using System.Diagnostics;
using System.Text

namespace Testtoolbelt
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = "/C td wf workflow";
            Process.Start("cmd.exe", command);
            string[] words = new string[] { command };
            try
            {
                StreamWriter file = new StreamWriter(@"c:\home\sample01.txt", false, Encoding.UTF8);
                for (int i = 0; i < words.Length; i+=2)
                {
                    file.WriteLine(string.Format("{0},{1}", words[i], words[i + 1]));
                }
                file.Close();
                Console.WriteLine("done");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}