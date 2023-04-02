using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.
using static System.Net.Mime.MediaTypeNames;

namespace SemestrWorkWinTools
{
    public static class SemestrWorkTools
    {
        public static int[] GenerateRandomData(int size)
        {
            int[] data = new int[size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                data[i] = rnd.Next();
            }
            return data;
        }

        public static TimeSpan Benchmark(Action method, int numRuns)
        {
            // Disable the garbage collector
            method();
            GC.TryStartNoGCRegion(200000000);

            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < numRuns; i++)
            {
                method();
            }
            stopwatch.Stop();

            // Re-enable the garbage collector
            if (GCSettings.LatencyMode == GCLatencyMode.NoGCRegion)
                GC.EndNoGCRegion();
            return TimeSpan.FromTicks(stopwatch.Elapsed.Ticks / numRuns);
        }

        public static void BuildGraph()
        {
            // Define the algorithm to test
            Func<int[], int> algorithm = (arr) =>
            {
                // Do something with the input array
                // Return the result
                return arr.Length;
            };

            // Define the input sizes to test
            int[] inputSizes = { 100, 500, 1000, 2000, 5000, 10000 };

            // Create a new chart
            Chart chart = new Chart();
            chart.ChartAreas.Add(new ChartArea("default"));

            // Add a new series to the chart
            Series series = chart.Series.Add("Time Complexity");

            // Set the chart type to a line graph
            series.ChartType = SeriesChartType.Line;

            // Set the X axis title
            chart.ChartAreas[0].AxisX.Title = "Input Size";

            // Set the Y axis title
            chart.ChartAreas[0].AxisY.Title = "Time (ms)";

            // Test the algorithm for each input size
            foreach (int inputSize in inputSizes)
            {
                // Generate input data
                int[] data = GenerateData(inputSize);

                // Measure the execution time of the algorithm
                long startTime = DateTime.Now.Ticks;
                algorithm(data);
                long endTime = DateTime.Now.Ticks;

                // Compute the elapsed time in milliseconds
                double elapsedTime = (endTime - startTime) / (double)TimeSpan.TicksPerMillisecond;

                // Add a data point to the chart
                series.Points.AddXY(inputSize, elapsedTime);
            }

            // Display the chart
            chart.Dock = DockStyle.Fill;
            Form form = new Form();
            form.Controls.Add(chart);
            Application.Run(form);
        }
    }
}

