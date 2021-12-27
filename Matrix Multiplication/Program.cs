using System;
using System.Threading;

namespace Matrix_Multiplication
{

    class Program
    {

        
        public static int N = Convert.ToInt16(Console.ReadLine()); //размер матриц N*N

        public static int[,] matrix1;
        public static int[,] matrix2;
        public static int[,] MainMatrix;
        private static Semaphore _semaphore = new Semaphore( 2,4); 
        private static bool _flag = false;
        private static Random _random = new Random(); //для заполнения матриц
        
        public static void DisplayMatrix() //метод заполнения матриц
        {
            Console.Write("Введите размер  квадратной матрицы. N= ");
            

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
            MainMatrix = new int[N, N];
            

            Thread[] calculators = new Thread[N * N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    calculators[N * i + j] = new Thread(Multiplication);
                    calculators[N * i + j].Name = $"{i}{j}";
                }
            }

            foreach (var c in calculators) //стартуем потоки
            {
                c.Start();
                c.Join();
            }
            foreach (var c in calculators) //ожидаем потоки
            {
                c.Join();
            }

            
        }

        public static void Multiplication()
        {

            int m_matrix = new int();
            int[] numbers = new int[2];
            for (int i = 0; i < 2; i++)
            {
                numbers[i] = Int32.Parse(Thread.CurrentThread.Name[i].ToString());
            }

            for (int i = 0; i < N; i++)
            {
                m_matrix += matrix1[numbers[0], i] * matrix2[i, numbers[1]];
            }

            Console.WriteLine($"Поток {Thread.CurrentThread.Name} посчитал значение элемента матрицы {m_matrix}");
            if (_flag) _semaphore.WaitOne();
            

            MainMatrix[numbers[0], numbers[1]]=m_matrix;
                
            Console.WriteLine($"Значение добавлено в матрицу на позицию [{numbers[0]}, {numbers[1]}]");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write($"{MainMatrix[i, j]}\t");
                }

                Console.WriteLine();
            }
        }
        
    }
}
        
        
       
   
        
    