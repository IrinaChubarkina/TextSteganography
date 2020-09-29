using System;
using System.IO;

namespace Service.Utils
{
    public static class ConsoleHelper
    {
        public static void ShowFileSize(string pathToFile)
        {
            var sourceFileSize = new FileInfo(pathToFile).Length / 1024;
            Console.WriteLine(sourceFileSize + " kb");
        }

        public static string GetTargetFilePath(string[] args)
        {
            return args.Length == 5 
                ? @"../../../TextFiles/" + args[4]
                : @"../../../TextFiles/result.txt";
        }

        public static string GetSourceFilePath(string[] args) => @"../../../TextFiles/" + args[2];
    }
}