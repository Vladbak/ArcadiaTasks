using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace AcronymExpansion
{
   public class Program
    {
        public static void Main(string[] args)
        {
            Hashtable Dictionary = new Hashtable();

            Dictionary.Add("lol", "laugh out loud");
            Dictionary.Add("dw", "don't worry");
            Dictionary.Add("hf", "have fun");
            Dictionary.Add("gg", "good game");
            Dictionary.Add("brb", "be right back");
            Dictionary.Add("g2g", "got to go");
            Dictionary.Add("wp", "well played");
            Dictionary.Add("gl", "good luck");
            Dictionary.Add("imo", "in my opinion");
        


            Console.WriteLine("Write string for replacing acronyms:");
            string input = Console.ReadLine();
            string resultString = Algorithm(input, Dictionary);
            Console.WriteLine("Your string without acronyms:");
            Console.WriteLine(resultString);

        }
        /// <summary>
        /// This method recieve string with some acronyms, look for them using regular expression and return 
        /// </summary>
        /// <param name="input">string with acronyms</param>
        /// <param name="Dictionary"> Collection of pairs "acronyms - full phrase" as "key-value"</param>
        /// <returns></returns>
        public static string Algorithm(string input, Hashtable Dictionary)
        {
            StringBuilder stringBuilderInput = new StringBuilder(input);
            /*this is our first part of pattern, we will fill it with our keys from Dictionary
                (\W+|^) - means that we can have 1 or more non-word symbols before possible acronym
                (so our acronym would not appear as part of another word)
                OR our acronym is in the beginning of the string (^)
            */
            string pattern = @"(\W+|^)(";
            foreach(var key in Dictionary.Keys)
            {
                pattern += key.ToString()+"|";
            }
            //we delete last added symbol "|", otherwise we'll have pattern like (gl|hf|), which will cause choosing every symbol in input string
            pattern = pattern.Remove(pattern.Length - 1, 1);

            /*
                same as in comment about first part of pattern, ($) - end of the string
            */
            pattern += @")(\W+|$)";
            
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            Match match = regex.Match(stringBuilderInput.ToString());
            string foundWord;

            /* here i look for first match in input string and replace key with value from Dictionary.
            i cant just replace all occurence of acronyms in one line of code, because
            some acronyms have only 1 white-space between them, and i can't create RegExp for that case, 
            so i have to combine using RegExp and that cycle, in which i look for first acronym, replace it, 
            and start looking for next.
            */
            while (match.Success)
            {
                //group[2] - part of match, which contains our acronym
                Group group = match.Groups[2];
                foundWord= group.Value;
                //we replacing our new acronym with full string from Hashtable with acronyms and their values
                stringBuilderInput.Replace( foundWord, 
                                            Dictionary[foundWord].ToString(), 
                                            group.Index,
                                            group.Length);

                /*looking for the next acronym, if there's no more, we left cycle */
                match = regex.Match(stringBuilderInput.ToString()); 

            }
            return stringBuilderInput.ToString();
        }
    }
}
