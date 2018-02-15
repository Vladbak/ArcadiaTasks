using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParsingMp3Tags
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string result="";
            foreach (var mp3FilePath in args)
            {

                if (File.Exists(mp3FilePath))
                {
                    ProcessTagFromMP3File(mp3FilePath);
                }
                else
                {
                    result = "Mp3 file not found";
                }

                Console.WriteLine(result);
            }

        }

        /*
Field      | TAG | song | artist | album | year | comment | genre
Byte Width | 3   | 30   | 30     | 30    | 4    | 30      | 1
------------------------------------------------------------------
128 Bytes
            
            */


        public static string ProcessTagFromMP3File(string mp3FilePath)
        {
            string result = "";
            byte[] bufer = new byte[30];
            using (FileStream fs = new FileStream(mp3FilePath, FileMode.Open))
            {
                if (!isThereTag(fs))
                    return "No tag found";



            }
            return "1";

        }

        public static string BytesToString(byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public static bool isThereTag(FileStream fs)
        {
            try
            {
                byte[] possibleTAG = new byte[3];
                fs.Seek(-128, SeekOrigin.End);
                fs.Read(possibleTAG, 0, 3);

                if (BytesToString(possibleTAG).Equals("TAG"))
                    return true;
                else
                    return false;

            }
            catch
            {
                return false;
            }
            
        }
    }
}
