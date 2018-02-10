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
           public int x, y;
           public int Max_x, Max_y, Min_x, Min_y;
            public Position(int x1, int y1,  int maxx, int maxy, int minx, int miny)
            {
                x = x1;
                y = y1;
                Max_x = maxx;
                Max_y = maxy;
                Min_x = minx;
                Min_y = miny;

            }

        }

        struct Delta
        {
            public int Horizontal, Vertical;
            public Delta(int h, int v)
            {
                Horizontal = h;
                Vertical = v;
            }
        }

        static char[,] Matrix, resultMatrix;

        /// <summary>
        /// This is main method in which we are taking every letter, looking if some of the target words has it 
        /// on first or last position, if it's true, we call another method to find out, if it's the whole word or not.
        /// </summary>
        /// <param name="Matrix"></param>
        /// <param name="targetWords"></param>
        static char[,] mainAlgorythm(char[,] Matrix, string[] targetWords)
        {
            int matrixHorizSize = Matrix.GetUpperBound(0) + 1;
            int matrixVertSize = Matrix.GetUpperBound(1) + 1;

            resultMatrix = new char[matrixHorizSize, matrixVertSize];

            Solution.Matrix = Matrix;
            Position position = new Position();
            position.Min_x = 0;
            position.Min_y = 0;
            position.Max_x = matrixHorizSize - 1;
            position.Max_y = matrixVertSize - 1;

            char[] firstLetters =new char[targetWords.Length];
            char[] lastLetters = new char[targetWords.Length];

            for (int i=0; i<targetWords.Length; i++)
            {
                firstLetters[i] = targetWords[i][0];
                lastLetters[i] = targetWords[i][targetWords[i].Length-1];
            }

            

            for (int i=0; i< matrixVertSize; i++)
            {
                position.x = i;

                for (int j=0; j<matrixHorizSize; j++ )
                {
                    position.y = j;

                    for (int k=0; k< targetWords.Length; k++)
                    {
                        if (Matrix[i,j].Equals(firstLetters[k]))
                            lookInClosestTiles(position, targetWords[k], true);
                        
                        if (Matrix[i, j].Equals(lastLetters[k]))
                            lookInClosestTiles(position, targetWords[k], true);
                    }
                }
            }

            return resultMatrix;

        }

        static void lookInClosestTiles(Position position, string targetWord, bool isFirstLetter)
        {
              /*
            those arrays contain flags, determing in which direction of axis movement will be,
             for example, (1,-1) means moving Right on x-axis (1) and Up on Y-axis (-1), so in result
            we will be moving to the North-East direction
              */
            int[] dHorizontalValues = new int[] { 1, 1, 0, -1, -1, -1, 0, 1 };
            int[] dVerticalValues = new int[]   { 0, -1, -1, -1, 0, 1, 1, 1 };
            
            for (int i=0; i<8; i++) // 8 directions
            {
                Delta delta = new Delta(dHorizontalValues[i], dVerticalValues[i]);

                if (findWord(delta, position, targetWord, isFirstLetter))
                {
                    insertWordInResultMatrix(delta, position, targetWord, isFirstLetter);
                    return;
                }

            }

        }


        static void insertWordInResultMatrix(Delta delta, Position position, string targetWord, bool isFirstLetter)
        {
            if (!isFirstLetter)
                targetWord.Reverse();
            for (int i=0; i< targetWord.Length; i++)
            {
                resultMatrix[position.x, position.y] = targetWord[i];
                position.x += delta.Horizontal;
                position.y += delta.Vertical;

            }

        }


        /// <summary>
        /// This method is checking letters on 1 of 8 possible direction(N, NW, W, SW, S, SE, E, NE), whether they match for target word
        /// </summary>
        /// <param name="deltaHorizontal"> step for moving on X-axis of matrix  </param>
        /// <param name="deltaVertical">   step for moving on Y-axis of matrix </param>
        /// <param name="position">        cell from which we start searching </param>
        /// <param name="targetWord">      word, which we a looking for </param>
        /// <param name="isFirstLetter">   determine, whether we are searching for straight word(true) or its reverse version(false)    </param>
        /// <returns> true, if method has found word, otherwise returns false </returns>
        static bool findWord(Delta delta, Position position, string targetWord, bool isFirstLetter)
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
                Position.x += delta.Horizontal;
                Position.y += delta.Vertical;
                index += step;
                countFoundLetters++;

                //we check until we've found word or we've reached the edge of matrix
            } while (Position.x > Position.Min_x &&
                        Position.x < Position.Max_x &&
                        Position.y > Position.Min_y &&
                        Position.y < Position.Max_y &&
                        countFoundLetters < targetWord.Length);

            if (countFoundLetters == targetWord.Length)
                return true;
            return false;
        }

       
    }
}
