using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNF
{
    public class Konj : IEquatable<Konj>
    {
        internal List<int> variables = new List<int>();

        public Konj(params int[] vars)
        {
            if (vars.Length > 5)
                throw new ArgumentException("Коньюнкция может состоять максимум из 5 переменных");
            foreach(var e in vars)
                variables.Add(e);
        }

        public bool Value(bool[] valueSet)
        {
            bool result = true;
            foreach(var e in variables)
            {
                if (e < 0) result &= !valueSet[Math.Abs(e) - 1];
                else result &= valueSet[e - 1];
            }
            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for(int i = 0; i < variables.Count;i++)
            {
                if (variables[i] < 0) sb.Append("-");
                sb.Append($"X{Math.Abs(variables[i])}");
                if (i + 1 != variables.Count) sb.Append("&");
            }
            return sb.ToString();
        }

        public bool Equals(Konj? other)
        {
            if (variables.Count != other.variables.Count) return false;
            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i] != other.variables[i]) return false;
            }
            return true;
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as Konj);
        }

        public override int GetHashCode()
        {
            return variables.GetHashCode();
        }
    }

    public class KonjLengthComparer : IComparer<Konj>
    {
        public int Compare(Konj? x, Konj? y)
        {
            return x.variables.Count.CompareTo(y.variables.Count);
        }
    }
}