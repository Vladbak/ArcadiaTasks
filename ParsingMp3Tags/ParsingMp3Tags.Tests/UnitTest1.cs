using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    
    }
}
