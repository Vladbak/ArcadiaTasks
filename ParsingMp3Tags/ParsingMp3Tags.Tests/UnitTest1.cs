using System.Collections;
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
            
            using (StreamReader sr = new StreamReader(@"../../../Dictionary.txt"))
            {
                expected=sr.ReadLine();
            }
            Program.CreateGenreDictionary(ht);
            string actual = ht[0].ToString();

            Assert.AreEqual(expected, actual);

        }

    }
}
