using System;
using System.Threading.Tasks;

namespace Matrix_Multiplication
{

    class Program
    {
        public static int N = Convert.ToInt16(Console.ReadLine()); //размер матриц N*N

        public static int[,] matrix1;
        public static int[,] matrix2;
        public static int[,] MainMatrix;
        private static Random _random = new Random(); //для заполнения матриц

        public static void DisplayMatrix() //метод заполнения матриц
        {
            Console.Write("Введите размер матрицы. N= ");
            matrix1 = new int[N, N];
            Console.WriteLine("Матрица 1:");
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrix1[i, j] = _random.Next() % 10;
                    Console.Write($"{matrix1[i, j]}\t");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            matrix2 = new int[N, N];
            Console.WriteLine("Матрица 2:");
            for (int i = 0; i < matrix2.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    matrix2[i, j] = _random.Next() % 10;
                    Console.Write($"{matrix2[i, j]}\t");
                }

                Console.WriteLine();
            }

        }

        public static void Main(string[] args)
        {
            DisplayMatrix();
            Multiplication(matrix1, matrix2);
            DisplayMainMatrix(MainMatrix);

        }

        static int[,] Multiplication(int[,] matrix1, int[,] matrix2)
        {
            MainMatrix = new int[matrix1.GetLength(1), matrix2.GetLength(0)];
            var timer = System.Diagnostics.Stopwatch.StartNew();
            Parallel.For((long) 0, matrix1.GetLength(1), i =>
            {
                for (int j = 0; j < matrix2.GetLength(0); j++)
                {
                    MainMatrix[i, j] = 0;
                    for (int k = 0; k < matrix1.GetLength(1); k++)
                    {
                        MainMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
                    }


                }


            });

            timer.Stop();
            Console.WriteLine($"Время вычислений в  многопоточнй программе {timer.ElapsedMilliseconds}");
            return MainMatrix;
        }

        static void DisplayMainMatrix(int[,] m)
        {
            Console.WriteLine("Получившаяся матрица");
            MainMatrix = new int[matrix1.GetLength(1), matrix2.GetLength(0)];
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    Console.Write($"{m[i, j]}\t");
                }

                Console.WriteLine();
            }
        }



    }
}
        
        
       
   
        
    