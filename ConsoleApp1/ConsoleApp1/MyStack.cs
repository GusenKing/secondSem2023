using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MyStack<T> where T: IComparable
    {
        private int top = -1;
        private T[] arr;
        private T? max;

        public MyStack(int maxSize)
        {
            arr = new T[maxSize];
        }

        public void Push(T item)
        {
            if (top >= arr.Length) throw new IndexOutOfRangeException("Стек переполнен");
            if (item.CompareTo(max) > 0) max = item;
            arr[++top] = item;
        }

        public T Pop()
        {
            if (top == -1) throw new IndexOutOfRangeException("Стек пуст");
            return arr[top--];
        }

        public T GetMax()
        {
            return top == -1 ? throw new IndexOutOfRangeException("Stack is empty") : max;
        }
        public bool IsEmpty()
        {
            return top == -1;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for(int i = 0; i <= top; i++)
                sb.Append($"{arr[i]}; ");
            return sb.ToString();
        }
    }
}
