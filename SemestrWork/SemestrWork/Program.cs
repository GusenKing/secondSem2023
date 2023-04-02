using SemestrWork;


SemestrWorkTools.BuildBenchmarkGraph((data) => QuickSortAlgorithm.QuickSort(data),
                            (size) => SemestrWorkTools.GenerateRandomData(size),
                            (0, 10000000, 50000),
                            3,
                            25,
                            true);

SemestrWorkTools.BuildBenchmarkGraph((data) => QuickSortAlgorithm.QuickSort(data),
                            (size) => SemestrWorkTools.GenerateSortedData(size),
                            (0, 10000000, 50000),
                            3,
                            25,
                            true);

SemestrWorkTools.BuildBenchmarkGraph((data) => QuickSortAlgorithm.QuickSort(data),
                            (size) => SemestrWorkTools.GenerateDataWithSameValue(size),
                            (0, 10000000, 50000),
                            3,
                            25,
                            true);

SemestrWorkTools.BuildBenchmarkGraph((data) => QuickSortAlgorithm.QuickSort(data),
                            (size) => SemestrWorkTools.GenerateRandomData(size),
                            (0, 10000000, 50000),
                            1,
                            25,
                            false);

SemestrWorkTools.BuildBenchmarkGraph((data) => QuickSortAlgorithm.QuickSort(data),
                            (size) => SemestrWorkTools.GenerateSortedData(size),
                            (0, 10000000, 50000),
                            1,
                            25,
                            false);

SemestrWorkTools.BuildBenchmarkGraph((data) => QuickSortAlgorithm.QuickSort(data),
                            (size) => SemestrWorkTools.GenerateDataWithSameValue(size),
                            (0, 10000000, 50000),
                            1,
                            25,
                            false);