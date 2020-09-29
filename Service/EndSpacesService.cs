using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Service
{
    public class EndSpacesService
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
                for (int i = 0; i < text.Length - 2; i++)
                {
                    if (text[i] == ' ' && text[i+1] == '\r' && text[i+2] == '\n') //remove all end spaces
                    {
                        counter++;
                        continue;
                    }
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
                            sw.Write(" ");
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
            for (var i = 1; i < result.Length; i++)
            {
                if (result[i-1] == ' ' && result[i] == '\n')
                {
                    Console.WriteLine("dfb");
                    break;
                }
                
                if (result[i-1] == ' ' && result[i] == '\r')
                {
                    key += "1";
                    continue;
                } 
                
                if (result[i-1] != ' ' && result[i] == '\r')
                {
                    key += "0";
                }
            }

            return key;
        }
    }
}