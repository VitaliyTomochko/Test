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
                {1, 1, 1, 1, 1, 1, 1},
                {2, 2, 1, 1, 1, 1, 1},
                {2, 2, 1, 1, 1, 1, 1},
                {2, 2, 5, 5, 5, 5, 5},
                {2, 2, 5, 5, 5, 5, 5},
                {2, 2, 7, 7, 7, 7, 7},
                {2, 2, 7, 7, 7, 7, 7}};
        }
        /*  matrix = new int[,] {
              {1, 1, 1, 2, 1, 1, 1},
              {2, 2, 2, 2, 2, 2, 4},
              {2, 2, 2, 2, 2, 2, 4},
              {6, 2, 6, 2, 2, 2, 6},
              {6, 6, 6, 2, 2, 2, 6},
              {8, 8, 8, 2, 2, 2, 1},
              {8, 8, 8, 8, 0, 0, 1}};

      }*/

        public int searchInRow(int i, int j)
        {
            int width = 1;
            int iter = i;
            int jter = j;
            for (; jter < m - 1; jter++)
            {
                if (matrix[iter, jter] == matrix[iter, jter + 1])
                {
                    width += 1;
                }
                else break;
            }
            //    elementsSubMatrix[countS++] = new Struct(i, j, jter, width, ((width - i + 1) * (jter - j + 1)), matrix[i, j]);
            return width;
        }
        public int searchInColomn(int i, int j)
        {
            int height = 1;
            int jter = j;
            int iter = i;
            for (; iter < n - 1; iter++)
            {
                if (matrix[iter, jter] == matrix[iter + 1, jter])
                {
                    height += 1;
                }
                else break;
            }
            //    elementsSubMatrix[countS++] = new Struct(i, j, iter, height, ((height - i + 1) * (height - j + 1)), matrix[i, j]);
            return height;
        }

        public bool hasAdjacent(int i, int j, ref bool isColomn, ref bool isRow)
        {
            if (i + 1 == n - 1 && j + 1 == m - 1)
            {
                isColomn = false;
                isRow = false;
                return false;
            }
            if(i<n-1 && j<m-1){
               if (i < j) isRow = true;
                if (i > j) isColomn = true;
                if (i == j)
                {
                    isRow = false;
                    isColomn = false;
                }
                if (matrix[i, j] == matrix[i + 1, j] &&
                  matrix[i, j] == matrix[i, j + 1] &&
                  matrix[i, j] == matrix[i + 1, j + 1])
                {
                    return true;
                }
            }
            return false;
        }

        public void searchInSubMatrix(int i, int j, int width, int height)
        {
            bool isTop = false;
            int iterSquare = i;
            int jterSquare = j;
            bool isRow = false, isColomn = false;
            //Console.WriteLine($"{matrixLengthRow} {matrixLengthColomn}");
            for (; iterSquare < height + i; iterSquare++)
            {
                for (jterSquare = j; jterSquare < width + j; jterSquare++)
                {

                    if (hasAdjacent(iterSquare, jterSquare, ref isColomn, ref isRow) == true)
                    {
                        isTop = true;
                                         
                    }
                    if (hasAdjacent(iterSquare, jterSquare, ref isColomn, ref isRow) == false)
                    {

                        break;
                    }
                }

                if (isTop == true)
                {
                    break;
                }
            }

            if (isTop)
                elementsSubMatrix[countS++] = new Struct(i, j,  height - 1 + i, jterSquare , ((height) * (jterSquare-j+1)), matrix[i, j]);

            j++;
            if (j == m - 1 && i < n - 1)
            {
                i++;
                j = 0;

            }
            if (i == n - 1)
            {
                Console.WriteLine("Done");
                return;
            }
            if (i < n && j < m)
                searchInSubMatrix(i, j, searchInRow(i, j), searchInColomn(i, j));

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
            program.searchInSubMatrix(0, 0, program.searchInRow(0, 0), program.searchInColomn(0, 0));
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


