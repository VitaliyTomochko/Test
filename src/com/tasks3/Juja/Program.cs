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
        public Struct[] elementsRC;
        public Struct[] elementsS;
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
            elementsRC = new Struct[49];
            elementsS = new Struct[49];
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
                {2, 2, 3, 0, 3, 4, 4},
                {5, 5, 0, 3, 1, 2, 4},
                {1, 2, 0, 7, 1, 7, 4},
                {4, 4, 6, 6, 6, 6, 4},
                {4, 4, 6, 6, 6, 6, 4},
                {5, 5, 6, 6, 6, 6, 7},
                {8, 4, 4, 4, 4, 4, 4}};
        }
        public void searchInRow(int i, int j, int lengthRow, ref int matrixLengthRow, ref bool isTrue, ref int iterRow, ref int jterRow)
        {
            isTrue = false;
            iterRow = i;
            for (jterRow = j; jterRow < lengthRow; jterRow++)
            {
                if (matrix[iterRow, jterRow] == matrix[iterRow, jterRow + 1])
                {
                    isTrue = true;
                    matrixLengthRow += 1;

                }
                else break;
            }

        }
        public void searchInColomn(int i, int j, int lengthColomn, ref int matrixLengthColomn, ref bool isTrue, ref int iterColomn, ref int jterColomn)
        {
            isTrue = false;
            jterColomn = j;
            for (iterColomn = i + 1; iterColomn < lengthColomn - 1; iterColomn++)
            {
                if (matrix[iterColomn, jterColomn] == matrix[iterColomn + 1, jterColomn])
                {
                    isTrue = true;
                    matrixLengthColomn += 1;

                }
                else break;
            }
        }
        public void searchInSubMatrix(ref bool isSquare,int i , int j , int matrixLengthRow, int matrixLengthColomn)
        {
            bool isTop = false;
            bool isLeft = false;
            isSquare = true;
           int iterSquare = i + 1;
           int jterSquare = j + 1;
            for (; iterSquare < 6; iterSquare++)
            {
                for (; jterSquare < 6; jterSquare++)
                {
                    if (matrix[iterSquare - 1, jterSquare] == matrix[iterSquare + 1, jterSquare] &&
                        matrix[iterSquare, jterSquare] == matrix[iterSquare, jterSquare + 1] &&
                        matrix[iterSquare, jterSquare] == matrix[iterSquare + 1, jterSquare + 1])
                    {
                        isTop = true;
                        x1 = iterSquare + 1;
                        y1 = jterSquare + 1;

                    }
                }
            }
            if (isTop)
                elementsS[countS++] = new Struct(i, j, x1, y1, ((iterSquare - i + 1) * (jterSquare - j + 1)), matrix[i, j]);
           
            for (; iterSquare < 6; iterSquare++)
            {
                for (; jterSquare < 6; jterSquare++)
                {
                    if (matrix[iterSquare, jterSquare - 1] == matrix[iterSquare + 1, jterSquare] &&
                        matrix[iterSquare, jterSquare] == matrix[iterSquare, jterSquare + 1] &&
                        matrix[iterSquare, jterSquare] == matrix[iterSquare + 1, jterSquare + 1])
                    {
                        isLeft = true;
                        x1 = iterSquare + 1;
                        y1 = jterSquare + 1;

                    }
                }
            }
            if (isLeft)
                elementsS[countS++] = new Struct(i, j, x1, y1, ((iterSquare - i + 1) * (jterSquare - j + 1)), matrix[i, j]);
            if (isLeft && isTop)
            {
                iterSquare ++;
                jterSquare++;
                searchInSubMatrix(ref isSquare,  i,  j, matrixLengthRow, matrixLengthColomn);
                    
                        }
        }

        public void findSubMatrix()
        {
            int iterRow = 0, iterColomn = 0, iterSquare = 0;
            int jterRow = 0, jterColomn = 0, jterSquare = 0;
            int matrixLengthRow = 1, matrixLengthColomn = 1;
            // int subMatrixLengthRow = 1, subMatrixLengthColomn = 1;
            bool isRow = false, isColomn = false, isSquare = false;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < m - 1; j++)
                {
                    if (j + 1 == m - 1)
                    {
                        searchInColomn(i, j + 1, n - 1, ref matrixLengthColomn, ref isColomn, ref iterColomn, ref jterColomn);
                        if (isColomn)
                        {
                            elementsRC[countRC++] = new Struct(i, j + 1, iterColomn, jterColomn, ((iterColomn - i + 1) * (jterColomn - j + 1)), matrix[i, j + 1]);
                            isColomn = false;
                            matrixLengthColomn = 1;
                        }
                    }
                    if (i + 1 == n - 1)
                    {
                        searchInRow(i + 1, j, m - 1, ref matrixLengthRow, ref isRow, ref iterRow, ref jterRow);
                        if (isRow)
                        {
                            elementsRC[countRC++] = new Struct(i + 1, j, iterRow, jterRow, ((iterRow - i + 1) * (jterRow - j + 1)), matrix[i + 1, j]);
                            isRow = false;
                            matrixLengthRow = 1;
                        }
                    }
                    if (matrix[i, j] == matrix[i, j + 1])
                    {
                        isRow = true;
                        iterRow = i;
                        jterRow = j + 1;
                        for (; jterRow < m - 1; jterRow++)
                        {
                            if (matrix[iterRow, jterRow] == matrix[iterRow, jterRow + 1])
                            {
                                matrixLengthRow += 1;
                            }
                            else break;
                        }
                    }
                    if (matrix[i, j] == matrix[i + 1, j])
                    {
                        isColomn = true;
                        iterColomn = i + 1;
                        jterColomn = j;
                        for (; iterColomn < n - 1; iterColomn++)
                        {
                            if (matrix[iterColomn, jterColomn] == matrix[iterColomn + 1, jterColomn])
                            {
                                matrixLengthColomn += 1;
                            }
                            else break;
                        }
                    }
                    if (matrix[i, j] == matrix[i + 1, j] &&
                        matrix[i, j] == matrix[i, j + 1] &&
                        matrix[i, j] == matrix[i + 1, j + 1])
                    {
                  //     bool isTop;
                  //     bool isLeft;
                        searchInSubMatrix(ref isSquare,  i,  j, matrixLengthRow, matrixLengthColomn);
                    }




                    if (isRow == true)
                    {
                        elementsRC[countRC++] = new Struct(i, j, iterRow, jterRow, (((iterRow - i + 1) * (jterRow - j + 1))), matrix[i, j]);
                        isRow = false;
                        matrixLengthRow = 1;
                    }
                    if (isColomn == true)
                    {
                        elementsRC[countRC++] = new Struct(i, j, iterColomn, jterColomn, ((iterColomn - i + 1) * (jterColomn - j + 1)), matrix[i, j]);
                        isColomn = false;
                        matrixLengthColomn = 1;
                    }
                }
            }
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
            program.findSubMatrix();
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
                Console.WriteLine($"[x0,y0] [{program.elementsS[i].x0},{program.elementsS[i].y0}]");
                Console.WriteLine($"[x1,y1] [{program.elementsS[i].x1},{program.elementsS[i].y1}]");
                Console.WriteLine($"S [{program.elementsS[i].square}]");
                Console.WriteLine($"color [{program.elementsS[i].color}]");

            }
        }
    }
}

