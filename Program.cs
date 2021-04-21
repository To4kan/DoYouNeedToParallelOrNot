using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MultiplyMatrices
{
    class Program
    {
        #region MultiplyMatrices
        static void MultiplyMatricesSequential(double[,] matA, double[,] matB, double[,] result)
        {
            int matARows = matA.GetLength(0);//Строк
            int matACols = matA.GetLength(1);//Столбцов первой
            int matBCols = matB.GetLength(1);//Столбцов второй

            for (int i = 0; i < matARows; i++)
            {
                for (int j = 0; j < matBCols; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < matACols; k++)
                    {
                        temp += matA[i, k] * matB[k, j];
                    }
                    result[i, j] += temp;
                }
            }
        }
        static void MultiplyMatricesParallel(double[,] matA, double[,] matB, double[,] result)
        {
            int matARows = matA.GetLength(0);//Строк
            int matACols = matA.GetLength(1);//Столбцов первой
            int matBCols = matB.GetLength(1);//Столбцов второй

            Parallel.For(0, matARows, i =>
            {
                for (int j = 0; j < matBCols; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < matACols; k++)
                    {
                        temp += matA[i, k] * matB[k, j];
                    }
                    result[i, j] = temp;
                }
            }); // Parallel.For
        }
        #endregion
        #region PrepareData
        static double[,] LoadMatrix(int rows, int cols, string type)
        {
            string InputPath = Path.Combine(Directory.GetCurrentDirectory(), "Input", rows.ToString() + "_" + type + ".txt");
            double[,] matrix = new double[rows, cols];
            StreamReader streamReader = new StreamReader(InputPath);
            for (int i = 0; i < rows; i++)
            {
                string[] row = streamReader.ReadLine().Split(';');
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = Convert.ToDouble(row[j]);
                }
            }
            return matrix;
        }
        #endregion
        static void Main(string[] args)
        {
            #region Готовим директории для записи результатов в файл
            string OutputPath = Path.Combine(Directory.GetCurrentDirectory(), "Output");
            int curentfiles = 0;
            if (Directory.Exists(OutputPath))
            {
                curentfiles = Directory.GetFiles(OutputPath).Count();
                if (curentfiles >= 20* 100*2)
                {
                    Directory.Delete(OutputPath, true);
                    Directory.CreateDirectory(OutputPath);
                    curentfiles = 0;
                }
            }
            else
            {
                Directory.CreateDirectory(OutputPath);
            }
            #endregion
            #region Получаем аргумент какой файл вытаскивать
            int arg;
            int arg2;
            int arg3;
            try
            {
                arg = Convert.ToInt32(args[0]);
                arg2 = Convert.ToInt32(args[1]);
                arg3 = Convert.ToInt32(args[2]);
            }
            catch
            {
                return;
            }
            #endregion
            #region Создение матрицы
            double[,] m1 = LoadMatrix(arg, arg, "1");
            double[,] m2 = LoadMatrix(arg, arg, "2");
            double[,] result = new double[arg, arg];
            #endregion
            Stopwatch stopwatch = new Stopwatch();
            #region Последовательное вычисление
            if (arg2 == 1)
            {
                stopwatch.Start();
                MultiplyMatricesSequential(m1, m2, result);
                stopwatch.Stop();
                File.WriteAllText(Path.Combine(OutputPath, arg.ToString() + "_1_"+arg3.ToString()+".txt"), stopwatch.ElapsedMilliseconds.ToString());
            }
            #endregion
            #region Параллельное вычисление
            if (arg2 == 2)
            {
                stopwatch.Start();
                MultiplyMatricesParallel(m1, m2, result);
                stopwatch.Stop();
                File.WriteAllText(Path.Combine(OutputPath, arg.ToString() + "_2_" + arg3.ToString() +".txt"), stopwatch.ElapsedMilliseconds.ToString());
            }
            #endregion
        }
    }
}
