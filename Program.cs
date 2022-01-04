using System;
using System.IO;

namespace DylanClarkeCsvToJson
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath;
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Please specify a filepath as a parameter.");
                filePath =  Console.ReadLine();
            }
            else
            {
                filePath = args[0];
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Filepath was not specified.");
                return;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Couldn't find {filePath}.");
                return;
            }

            var fileContents = File.ReadAllText(filePath);
            Console.WriteLine(fileContents);
        }
    }
}
