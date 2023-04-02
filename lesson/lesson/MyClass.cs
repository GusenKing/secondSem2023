using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson
{
    public class MyClass
    {
        public int size;
        public int[] mas;

        public MyClass(int n) 
        {
            Random r = new Random();
            size = n;
            mas = new int[size];
            for(int i =0; i < size; i++)
            {
                mas[i] = r.Next(-100, 100);
            }
        }

        public int SomeMethod(Func<int, int, int> d)
        {
            if (size == 1)
                return mas[0];
            int result = mas[0];

            for(int i = 1; i < size; i++)
                result = d(result, mas[i]);

            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach(var e in mas)
            {
                sb.Append($"{e} ");
            }
            return sb.ToString();
        }
    }
}
