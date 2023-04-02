using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson
{
    public class ArOperations
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }

        public int Min(int a, int b)
        {
            return a - b;
        }

        public int Prod(int a, int b)
        {
            return a * b;
        }

        public int Div(int a, int b)
        {
            return a / b;
        }

        public int Max(int a, int b)
        {
            return a > b ? a : b;
        }
    }
}
