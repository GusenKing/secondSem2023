using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNF
{
    public class DNF
    {
        private List<Konj> konjs = new List<Konj>();

        public DNF() { }
        public DNF(string stringDNF)
        {
            foreach(var konj in stringDNF.ToUpper().Split('V'))
            {
                List<int> newVars = new List<int>();
                int nextSign = 1;
                for (int i = 0; i < konj.Length; i++)
                {
                    if (konj[i] == '-')
                        nextSign = -1;
                    if (char.IsDigit(konj[i]))
                    {
                        int varIndex = (int)char.GetNumericValue(konj[i]);
                        if (!(1 <= varIndex && varIndex <= 5))
                            throw new ArgumentException("Функция только от переменных x1,x2,x3,x4,x5");
                        newVars.Add(nextSign * varIndex);
                        nextSign = 1;
                    }
                }
                konjs.Add(new Konj(newVars.ToArray()));
            }
        }

        public void Insert(Konj k)
        {
            foreach (var e in konjs)
            {
                if (k.Equals(e)) return;
            }
            konjs.Add(k);
        }
        public DNF Disj(DNF other)
        {
            var newResult = new DNF();
            foreach (var k in konjs)
                newResult.konjs.Add(k);

            foreach (var newKonj in other.konjs)
            {
                bool unique = true;
                foreach (var curKonj in newResult.konjs)
                {
                    if (newKonj.Equals(curKonj))
                    {
                        unique = false;
                        break;
                    }
                }
                if (unique) newResult.konjs.Add(newKonj);
            }
            return newResult;
        }

        public bool Value(bool[] valueSet)
        {
            if (valueSet.Length != 5) throw new ArgumentException();
            bool result = false;
            foreach(var k in konjs)
            {
                result |= k.Value(valueSet);
            }
            return result;
        }

        public void SortByLength()
        {
            konjs.Sort(new KonjLengthComparer());
        }

        public DNF DnfWith(int i)
        {
            var newResult = new DNF();
            foreach (var k in konjs)
            {
                if (k.variables.Contains(i))
                    newResult.Insert(k);
            }
            return newResult;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < konjs.Count; i++)
            {
                sb.Append(konjs[i].ToString());
                if (i + 1 != konjs.Count) sb.Append("v");
            }
            return sb.ToString();
        }
    }
}
