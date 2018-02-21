using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_Search_Solver
{
    public static class Solution
    {
        public struct Position
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

        public struct Delta
        {
            public int Horizontal, Vertical;
            public Delta(int h, int v)
            {
                Horizontal = h;
                Vertical = v;
            }
        }

        

        /// <summary>
        /// This is main method in which we are taking every letter, looking if some of the target words has it 
        /// on first or last position, if it's true, we call another method to find out, if it's the whole word or not.
        /// </summary>
        /// <param name="Matrix"> input matrix with letters</param>
        /// <param name="targetWords"> string array of words to find in Matrix</param>
        static public char[,] mainAlgorythm(char[,] Matrix, string[] targetWords)
        {

            //here we get sizes of our Matrix
            int matrixHorizSize = Matrix.GetUpperBound(0) + 1;
            int matrixVertSize = Matrix.GetUpperBound(1) + 1;

            char[,] resultMatrix = new char[matrixHorizSize, matrixVertSize];
            for (int i = 0; i < matrixHorizSize; i++)
                for (int j = 0; j < matrixVertSize; j++)
                    resultMatrix[i, j] = '+';

            
            Position position = new Position();
            position.Min_x = 0;
            position.Min_y = 0;
            position.Max_x = matrixHorizSize - 1;
            position.Max_y = matrixVertSize - 1;

            //two arrays of first and last symbols of target words; we will compare every symbol in Matrix with letters from these arrays
            //if we have a match, then we will go further and search for whole word
            char[] firstLetters =new char[targetWords.Length];
            char[] lastLetters = new char[targetWords.Length];

            for (int i=0; i<targetWords.Length; i++)
            {
                firstLetters[i] = targetWords[i][0];
                lastLetters[i] = targetWords[i][targetWords[i].Length-1];
            }

            

            for (int i=0; i< matrixVertSize; i++)
            {
                position.y = i;

                for (int j=0; j<matrixHorizSize; j++ )
                {
                    position.x = j;

                    for (int k=0; k< targetWords.Length; k++)
                    {
                        if (Matrix[i, j].Equals(firstLetters[k]))
                            lookInClosestTiles(Matrix, ref resultMatrix, position, targetWords[k], true);

                        if (Matrix[i, j].Equals(lastLetters[k]))
                            lookInClosestTiles(Matrix, ref resultMatrix, position, targetWords[k], false);
                    }
                }
            }

            return resultMatrix;

        }

        /// <summary>
        /// this method creates 8 delta-structures for 8 directions,
        /// calls findWord method for searching in that directions
        /// and? if it finds word, writes word into result matrix
        /// </summary>
        /// <param name="Matrix">          matrix of letters  </param>
        /// <param name="resultMatrix">    matrix, which contains only target words and '+' in other positions </param>
        /// <param name="position">        cell from which we start searching </param>
        /// <param name="targetWord">      word, which we a looking for </param>
        /// <param name="isFirstLetter">   determine, whether we are searching for straight word(true) or its reverse version(false)    </param>
        static public void lookInClosestTiles(char[,] Matrix, ref char[,] resultMatrix,  Position position, string targetWord, bool isFirstLetter)
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

                if (findWordInDirection(Matrix, delta, position, targetWord, isFirstLetter))
                {
                    insertWordInResultMatrix(ref resultMatrix, delta, position, targetWord, isFirstLetter);
                    return;
                }

            }

        }

        /// <summary>
        /// this methos insert found word (symbol by symbol) into result matrix
        /// </summary>
        /// <param name="resultMatrix">    matrix, which contains only target words and '+' in other positions </param>
        /// <param name="delta">           struct, contains direction for inserting word  </param>
        /// <param name="position">        cell from which we start inserting </param>
        /// <param name="targetWord">      word, which we a inserting </param>
        /// <param name="isFirstLetter">   determine, whether we are inserting straight word(true) or its reverse version(false)    </param>
        static public void insertWordInResultMatrix(ref char[,] resultMatrix, Delta delta, Position position, string targetWord, bool isFirstLetter)
        {
            //if we are inserting word backward, we should reverse it
            if (!isFirstLetter)
                targetWord = new string(targetWord.ToCharArray().Reverse().ToArray());
                
            for (int i=0; i< targetWord.Length; i++)
            {
                resultMatrix[ position.y, position.x] = targetWord[i];
                position.x += delta.Horizontal;
                position.y += delta.Vertical;

            }

        }


        /// <summary>
        /// This method is checking letters on 1 of 8 possible direction(N, NW, W, SW, S, SE, E, NE), whether they match for target word
        /// </summary>
        /// <param name="Matrix">          matrix of letters  </param>
        /// <param name="delta">           struct, contains direction for moving and searching through matrix  </param>
        /// <param name="position">        cell from which we start searching </param>
        /// <param name="targetWord">      word, which we a looking for </param>
        /// <param name="isFirstLetter">   determine, whether we are searching for straight word(true) or its reverse version(false)    </param>
        /// <returns> true, if method has found word, otherwise returns false </returns>
        static public bool findWordInDirection(char[,] Matrix, Delta delta, Position position, string targetWord, bool isFirstLetter)
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
                // if next symbol in our imaginary chain of letter isn't equal to symbol in target word, we immediatly stop searching
                if (Matrix[position.y, position.x] != targetWord[index])
                {
                    return false;
                }
                position.x += delta.Horizontal;
                position.y += delta.Vertical;
                index += step;
                countFoundLetters++;

                //we check until we've found word or we've reached the edge of matrix
            } while (position.x >= position.Min_x &&
                        position.x <= position.Max_x &&
                        position.y >= position.Min_y &&
                        position.y <= position.Max_y &&
                        countFoundLetters < targetWord.Length);
            //if yes, we have found the word, else, we reached the end of matrix and should stop searching
            if (countFoundLetters == targetWord.Length)
                return true;
            return false;
        }

       
    }
}
