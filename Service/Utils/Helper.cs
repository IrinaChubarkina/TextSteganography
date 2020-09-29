using System.Collections.Generic;
using System.Linq;

namespace Service.Utils
{
    public static class Helper
    {
        public static string DecodeWord(Dictionary<char, string> collection, string word)
        {
            var chars = Enumerable.Range(0, word.Length / Constants.CodeWidth)
                .Select(i => word.Substring(i * Constants.CodeWidth, Constants.CodeWidth))
                .Select(code => collection.FirstOrDefault(pair => pair.Value == code).Key);
            return string.Concat(chars);
        }
    }
}