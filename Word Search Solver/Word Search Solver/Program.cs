using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_Search_Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            int rowNumber, columnNumber;
            char[,] Matrix;

            try {
                Console.WriteLine("Write number of rows in matrix:");
                rowNumber = Convert.ToInt32( Console.ReadLine());
                Console.WriteLine("Write number of columns in matrix:");
                columnNumber = Convert.ToInt32(Console.ReadLine());
                if (!validateInputSize(rowNumber, columnNumber))
                {
                    Console.Write("Wrong input, app is closing!");
                    return;
                }

                Matrix = new char[rowNumber, columnNumber];
                Console.WriteLine("Write your matrix without white spaces, you can use new lines:");

                for (int i=0; i<rowNumber; i++)
                {
                    string input = Console.ReadLine();
                    for (int j=0; j< columnNumber; j++)
                    {
                        Matrix[i, j] = input[j];
                    }
                }
                
                Console.WriteLine("Your matrix is ");
                for (int i = 0; i < rowNumber; i++)
                {
                    for (int j = 0; j < columnNumber; j++)
                    {
                        Console.Write(Matrix[i,j]);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("Write target words to find, separated by commas or white spaces: ");
                string[] targetWords = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);



            }
            catch
            {
                Console.WriteLine("Something is wrong, app will be closed, sorry");
                return;
            }

        }

        static bool validateInputSize(int rowNumber, int columnNumber)
        {
            if (rowNumber <= 0 || columnNumber <= 0)
                return false;
            return true;
        }
    }
}
