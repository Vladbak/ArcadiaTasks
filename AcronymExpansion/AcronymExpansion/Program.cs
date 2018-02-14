using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AcronymExpansion
{
    class Program
    {
        public static void Main(string[] args)
        {
           
            Console.WriteLine("Write string for replacing acronyms");
            string input = Console.ReadLine();
          string aaa =   Algorithm(input);
            Console.WriteLine(aaa);

        }

        public static string Algorithm(string input)
        {
            StringBuilder stringBuilderInput = new StringBuilder(input);
            string pattern = @"(\W+|^)(gl|hf)(\W+|$)";
            
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            Match match = regex.Match(stringBuilderInput.ToString());
            int matchCount = 0;
            

            while (match.Success)
            {
                Group group = match.Groups[2];
                string a = group.Value;
                stringBuilderInput.Replace(a, "hello", 
                    group.Index,
                   group.Length);
                match = regex.Match(stringBuilderInput.ToString());

            }
            return stringBuilderInput.ToString();
        }
    }
}
