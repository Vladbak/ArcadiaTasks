using System;
using System.Collections;
using System.IO;

namespace ParsingMp3Tags
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Hashtable GenreDictionary = new Hashtable();
            CreateGenreDictionary(GenreDictionary);

            foreach (var mp3FilePath in args)
            {
                Console.WriteLine(mp3FilePath);
                if (File.Exists(mp3FilePath))
                {
                    Console.WriteLine(ProcessTagFromMP3File(mp3FilePath, GenreDictionary));
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Mp3 file not found\n");
                }
  
            }

        }

        /*
Field      | TAG | song | artist | album | year | comment | genre
Byte Width | 3   | 30   | 30     | 30    | 4    | 30      | 1
------------------------------------------------------------------
128 Bytes
            
            */

        public static void CreateGenreDictionary(Hashtable gd)
        {
            using (StreamReader sr = new StreamReader(@"../../Dictionary.txt"))
            {
                int i = 0;
                string genre;
                while (!sr.EndOfStream) 
                {
                    genre = sr.ReadLine();
                    gd.Add(i++, genre);
                }
            }
        }
        public static string ProcessTagFromMP3File(string mp3FilePath,  Hashtable GenreDictionary)
        {
            string result = "";
            byte[] bufer30 = new byte[30];
            byte[] bufer4 = new byte[4];
            byte[] bufer1 = new byte[1];
            using (FileStream fs = new FileStream(mp3FilePath, FileMode.Open))
            {
                if (!isThereTag(fs))
                    return "No tag found";

                try {
                    //we skip 3 first bytes of tag as we know it is "TAG", and we dont need it
                    fs.Seek(-125, SeekOrigin.End);

                    fs.Read(bufer30, 0, 30);
                    result += "Song title:\t" + BytesToString(bufer30) + "\n";

                    fs.Read(bufer30, 0, 30);
                    result += "Artist:\t\t" + BytesToString(bufer30) + "\n";

                    fs.Read(bufer30, 0, 30);
                    result += "Album:\t\t" + BytesToString(bufer30) + "\n";

                    fs.Read(bufer4, 0, 4);
                    result += "Year:\t\t" + BytesToString(bufer4) + "\n";
        //            133212

                    fs.Read(bufer30, 0, 30);
                    if (bufer30[28] == 0 && bufer30[29] != 0)
                    {
                        result += "Comment:\t" + BytesToString(bufer30).Substring(0, 28) + "\n";
                        result += "Track Number:\t" + bufer30[29] + "\n";
                    }else
                        result += "Comment:\t" + BytesToString(bufer30) + "\n";

                    fs.Read(bufer1, 0, 1);
                    result += "Genre:\t\t";
                    int GenreNumber;
                    if (Int32.TryParse(BytesToString(bufer1), out GenreNumber))
                        result += GenreDictionary[GenreNumber] + "\n";
                    else
                        result += "\n";

                    return result;

                }
                catch
                {
                    return "Tag is incorrect";
                }
            }

        }

        public static string BytesToString(byte[] bytes)
        {
            return System.Text.Encoding.Default.GetString(bytes);
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
