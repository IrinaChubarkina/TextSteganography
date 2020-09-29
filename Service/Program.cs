﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.VisualBasic.CompilerServices;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            const string textPath = @"../../../../white.txt";
            const string word = "иришка";
            var symbols = new Dictionary<char, string>()
            {
                {'и', "000"},
                {'р', "001"},
                {'ш', "010"},
                {'к', "100"},
                {'а', "110"}
            };
            string bits = String.Empty;

            foreach (var c in word)
            {
                bits += symbols[c];
            }

            Console.WriteLine(bits);
            
            var text = File.ReadAllText(textPath, Encoding.UTF8);
            var lineCount = File.ReadLines(textPath).Count();

            if (lineCount < bits.Length)
            {
                throw new ArgumentException("мало строк!");
            }
            var length = new FileInfo(textPath).Length;
            Console.WriteLine(length);
            string path = @"../../../../result.txt";

            // var specialService = new SpecialSymbolsService();
            // specialService.Encode(path, textPath, bits);
            // var key = specialService.Decode(path);
            

            
            // var spacesService = new EndSpacesService();
            // spacesService.Encode(path, textPath, bits);
            // var key = spacesService.Decode(path);
            
            var replacementService = new ReplacementService();
            replacementService.Encode(path, textPath, bits, word);
            var key = replacementService.Decode(path);


            var result = File.ReadAllText(path, Encoding.UTF8);
            int ert = 0;

            // for (var i=1; i< 600; i++)
            for (var i=2; i< text.Length; i++)
            {
                string pattern = @"[а-я]+";
                var input = result[i];
                Match m = Regex.Match(input.ToString(), pattern, RegexOptions.IgnoreCase);
                if (m.Success) ert++;
                    
                    

                if (result[i-1] == ' ' && result[i] == '\r')
                {
                    // Console.WriteLine("dfb");
                }
                
                if (result[i] == ' ' && result[i] == '\r')
                {
                    // Console.WriteLine("dfb");
                }
                
                if (result[i] == '\n' || result[i] == '\r')
                {
                    // Console.Write((int)result[i]);
                    // Console.Write(',');
                    //
                    // continue;
                }
                
                // if (result[i-1] == ' ' && result[i] == '\n')
                // {
                //     Console.WriteLine("dfb");
                // }
                
                
               
                
                // Console.Write(result[i]);
                // Console.Write(',');
            }
            
            Console.Write(ert);
            Console.WriteLine(key);


        }
    }
}