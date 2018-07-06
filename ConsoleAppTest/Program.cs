using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer(10000);
            timer.Elapsed += (sender, e) =>  RemoveFile();
            timer.Start();

            Console.ReadKey();
        }

        private static void RemoveFile()
        {
            var files = Directory.GetFiles("C:\\fiodor", "*.temp", SearchOption.AllDirectories);
            foreach (var f in files)
            {
                Console.WriteLine(f);

                var fileDate = File.GetCreationTime(f);

                if (DateTime.Now.Day == fileDate.Day && Path.GetExtension(f).Equals(".temp"))
                {
                    File.Delete(f);
                }
            }

            Console.ReadKey();
        }
    }
}
