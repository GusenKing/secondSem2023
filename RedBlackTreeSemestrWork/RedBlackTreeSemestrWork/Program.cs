using RedBlackTreeSemestrWork;


var avgs = BenchmarkTools.GetAverageTimesAndOperations(1000);
foreach(var key in avgs.Keys)
{
    Console.WriteLine($"{key} - {avgs[key]}");
}

