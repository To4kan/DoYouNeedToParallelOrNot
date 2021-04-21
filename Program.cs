using System;
using System.Collections.Generic;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace MatrixResultsCollector
{
    class Program
    {
        static string path = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        static void Main(string[] args)
        {
            Console.WriteLine("Введите начальную размерность матрицы в десятках");
            int starti = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите конечную размерность матрицы в десятках");
            int stopi = Convert.ToInt32(Console.ReadLine());
            var excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Add();
            #region Заполнить первый лист данными по последовательным вычислениям
            Excel._Worksheet workSheetSequential = (Excel.Worksheet)excelApp.ActiveSheet;
            workSheetSequential.Name = "Последовательные вычисления";
            workSheetSequential.Cells[1, 1]= "Размер матрицы NxN";
            for (int j = 1; j <= 20; j++)
            {
                workSheetSequential.Cells[j+1 , 1] = $"Время вычисления {j} прогона";
            }
            workSheetSequential.Cells[22, 1] = "Среднее значение прогонов";
            ((Excel.Range)workSheetSequential.Columns[1]).AutoFit();
            List<double> workSheetSequentialSum = new List<double>() { };
            int ii = starti*10;
            for (int i = 1; i <= stopi-starti + 1; i++)
            {
                workSheetSequential.Cells[1, i+1] = ii;
                double sum = 0.0;
                for (int j = 1; j <= 20; j++)
                {
                    double filedata = Convert.ToDouble(File.ReadAllText(Path.Combine(path, ii.ToString() + "_" + "1" + "_" + j.ToString() + ".txt")));
                    workSheetSequential.Cells[j + 1, i+1] = filedata;
                    sum += filedata;
                }
                workSheetSequentialSum.Add(sum / 20);
                workSheetSequential.Cells[22, i+1] = sum/20;
                ((Excel.Range)workSheetSequential.Columns[i]).AutoFit();
                ii += 10;
            }
            #endregion
            #region Заполнить первый лист данными по последовательным вычислениям
            Excel._Worksheet workSheetParallel = (Excel.Worksheet)excelApp.Worksheets.Add();
            workSheetParallel.Name = "Паралельные вычисления";
            workSheetParallel.Cells[1, 1] = "Размер матрицы NxN";
            for (int j = 1; j <= 20; j++)
            {
                workSheetParallel.Cells[j+1, 1] = $"Время вычисления {j} прогона";
            }
            workSheetParallel.Cells[22, 1] = "Среднее значение прогонов";
            ((Excel.Range)workSheetParallel.Columns[1]).AutoFit();
            List<double> workSheetParallelSum = new List<double>() { };
            ii = starti*10;
            for (int i = 1; i <= stopi - starti+1; i++)
            {
                workSheetParallel.Cells[1, i+1] = ii;
                double sum = 0.0;
                for (int j = 1; j <= 20; j++)
                {
                    double filedata = Convert.ToDouble(File.ReadAllText(Path.Combine(path, ii.ToString() + "_" + "2" + "_" + j.ToString() + ".txt")));
                    workSheetParallel.Cells[j + 1, i + 1] = filedata;
                    sum += filedata;
                }
                workSheetParallelSum.Add(sum / 20);
                workSheetParallel.Cells[22, i+1] = sum / 20;
                ((Excel.Range)workSheetParallel.Columns[i]).AutoFit();
                ii += 10;
            }
            #endregion
            workSheetParallel.Range["A1:CW1"].Copy();
            Excel._Worksheet workSheetResults = (Excel.Worksheet)excelApp.Worksheets.Add();
            workSheetResults.Name = "Результаты";
            workSheetResults.Paste();
            workSheetResults.Cells[2, 1] = "Результаты последовательных вычислений";
            workSheetResults.Cells[3, 1] = "Результаты паралельных вычислений";
            ((Excel.Range)workSheetResults.Columns[1]).AutoFit();
            for (int i = 1; i <= stopi - starti + 1; i++)
            {
                workSheetResults.Cells[2, i+1] = workSheetSequentialSum[i-1];
                workSheetResults.Cells[3, i+1] = workSheetParallelSum[i-1];
                ((Excel.Range)workSheetResults.Columns[i+1]).AutoFit();
            }
        }
    }
}
