using System;
using System.IO;
using Service.Services;
using Service.Utils;
using Service.Validators;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleValidator.ValidateArgsAndThrow(args);
            var sourceFilePath = ConsoleHelper.GetSourceFilePath(args);
            var method = args[1];

            if (args[0] == "encode")
            {
                var word = args[3];
                var wordAsBits = string.Empty;
                foreach (var character in word)
                {
                    wordAsBits += Constants.Constants.RussianCharactersToCodes[character];
                }
                var targetFilePath = ConsoleHelper.GetTargetFilePath(args);
                HandleEncodeMode(method, sourceFilePath, targetFilePath, wordAsBits, word);
            }
            else if (args[0] == "decode")
            {
                HandleDecodeMode(method, sourceFilePath);
            }
        }

        private static void HandleDecodeMode(string method, string sourceFilePath)
        {
            try
            {
                string word;
                switch (method)
                {
                    case "1":
                        word = SpecialSymbolsService.Decode(sourceFilePath);
                        break;
                    case "2":
                        word = EndSpacesService.Decode(sourceFilePath);
                        break;
                    case "3":
                        word = ReplacementService.Decode(sourceFilePath);
                        break;
                    default:
                        throw new ArgumentException("Wrong method");
                }

                Console.WriteLine("Word: " + word);
                ConsoleHelper.ShowFileSize(sourceFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }   
        }

        private static void HandleEncodeMode(string method, string sourceFilePath, string targetFilePath, string wordAsBits, string word)
        {
            try
            {
                switch (method)
                {
                    case "1":
                        SpecialSymbolsService.Encode(sourceFilePath, targetFilePath, wordAsBits);
                        break;
                    case "2":
                        EndSpacesService.Encode(targetFilePath, sourceFilePath, wordAsBits);
                        break;
                    case "3":
                        ReplacementService.Encode(targetFilePath, sourceFilePath, wordAsBits, word);
                        break;
                    default:
                        throw new ArgumentException("Wrong method");
                }
                Console.WriteLine("Success");
                ConsoleHelper.ShowFileSize(sourceFilePath);
                ConsoleHelper.ShowFileSize(targetFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }   
        }
    }
}