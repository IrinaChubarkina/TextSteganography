using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            const string textPath = @"../../../../white.txt";
            
            var text = File.ReadAllText(textPath, Encoding.ASCII);
            var result = new char[text.Length];
            
            var length = new FileInfo(textPath).Length;
            Console.WriteLine(length);
            for (int i = 1; i < text.Length; i++)
            {
                // if (text[i] == '\n' && text[i-1] == '\r')
                // {
                //     result[i] = '\r';
                //     result[i-1] = '\n';
                //     continue;
                // }
                
                if (text[i] == '\r')
                {
                    // result[i] = '\r';r
                    // result[i-1] = '\n';
                    continue;
                }

                result[i] = text[i];
            }
            // foreach (var VARIABLE in result)
            // {
            //     if (VARIABLE == '\n' || VARIABLE == '\r')
            //     {
            //         Console.Write((int)VARIABLE);
            //         Console.Write(',');
            //
            //         continue;
            //     }
            //
            //     Console.Write(VARIABLE);
            //     Console.Write(',');
            //
            // }
            
            string path = @"../../../../result1.txt";

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(result);
            }
            
            var length1 = new FileInfo(path).Length;
            Console.WriteLine(length1);
        }
    }
}