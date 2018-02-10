using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_Search_Solver
{
   static class Solution
    {
        struct Position
        {
          static public int x, y;
          static public int Max_x, Max_y, Min_x, Min_y;
        }

        static char[,] Matrix;

        /// <summary>
        /// This is main method in which we are taking every letter, looking if some of the target words has it 
        /// on first or last position, if it's true, we call another method to find out, if it's the whole word or not.
        /// </summary>
        /// <param name="Matrix"></param>
        /// <param name="targetWords"></param>
        static void mainAlgorythm(char[,] Matrix, string[] targetWords)
        {
            int matrixHorizSize = Matrix.GetUpperBound(0) + 1;
            int matrixVertSize = Matrix.GetUpperBound(1) + 1;

            Solution.Matrix = Matrix;
            Position.Min_x = 0;
            Position.Min_y = 0;
            Position.Max_x = matrixHorizSize - 1;
            Position.Max_y = matrixVertSize - 1;

            char[] firstLetters =new char[targetWords.Length];
            char[] lastLetters = new char[targetWords.Length];

            for (int i=0; i<targetWords.Length; i++)
            {
                firstLetters[i] = targetWords[i][0];
                lastLetters[i] = targetWords[i][targetWords[i].Length-1];
            }

            

            for (int i=0; i< matrixVertSize; i++)
            {
                for (int j=0; j<matrixHorizSize; j++ )
                {
                    for (int k=0; k<firstLetters.Length; k++)
                    {
                        if (Matrix[i,j].Equals(firstLetters[k]))
                        {

                        }

                        if (Matrix[i, j].Equals(lastLetters[k]))
                        {

                        }
                    }
                }
            }
        }

        static void lookInClosestTiles(Position position, string targetWord, bool isFirstLetter)
        {

        }

        static bool findWord(int deltaHorizontal, int deltaVertical, Position position, string targetWord, bool isFirstLetter)
        {
            int index, step, countFoundLetters=0;
            if (isFirstLetter)
            {
                index = 0;
                step = 1;
            }
            else
            {
                index = targetWord.Length - 1;
                step = -1;
            }

            do
            {
                if (Matrix[Position.x, Position.y] != targetWord[index])
                {
                    return false;
                }
                Position.x += deltaHorizontal;
                Position.y += deltaVertical;
                index += step;
                countFoundLetters++;

            } while (Position.x > Position.Min_x &&
                        Position.x < Position.Max_x &&
                        Position.y > Position.Min_y &&
                        Position.y < Position.Max_y && 
                        countFoundLetters<targetWord.Length)
            if (countFoundLetters == targetWord.Length)
                return true;
            return false;
        }

       
    }
}
