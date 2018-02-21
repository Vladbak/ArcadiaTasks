using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Word_Search_Solver;


namespace UnitTests
{
    [TestClass]
    public class WordSearchSolverTests
    {
        [TestMethod]
        public void FindWordInMatrix()
        {
            char[,] Matrix = {      { 'a','b','c','d','n'},
                                    { 'f','c','a','t','o'},
                                    { 't','b','c','d','m'},
                                    { 'h','o','u','s','e'},
                                    { 'a','s','l','s','l'}, };

            Solution.Delta delta = new Solution.Delta(1, 0);
            Solution.Position position = new Solution.Position(0, 3, 4, 4, 0, 0);
            string targetWord = "house";
            bool isFirstLetter = true;

            Assert.IsTrue(
                Solution.findWordInDirection(Matrix, delta, position, targetWord, isFirstLetter));

        }

        [TestMethod]
        public void NotFindWordInMatrix()
        {
            char[,] Matrix = {      { 'a','b','c','d','n'},
                                    { 'f','c','a','t','o'},
                                    { 't','b','c','d','m'},
                                    { 'h','o','u','s','e'},
                                    { 'a','s','l','s','l'}, };

            Solution.Delta delta = new Solution.Delta(1, 0);
            Solution.Position position = new Solution.Position(0, 3, 4, 4, 0, 0);
            string targetWord = "house";
            bool isFirstLetter = false;

            Assert.IsFalse(
                Solution.findWordInDirection(Matrix, delta, position, targetWord, isFirstLetter));

        }

        [TestMethod]
        public void ReachEndOfMatrixWhileSearchingWord()
        {
            char[,] Matrix = {      { 'a','b','c','d','n'},
                                    { 'f','c','a','t','o'},
                                    { 't','b','c','d','m'},
                                    { 'h','o','u','s','e'},
                                    { 'a','s','l','s','l'}, };

            Solution.Delta delta = new Solution.Delta(1, 0);
            Solution.Position position = new Solution.Position(0, 3, 4, 4, 0, 0);
            string targetWord = "houseaaa";
            bool isFirstLetter = true;

            Assert.IsFalse(
                Solution.findWordInDirection(Matrix, delta, position, targetWord, isFirstLetter));

        }

        [TestMethod]
        public void InsertWordInMatrix()
        {
            char[,] actualResultMatrix = {      { '+','+','+','+','+'},
                                                { '+','+','+','+','+'}
                                 };
            char[,] expectedResultMatrix = {      { '+','+','+','+','+'},
                                                  { 'h','o','u','s','e'}
                                 };

            Solution.Delta delta = new Solution.Delta(1, 0);
            Solution.Position position = new Solution.Position(0, 1, 4, 1, 0, 0);
            string targetWord = "house";
            bool isFirstLetter = true;

            Solution.insertWordInResultMatrix(ref actualResultMatrix, delta, position, targetWord, isFirstLetter);
            CollectionAssert.AreEqual(actualResultMatrix, expectedResultMatrix);
        }

        [TestMethod]
        public void FindWordInMatrixAndWriteInResultMatrix()
        {
            char[,] Matrix = {      { 'a','b','c','d','n'},
                                    { 'f','c','a','t','o'},
                                    { 't','b','c','d','m'},
                                    { 'h','o','u','s','e'},
                                    { 'a','s','l','s','l'}, };

            char[,] actualResultMatrix = {      { '+','+','+','+','+'},
                                                { '+','+','+','+','+'},
                                                { '+','+','+','+','+'},
                                                { '+','+','+','+','+'},
                                                { '+','+','+','+','+'}
                                 };

            char[,] expectedResultMatrix = {    { '+','+','+','+','n'},
                                                { '+','+','+','+','o'},
                                                { '+','+','+','+','m'},
                                                { '+','+','+','+','e'},
                                                { '+','+','+','+','l'}
                                 };

            Solution.Position position = new Solution.Position(4, 0, 4, 4, 0, 0);
            string targetWord = "lemon";
            bool isFirstLetter = false;

            Solution.lookInClosestTiles(Matrix,ref actualResultMatrix, position, targetWord, isFirstLetter);

            CollectionAssert.AreEqual(actualResultMatrix, expectedResultMatrix);
        }

        [TestMethod]
        public void TotalTestforMainAlgorithm()
        {
            char[,] Matrix = {      { 'a','b','c','d','n'},
                                    { 'f','c','a','t','o'},
                                    { 't','b','c','d','m'},
                                    { 'h','o','u','s','e'},
                                    { 'a','s','l','s','l'} };

            char[,] expectedResultMatrix = {    { '+','+','+','+','n'},
                                                { '+','c','a','t','o'},
                                                { 't','+','+','+','m'},
                                                { 'h','o','u','s','e'},
                                                { '+','+','l','+','l'} };


            string[] targetWords =new string[] { "lemon", "house", "cat", "lot" };
            char[,] actualResultMatrix = Solution.mainAlgorythm(Matrix, targetWords);
           

            CollectionAssert.AreEqual(actualResultMatrix, expectedResultMatrix);
        }




    }
}
