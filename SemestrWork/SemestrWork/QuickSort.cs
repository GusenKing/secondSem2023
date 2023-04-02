using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemestrWork
{
    public static class QuickSortAlgorithm
    {
        public static long IterationsCount { get; private set; }

        private static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[(low + high) / 2]; // => опорный элемент в середине массива
            int i = low - 1, j = high + 1;     // => крайние указатели 

            while (true)
            {
                IterationsCount++;             // счётчик подсчёта итераций
                do                             // (для измерения показателей алгоритма)
                {
                    i++;                       // => идём слева по массиву, 
                } while (arr[i] < pivot);      //    ищем элемент, который больше опорного

                do
                {
                    j--;                       // => аналогично справа,
                } while (arr[j] > pivot);      //    ищем элемент, меньше опорного

                if (i >= j)                    // => левый указатель пересёк правый, 
                    return j;                  //    значит нашлось нужное раздление
                
                int temp = arr[i];             
                arr[i] = arr[j];               // => иначе меняем местами найденные ранее элементы,
                arr[j] = temp;                 // и переходим к очередной итерации цикла
            }
        }

        public static void QuickSort(int[] arr)
        {
            IterationsCount = 0;
            QuickSort(arr, 0, arr.Length - 1);
        }

        private static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)                          // => Условие выхода, на массивах длинной 1 и 0 low < high даст false
            {
                int p = Partition(arr, low, high);   // => Разделение массива, возвращает индекс элемента, 
                                                     //    вокруг которого произошло разделение

                QuickSort(arr, low, p);              // => Рекурсивно повторяем к левой
                QuickSort(arr, p + 1, high);         // => и к правой части раздления
            }
        }
    }
}


