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
                TagInformation tagInformation = new TagInformation();
                Console.WriteLine(mp3FilePath);
                if (File.Exists(mp3FilePath))
                {
                    ProcessTagFromMP3File(tagInformation, mp3FilePath, GenreDictionary);

                    Console.WriteLine("Song title:\t{0}", tagInformation.Song1);
                    Console.WriteLine("Artist:\t\t{0}", tagInformation.Artist1);
                    Console.WriteLine("Album:\t\t{0}", tagInformation.Album1);
                    Console.WriteLine("Year:\t\t{0}", tagInformation.Year1);
                    Console.WriteLine("Comment:\t{0}", tagInformation.Comment1);
                    Console.WriteLine("Genre:\t\t{0}", tagInformation.Genre1);
                    Console.WriteLine("Track Number:\t{0}", tagInformation.TrackNumber1);
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
            using (StreamReader sr = new StreamReader(Constants.PathToDictionary))
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
        public static TagInformation ProcessTagFromMP3File(TagInformation tagInformation, string mp3FilePath,  Hashtable GenreDictionary)
        {
            byte[] bufer30 = new byte[30];
            byte[] bufer4 = new byte[4];
            byte[] bufer1 = new byte[1];
            using (FileStream fs = new FileStream(mp3FilePath, FileMode.Open))
            {
                if (!isThereTag(fs))
                {
                    tagInformation.IsPresent = false;
                    return tagInformation;
                }
               

                try
                {
                    tagInformation.IsPresent = true;
                    //we skip 3 first bytes of tag as we know it is "TAG", and we dont need it
                    fs.Seek(-125, SeekOrigin.End);
                    //reading Song name
                    fs.Read(bufer30, 0, 30);
                    tagInformation.Song1 = BytesToString(bufer30);
                    
                    //reading artist name
                    fs.Read(bufer30, 0, 30);
                    tagInformation.Artist1 = BytesToString(bufer30) ;

                    //reading album name
                    fs.Read(bufer30, 0, 30);
                    tagInformation.Album1 = BytesToString(bufer30);
                    
                    //reading year
                    fs.Read(bufer4, 0, 4);
                    tagInformation.Year1 = BytesToString(bufer4) ;

                    //reading comment and, if present, track number
                    fs.Read(bufer30, 0, 30);
                    if (bufer30[28] == 0 && bufer30[29] != 0)
                    {
                        tagInformation.Comment1 = BytesToString(bufer30).Substring(0, 28) ;
                        tagInformation.TrackNumber1 = bufer30[29].ToString();
                    }
                    else
                        tagInformation.Comment1 = BytesToString(bufer30) ;

                    //reading genre
                    fs.Read(bufer1, 0, 1);
                    
                    int GenreNumber;
                    if (Int32.TryParse(BytesToString(bufer1), out GenreNumber))
                        tagInformation.Genre1 = GenreDictionary[GenreNumber].ToString();

                    return tagInformation;

                }
                catch
                {
                    tagInformation.IsValid = false;
                    return tagInformation;
                }
            }

        }

        public static string BytesToString(byte[] bytes)
        {
            return System.Text.Encoding.Default.GetString(bytes).TrimEnd(new char[] {' ', '\0'});
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
