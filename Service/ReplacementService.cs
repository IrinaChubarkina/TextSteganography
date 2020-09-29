using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Service
{
    public class ReplacementService
    {
        public void Encode(string path, string textPath, string bits, string word)
        {
            var symbols = new Dictionary<char, char>()
            {
                {'a', 'а'},
                {'e', 'е'},
                {'o', 'о'},
                {'p', 'р'},
                {'c', 'с'},
                {'y', 'у'},
                {'x', 'х'}
            };
            var wordLength = word.Length;
            var help = Convert.ToString(wordLength, 2).PadLeft(5, '0');
            
            var text = File.ReadAllText(textPath, Encoding.UTF8);
            var lineCount = File.ReadLines(textPath).Count();

            // if (lineCount < bits.Length)
            // {
            //     throw new ArgumentException("мало строк!");
            // }
            var length = new FileInfo(textPath).Length;
            // Console.WriteLine(length);
            
            using (StreamWriter sw = File.CreateText(path))
            {
                var helpCounter = 0;
                var wordCounter = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    if (symbols.Keys.Contains(text[i]) && helpCounter < 5)
                    {
                        var result = help[helpCounter] == '1'
                            ? symbols[text[i]]
                            : text[i];
                        sw.Write(result);
                        helpCounter++;
                        continue;
                    }
                    
                    if (symbols.Keys.Contains(text[i]) && wordCounter < bits.Length)
                    {
                        var result = bits[wordCounter] == '1'
                            ? symbols[text[i]]
                            : text[i];
                        sw.Write(result);
                        wordCounter++;
                        continue;
                    }
                    sw.Write(text[i]);
                }
            }
        }

        public string Decode(string path)
        {
            var symbols = new Dictionary<char, char>()
            {
                {'a', 'а'},
                {'e', 'е'},
                {'o', 'о'},
                {'p', 'р'},
                {'c', 'с'},
                {'y', 'у'},
                {'x', 'х'}
            };
            
            var result = File.ReadAllText(path, Encoding.UTF8);
            
            string key = String.Empty;
            string help = String.Empty;
            var helpCounter = 0;
            var wordCounter = 0;
            var wordLeght = 0;

            for (var i = 0; i < result.Length; i++)
            {
                if (symbols.Keys.Contains(result[i]) || symbols.Values.Contains(result[i]))
                {
                    if (helpCounter < 5)
                    {
                        string pattern = @"[а-я]+";
                        var input = result[i];
                        Match m = Regex.Match(input.ToString(), pattern, RegexOptions.IgnoreCase);
                        var symbol = m.Success ? "1" : "0";
                        help += symbol;
                        helpCounter++;
                        wordLeght = Convert.ToInt32(help, 2) * 3;
                    }
                    else if (wordCounter < wordLeght && wordLeght!= 0)
                    {
                        string pattern = @"[а-я]+";
                        var input = result[i];
                        Match m = Regex.Match(input.ToString(), pattern, RegexOptions.IgnoreCase);
                        var symbol = m.Success ? "1" : "0";
                        key += symbol;
                        wordCounter++;
                    }
                }
            }

            return key;
        }
    }
}