using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MyQueue<T>
    {
        private int begin = -1;
        private int end = 0;
        private T[] arr;

        public MyQueue(int maxSize) 
        {   
            arr = new T[maxSize];
        }

        public void Enqueue(T item)
        {
            if((end == arr.Length && begin == 0) || begin == end) throw new IndexOutOfRangeException("Queue is full");
            if (begin == -1) begin = 0;
            arr[end++ % arr.Length] = item;
        }

        public T Dequeue()
        {
            if (begin == end) throw new IndexOutOfRangeException("Queue is empty");
            return arr[begin++ % arr.Length];
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = begin % arr.Length; i < end % arr.Length; i++)
                sb.Append($"{arr[i]}; ");
            //sb.Append($"begin - {begin}, end - {end % arr.Length}");
            return sb.ToString();
        }
    }
}
