using System;
using System.Diagnostics;
using System.IO;

namespace CollectDataFromMultiplyMatrices
{
    class Program
    {
        static string CurrntPath = Directory.GetCurrentDirectory();
        static int Quantity = 20;
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите начальную размерность матрицы в десятках");
                int starti = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите конечную размерность матрицы в десятках");
                int stopi = Convert.ToInt32(Console.ReadLine());
                for (int i=starti*10; i <= stopi*10; i += 10)
                {
                    for (int j = 1; j <= 2; j++)
                    {
                        for (int count = 1; count <= Quantity; count++)
                        {
                            var process = Process.Start(Path.Combine(CurrntPath, "MultiplyMatrices.exe"),
                                i.ToString() + " " +
                                j.ToString() + " " +
                                count.ToString());
                            process.WaitForExit();
/*
                            Process p = new Process();
                            p.StartInfo.FileName = Path.Combine(CurrntPath, "EmptyStandbyList.exe");
                            p.StartInfo.Arguments = "workingsets|modifiedpagelist|standbylist|priority0standbylist";
                            p.StartInfo.UseShellExecute = false;
                            p.StartInfo.CreateNoWindow = true;
                            p.StartInfo.RedirectStandardOutput = true;
                            p.StartInfo.Verb = "runas";
                            p.Start();
                            p.WaitForExit();
*/
                            /*var ramCleaner = Process.Start(Path.Combine(CurrntPath, "MultiplyMatrices.exe"));
                            ramCleaner.WaitForExit();*/
                            Console.WriteLine($"{(j == 1 ? "Последовательные" : "Параллельные")} вычисления для матрицы {i}x{i} завершены для {count} прогона из {Quantity}");
                        }
                    }
                }
                Console.WriteLine("Файлы посчётов готовы");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
