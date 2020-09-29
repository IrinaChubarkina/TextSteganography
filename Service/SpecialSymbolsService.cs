using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Service
{
    public class SpecialSymbolsService
    {
        public void Encode(string path, string textPath, string bits)
        {
            var text = File.ReadAllText(textPath, Encoding.UTF8);
            var lineCount = File.ReadLines(textPath).Count();

            if (lineCount < bits.Length)
            {
                throw new ArgumentException("мало строк!");
            }
            var length = new FileInfo(textPath).Length;
            // Console.WriteLine(length);
            
            using (StreamWriter sw = File.CreateText(path))
            {
                var counter = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == '\r')
                    {
                        if (counter == bits.Length)
                        {
                            sw.Write(" ");
                            counter++;
                            continue;
                        }
                    
                        if (counter < bits.Length && bits[counter] == '1')
                        {
                            counter++;
                            continue;
                        }
                        counter++;
                    }
                    sw.Write(text[i]);
                }
            }
            
        }

        public string Decode(string path)
        {
            var result = File.ReadAllText(path, Encoding.UTF8);
            string key = String.Empty;
            for (var i=1; i< result.Length; i++)
            {
                if (result[i-1] == ' ' && result[i] == '\n')
                {
                    Console.WriteLine("dfb");
                    break;
                }
                
                if (result[i-1] == '\r' && result[i] == '\n')
                {
                    key += "0";
                    continue;
                } 
                
                if (result[i-1] != '\r' && result[i] == '\n')
                {
                    key += "1";
                }
            }

            return key;
        }
    }
}