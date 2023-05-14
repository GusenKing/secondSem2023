using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTreeSemestrWork
{
    internal static class BenchmarkTools
    {
        public static int[] RandomNumbersGenerator(int n)
        {
            Random rnd = new Random();
            int[] result = new int[n];
            for(int i = 0; i < n; i++)
                result[i] = rnd.Next();
            return result;
        }

        public static double Benchmark(Action method, bool timeBenchmark = true)
        {
            if (!timeBenchmark)
            {
                method();
                return RedBlackTree<int>.OperationsCount;
            }
                GC.TryStartNoGCRegion(200000000);

            var stopwatch = Stopwatch.StartNew();
            stopwatch.Restart();
            method();
            stopwatch.Stop();

            if (GCSettings.LatencyMode == GCLatencyMode.NoGCRegion)
                GC.EndNoGCRegion();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static Dictionary<string, double> GetAverageTimesAndOperations(int runsAmount)
        {
            Dictionary<string, double> results = new Dictionary<string, double>();
            double totalInsertionTime = 0, totalSearchTime = 0, totalDeletionTime = 0, 
                   totalInsertionOps = 0, totalSearchOps = 0, totalDeletionOps = 0;
            for (int i = 0; i <= runsAmount; i++)
            {
                RedBlackTree<int> tree = new RedBlackTree<int>();
                var dataToInsert = RandomNumbersGenerator(10000);
                foreach (var item in dataToInsert)
                {
                    totalInsertionTime += Benchmark(() => tree.Insert(item));
                }
                totalInsertionOps += RedBlackTree<int>.OperationsCount;

                var rnd = new Random();
                var dataToSearch = dataToInsert.OrderBy(x => rnd.Next()).Take(100);
                RedBlackTree<int>.OperationsCount = 0;
                foreach (var item in dataToSearch)
                {
                    totalSearchTime += Benchmark(() => tree.Search(item));
                }
                totalSearchOps += RedBlackTree<int>.OperationsCount;

                var dataToDelete = dataToInsert.OrderBy(x => rnd.Next()).Take(1000);
                RedBlackTree<int>.OperationsCount = 0;
                foreach (var item in dataToDelete)
                {
                    totalDeletionTime += Benchmark(() => tree.Delete(item));
                }
                totalDeletionOps += RedBlackTree<int>.OperationsCount;

            }
            results.Add("Среднее время вставки 10000 элементов", totalInsertionTime / runsAmount);
            results.Add("Среденее количество операций для вставки 10000 элементов", totalInsertionOps / runsAmount);
            results.Add("Среднее время поиска 100 элементов", totalSearchTime / runsAmount);
            results.Add("Среднее количество операций для поиска 100 элементов", totalSearchOps / runsAmount);
            results.Add("Среднее время удаления 1000 элементов", totalDeletionTime / runsAmount);
            results.Add("Среденее количество операций для удаления 1000 элементов", totalDeletionOps / runsAmount);

            return results;
        }
    }
}
