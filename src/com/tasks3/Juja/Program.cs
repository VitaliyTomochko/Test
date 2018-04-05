using System;
using System.Linq;

namespace Prog
{

    public class Struct
    {
        public int x0 { get; set; }
        public int y0 { get; set; }
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int square { get; set; }
        public int color;
        public Struct()
        {

        }

        public Struct(int x0, int y0, int x1, int y1, int square, int color)
        {
            this.x0 = x0;
            this.x1 = x1;
            this.y0 = y0;
            this.y1 = y1;
            this.square = square;
            this.color = color;

        }
    }

    class Functions : Struct
    {
        public int n;
        public int m;
        public Struct[] elementsRowColomn;
        public Struct[] elementsSubMatrix;
        public int[,] matrix;
        public int countRC = 0;
        public int countS = 0;

        public Functions()
        {

        }

        public Functions(int n, int m)
        {
            this.n = n;
            this.m = m;
            elementsRowColomn = new Struct[n * m];
            elementsSubMatrix = new Struct[n * m];
        }

        public void print()
        {
            Console.WriteLine();
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                    Console.Write(matrix[i, j] + "\t");
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        public void inputMatrix()
        {
            /*
            for (int i = 0; i < n; ++i)
            {
                string enterString = Console.ReadLine();
                string[] arrayString = enterString.Split(new Char[] { ' ' });
                for (int j = 0; j < n; ++j)
                {
                    matrix[i, j] = int.Parse(arrayString[j]);
                }
            }*/
            /*  matrix = new double[n, m];
              Random rand = new Random();
              for (int i = 0; i < n; i++)
              {
                  for (int j = 0; j < m; j++)
                      matrix[i, j] = rand.Next(0, 2);

              }*/
            matrix = new int[,] {
                {2, 2, 2, 2, 2, 4, 4},
                {2, 2, 2, 2, 2, 2, 4},
                {2, 2, 2, 2, 2, 7, 4},
                {4, 4, 6, 6, 6, 6, 4},
                {4, 4, 6, 6, 6, 6, 4},
                {5, 5, 6, 6, 6, 6, 7},
                {8, 4, 4, 4, 4, 4, 4}};
        }

        public int searchInRow(int i, int j, int lengthRow)
        {
            int matrixLengthRow = 1;
            int iterRow = i;
            for (int jterRow = j; jterRow <= lengthRow - 1; jterRow++)
            {
                if (matrix[iterRow, jterRow] == matrix[iterRow, jterRow + 1])
                {
                    matrixLengthRow += 1;

                }
                else break;
            }
            return matrixLengthRow;

        }
        public int searchInColomn(int i, int j, int lengthColomn)
        {
            int matrixLengthColomn = 1;
            int jterColomn = j;
            for (int iterColomn = i; iterColomn <= lengthColomn - 1; iterColomn++)
            {
                if (matrix[iterColomn, jterColomn] == matrix[iterColomn + 1, jterColomn])
                {
                    matrixLengthColomn += 1;
                }
                else break;
            }
            return matrixLengthColomn;
        }

        public bool hasAdjacent(int i, int j)
        {

            return (matrix[i, j] == matrix[i + 1, j] &&
                        matrix[i, j] == matrix[i, j + 1] &&
                    matrix[i, j] == matrix[i + 1, j + 1]);
        }

        public void searchInSubMatrix(int i, int j, int matrixLengthRow, int matrixLengthColomn)
        {
            bool isTop = false;
            int iterSquare = i;
           int jterSquare = j;
            for (; iterSquare < matrixLengthRow - 1; iterSquare++)
            {
                for (; jterSquare < matrixLengthColomn - 1; jterSquare++)
                {
                    if (hasAdjacent(iterSquare, jterSquare))
                    {
                        isTop = true;
                    }
                    else break;
                }
            }
            if (isTop)
            {
                 elementsSubMatrix[countS++] = new Struct(i,j,jterSquare,iterSquare,((iterSquare - i + 1) * (jterSquare - j + 1)), matrix[i, j]);
               // searchInSubMatrix(i++, jterSquare, searchInRow(i++, jterSquare, m - 1), searchInColomn(i++, jterSquare, n - 1));
            }
          //  searchInSubMatrix(i+1, j+1, searchInRow(i+1, j+1, m - 1), searchInColomn(i+1, j+1, n - 1));
              
        }
 
      }



    class Program
    {
        static void Main()
        {
            Functions program = new Functions(7, 7);
            program.inputMatrix();
            Console.WriteLine("Matrix :\n");
            program.print();
            program.searchInSubMatrix(0, 0 , program.searchInRow(0, 0, program.m - 1), program.searchInColomn(0, 0, program.n - 1));
;
            //  Console.WriteLine($"[x0,y0] [{program.elements[0].x0},{program.elements[0].y0}]");
            /*  for (int i = 0; i < program.countRC; i++)
              {
                  Console.WriteLine($"\nRESULT :");
                  Console.WriteLine($"[x0,y0] [{program.elementsRC[i].x0},{program.elementsRC[i].y0}]");
                  Console.WriteLine($"[x1,y1] [{program.elementsRC[i].x1},{program.elementsRC[i].y1}]");
                  //Console.WriteLine($"S [{program.elements[i].square}]");
                  Console.WriteLine($"color [{program.elementsRC[i].color}]");

              }*/
             for (int i = 0; i < program.countS; i++)
             {
                 Console.WriteLine($"\nRESULT :");
                 Console.WriteLine($"[x0,y0] [{program.elementsSubMatrix[i].x0},{program.elementsSubMatrix[i].y0}]");
                 Console.WriteLine($"[x1,y1] [{program.elementsSubMatrix[i].x1},{program.elementsSubMatrix[i].y1}]");
                 Console.WriteLine($"S [{program.elementsSubMatrix[i].square}]");
                 Console.WriteLine($"color [{program.elementsSubMatrix[i].color}]");

             }
        }
    }
}


