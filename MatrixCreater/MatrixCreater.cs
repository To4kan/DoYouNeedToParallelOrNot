using System;
using System.IO;
using System.Threading.Tasks;

namespace MatrixCreater
{
    class Program
    {
        static string path = Path.Combine(Directory.GetCurrentDirectory(), "Input");
        static void Main(string[] args)
        {
            #region Очистка директорий перед созданием матриц
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }
            #endregion
            ///Указывается размерность помноженная на 10
            ///C# считает до - 1, поэтому + 1
            int Start;
            int End;
            try
            {
                Console.WriteLine("Введите минимальную размерность массива (в десятках)");
                Start = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите максимальную размерность массива (в десятках)");
                End = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return;
            }
            
            Parallel.For(Start, End + 1, i =>
              {
                  InitializeMatrix(i * 10, i * 10, "1");
                  InitializeMatrix(i * 10, i * 10, "2");
                  Console.WriteLine($"Матрицы {i * 10}x{i * 10} были созданы");
              });
        }
        #region Создание Матриц
        static void InitializeMatrix(int rows, int cols, string type)
        {
            StreamWriter streamWriter = new StreamWriter(Path.Combine(path, rows.ToString() + "_" + type + ".txt"));
            Random r = new Random();
            for (int i = 0; i < rows; i++)
            {
                string row = "";
                for (int j = 0; j < cols; j++)
                {
                    row += j < cols - 1 ? r.Next(100).ToString() + ";" : r.Next(100).ToString();
                }
                streamWriter.WriteLine(row);
            }
            streamWriter.Close();
        }
        #endregion
    }
}
