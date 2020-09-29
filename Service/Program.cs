using System;
using System.IO;
using Service.Services;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var textPath = @"../../../../" + args[0];
            var word = args[1];
            var wordAsBits = string.Empty;

            foreach (var character in word)
            {
                wordAsBits += Constants.RussianCharactersToCodes[character];
            }

            // Console.WriteLine(wordAsBits);
            // var text = File.ReadAllText(textPath, Encoding.UTF8);
            
            
            var targetFilePath = @"../../../../result.txt";

            // SpecialSymbolsService.Encode(textPath, targetFilePath, wordAsBits);
            // var key = SpecialSymbolsService.Decode(targetFilePath);
            
            // EndSpacesService.Encode(targetFilePath, textPath, wordAsBits);
            // var key = EndSpacesService.Decode(targetFilePath);
            
            ReplacementService.Encode(targetFilePath, textPath, wordAsBits, word);
            var key = ReplacementService.Decode(targetFilePath);

            Console.WriteLine(key);
            Console.WriteLine(key == wordAsBits);
            
            var sourceFileSize = new FileInfo(textPath).Length / 1024;
            Console.WriteLine(sourceFileSize + " kb");
            
            var targetFileSize = new FileInfo(targetFilePath).Length / 1024;
            Console.WriteLine(targetFileSize + " kb");
        }
    }
}