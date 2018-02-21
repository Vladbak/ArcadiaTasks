using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingMp3Tags
{
   public class TagInformation
    {
        bool isPresent;
        bool isValid;
        string Song;
        string Artist;
        string Album;
        string Year;
        string Comment;
        string Genre;
        string TrackNumber;

        public TagInformation()
        {
            this.isPresent = true;
            this.isValid = true;
            this.Song = "";
            this.Artist = "";
            this.Album = "";
            this.Year = "";
            this.Comment = "";
            this.Genre = "";
            this.TrackNumber = "";


        }

        public bool IsPresent
        {
            get
            {
                return isPresent;
            }

            set
            {
                isPresent = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return isValid;
            }

            set
            {
                isValid = value;
            }
        }

        public string Song1
        {
            get
            {
                return Song;
            }

            set
            {
                Song = value;
            }
        }

        public string Artist1
        {
            get
            {
                return Artist;
            }

            set
            {
                Artist = value;
            }
        }

        public string Album1
        {
            get
            {
                return Album;
            }

            set
            {
                Album = value;
            }
        }

        public string Year1
        {
            get
            {
                return Year;
            }

            set
            {
                Year = value;
            }
        }

        public string Comment1
        {
            get
            {
                return Comment;
            }

            set
            {
                Comment = value;
            }
        }

        public string Genre1
        {
            get
            {
                return Genre;
            }

            set
            {
                Genre = value;
            }
        }

        public string TrackNumber1
        {
            get
            {
                return TrackNumber;
            }

            set
            {
                TrackNumber = value;
            }
        }

        /*
Field      | TAG | song | artist | album | year | comment | genre
Byte Width | 3   | 30   | 30     | 30    | 4    | 30      | 1
------------------------------------------------------------------
128 Bytes

     */
    }
}
