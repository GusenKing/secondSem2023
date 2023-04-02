using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Net.Mime.MediaTypeNames;

namespace SemestrWork
{
    public static class SemestrWorkTools
    {
        public static int[] GenerateRandomData(int size, int minV = 0, int maxV = int.MaxValue)
        {
            int[] data = new int[size];
            Random rnd = new Random();
            for(int i = 0; i < size; i++) 
            {
                data[i] = rnd.Next(minV, maxV);
            }
            return data;
        }

        public static int[] GenerateSortedData(int size)
        {
            int[] data = new int[size];
            Random rnd = new Random();
            for(int i = 0; i < size; i++)
            {
                data[i] = rnd.Next(i * 10, (i + 1) * 10);
            }
            return data; 
        }
        
        public static int[] GenerateInputSizes(int start, int end, int step)
        {
            var arr = new int[(end - start) / step + 1];
            arr[0] = 1;
            arr[1] = step;
            for(int i = 2; i < arr.Length; i++)
            {
                arr[i] = arr[i - 1] + step;
            }
            return arr;
        }

        public static int[] GenerateDataWithSameValue(int size)
        {
            int[] arr = new int[size];
            Random rnd = new Random();
            int v = rnd.Next();
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = v;
            }
            return arr;
        }

        public static double Benchmark(Action method, int numRuns, bool timeBenchmark = true)
        {
            method();
            if (!timeBenchmark) return QuickSortAlgorithm.IterationsCount;
            GC.TryStartNoGCRegion(200000000);

            var stopwatch = Stopwatch.StartNew();
            stopwatch.Restart();
            for (int i = 0; i < numRuns; i++)
            {
                method();
            }
            stopwatch.Stop();

            if (GCSettings.LatencyMode == GCLatencyMode.NoGCRegion)
                GC.EndNoGCRegion();
            return stopwatch.Elapsed.TotalMilliseconds / numRuns;
        }

        private static Chart SetupChart(int maxX, bool timeBenchmark)
        {
            Chart chart = new Chart();
            chart.ChartAreas.Add(new ChartArea("default"));

            Series series = chart.Series.Add("Time Complexity");

            series.ChartType = SeriesChartType.Line;
            series.Color = Color.Green;
            series.BorderWidth = 3;

            chart.ChartAreas[0].AxisX.Title = "Input Size";
            chart.ChartAreas[0].AxisY.Title = timeBenchmark ? "Time (ms)" : "Iterations";

            chart.ChartAreas[0].AxisX.Interval = maxX / 10;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            if (timeBenchmark) chart.ChartAreas[0].AxisY.Maximum = 250;
            chart.ChartAreas[0].AxisY.Maximum = 120000000;
            return chart;
        }
        public static void BuildBenchmarkGraph(Action<int[]> algorithm,
                                      Func<int, int[]> dataGeneratingMethod,
                                      (int, int, int) inputSizeAndStep,
                                      int testRunsAmount,
                                      int smoothingWindow,
                                      bool timeBenchmark)
        {
            int[] inputSizes = GenerateInputSizes(inputSizeAndStep.Item1,
                                                  inputSizeAndStep.Item2,
                                                  inputSizeAndStep.Item3);


            var chart = SetupChart(inputSizeAndStep.Item2, timeBenchmark);
            var series = chart.Series[0];

            var windowBuffer = new Queue<double>();
            double sum = 0;
            foreach (int inputSize in inputSizes)
            {
                int[] data = dataGeneratingMethod(inputSize);
                var elapsed = Benchmark(() => algorithm(data), testRunsAmount, timeBenchmark);

                windowBuffer.Enqueue(elapsed);
                sum += elapsed;
                if(windowBuffer.Count > smoothingWindow)
                    sum -= windowBuffer.Dequeue();

                if (inputSize % (inputSizeAndStep.Item2 / 10) == 0) Console.WriteLine($"{inputSize}: {sum / windowBuffer.Count}");
                series.Points.AddXY(inputSize, sum / windowBuffer.Count);
            }

            chart.Dock = DockStyle.Fill;
            Form form = new Form();
            form.Controls.Add(chart);
            System.Windows.Forms.Application.Run(form);
        }
    }
}
