using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Chapter9
{
    class RegularExpressionPlayaround
    {
        static string myText = @"This comprehensive compendium provides a broad and thorough investigation of all
                                aspecs of programming with ASP.NET. Entiraly revised and updated for the fourth
                                release of .NET, this book will give you the information you need to master ASP.NET and build a dynamic
                                , successful, enterprise Web application.";
        public static void FindIon()
        {
            const string pattern = @"\ba\S*ion\b";
            // RegexOptions.ExplicitCapture 修改收集匹配的方式，方法是确保把显式指定的匹配作为有效的搜索结果
            MatchCollection myMatchs = Regex.Matches(myText, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            WriteMatches(myText,myMatchs);
        }

        public static void WriteMatches(string text,MatchCollection matchs)
        {
            Console.WriteLine("Original text was: \n\n" + text + "\n");
            Console.WriteLine("No. of matches: " + matchs.Count);

            foreach(Match nextMatch in matchs)
            {
                int index = nextMatch.Index;
                string result = nextMatch.ToString();
                int charsBefore = (index < 5) ? index : 5;
                int fromEnd = text.Length - index - result.Length;
                int charsAfter = (fromEnd < 5) ? fromEnd : 5;
                int charsToDisplay = charsBefore + charsAfter + result.Length;

                Console.WriteLine("Index: {0}, \tString: {1}, \t{2}", index, result, text.Substring(index - charsBefore, charsToDisplay));
            }
        }
    }
}
