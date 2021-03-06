﻿using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ParsingMp3Tags.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckBytesToStringEngString()
        {
            byte[] testByteArray = System.Text.Encoding.Default.GetBytes("Test");

            string expected = "Test";
            string actual = Program.BytesToString(testByteArray);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void CheckBytesToStringRusString()
        {
            byte[] testByteArray = System.Text.Encoding.Default.GetBytes("Тест");

            string expected = "Тест";
            string actual = Program.BytesToString(testByteArray);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void CheckCreateDictionary()
        {
            Hashtable ht = new Hashtable();

            string expected;
            
            using (StreamReader sr = new StreamReader(Constants.PathToDictionary))
            {
                expected=sr.ReadLine();
            }
            Program.CreateGenreDictionary(ht);
            string actual = ht[0].ToString();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void CheckIsThereTagMethodWithNoTagFile()
        {
            bool expected = false;
            bool actual;
            using (FileStream fs = new FileStream(Constants.PathToMp3WithoutTag, FileMode.Open))
            {
                actual = Program.isThereTag(fs);
            }
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckIsThereTagMethodWithTaggedFile()
        {
            bool expected = true;
            bool actual;
            using (FileStream fs = new FileStream(Constants.PathToMp3WithTag, FileMode.Open))
            {
                actual = Program.isThereTag(fs);
            }
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckMp3withTagAndNoTrackNumber()
        {
            TagInformation tag = new TagInformation();
            Hashtable ht = new Hashtable();
            Program.CreateGenreDictionary(ht);
            Program.ProcessTagFromMP3File(tag, Constants.PathToMp3WithTagButNoTrackNumber, ht);
            string expectedSong = "Let's Twist Again";
            string expectedArtist = "Elvis Presley";
            string expectedAlbum = "";
            string expectedYear = "";
            string expectedComment = "";
            string expectedGenre = "";
            string expectedTrackNumber = "";

            string actualSong = tag.Song1;
            string actualArtist = tag.Artist1;
            string actualAlbum = tag.Album1;
            string actualYear = tag.Year1;
            string actualComment = tag.Comment1;
            string actualGenre = tag.Genre1;
            string actualTrackNumber = tag.TrackNumber1;

            Assert.AreEqual(expectedSong, actualSong);
            Assert.AreEqual(expectedArtist, actualArtist);
            Assert.AreEqual(expectedAlbum, actualAlbum);
            Assert.AreEqual(expectedYear, actualYear);
            Assert.AreEqual(expectedComment, actualComment);
            Assert.AreEqual(expectedGenre, actualGenre);
            Assert.AreEqual(expectedTrackNumber, actualTrackNumber);
        }

        [TestMethod]
        public void CheckMp3withTag()
        {
            TagInformation tag = new TagInformation();
            Hashtable ht = new Hashtable();
            Program.CreateGenreDictionary(ht);
            Program.ProcessTagFromMP3File(tag, Constants.PathToMp3WithTag, ht);
            string expectedSong = "Spitfire (Edit)";
            string expectedArtist = "The Prodigy";
            string expectedAlbum = "Spitfire [Single]";
            string expectedYear = "2005";
            string expectedComment = "";
            string expectedGenre = "";
            string expectedTrackNumber = "1";

            string actualSong = tag.Song1;
            string actualArtist = tag.Artist1;
            string actualAlbum = tag.Album1;
            string actualYear = tag.Year1;
            string actualComment = tag.Comment1;
            string actualGenre = tag.Genre1;
            string actualTrackNumber = tag.TrackNumber1;

            Assert.AreEqual(expectedSong, actualSong);
            Assert.AreEqual(expectedArtist, actualArtist);
            Assert.AreEqual(expectedAlbum, actualAlbum);
            Assert.AreEqual(expectedYear, actualYear);
            Assert.AreEqual(expectedComment, actualComment);
            Assert.AreEqual(expectedGenre, actualGenre);
            Assert.AreEqual(expectedTrackNumber, actualTrackNumber);
        }






    }
}
