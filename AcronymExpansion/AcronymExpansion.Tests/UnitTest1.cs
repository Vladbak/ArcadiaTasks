using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcronymExpansion;

namespace AcronymExpansion.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SimpleLOLtoLaughOutLoud()
        {

            Hashtable Dictionary = new Hashtable();

            Dictionary.Add("lol", "laugh out loud");
            string input = "lol";

            string expected = "laugh out loud";
            string actual =Program.Algorithm(input, Dictionary);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SeveralAcronymsAsPartOfWords()
        {

            Hashtable Dictionary = new Hashtable();

            Dictionary.Add("lol", "laugh out loud");
            Dictionary.Add("dw", "don't worry");
            Dictionary.Add("hf", "have fun");
            string input = "lol dww ahfa hf";

            string expected = "laugh out loud dww ahfa have fun";
            string actual = Program.Algorithm(input, Dictionary);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AcronymsWithPunctuationMarks()
        {

            Hashtable Dictionary = new Hashtable();

            Dictionary.Add("lol", "laugh out loud");
            Dictionary.Add("dw", "don't worry");
            Dictionary.Add("hf", "have fun");
            string input = ".lol. !dw! ?hf? ,dw,";

            string expected = ".laugh out loud. !don't worry! ?have fun? ,don't worry,";
            string actual = Program.Algorithm(input, Dictionary);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VoidInput()
        {

            Hashtable Dictionary = new Hashtable();

            Dictionary.Add("lol", "laugh out loud");
            Dictionary.Add("dw", "don't worry");
            Dictionary.Add("hf", "have fun");
            string input = "";

            string expected = "";
            string actual = Program.Algorithm(input, Dictionary);
            Assert.AreEqual(expected, actual);
        }

    }
}
