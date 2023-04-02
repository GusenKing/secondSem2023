using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociativeArray
{
    internal class ArrayEntry<TKey, TValue>
    {
        internal TKey key;
        internal TValue value;

        public ArrayEntry(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }

    public class AssociatArray<TKey, TValue> where TKey : IEquatable<TKey>
    {
        private static readonly int SIZE = 128;
        private ArrayEntry<TKey, TValue>[] entries = new ArrayEntry<TKey, TValue>[SIZE];

        private int Hash(TKey key)
        {
            return Math.Abs(key.GetHashCode() % SIZE);
        }
        public TValue Get(TKey key)
        {
            int index = Hash(key);

            while (entries[index] != null)
            {
                if (key.Equals(entries[index].key))
                    break;
                index = (index + 1) % SIZE;
            }
            if (entries[index] == null) throw new KeyNotFoundException();
            return entries[index].value;
        }

        public void Set(TKey key, TValue value)
        {
            if (null == value)
            {
                Delete(key);
            }
            else
            {
                int index = Hash(key);
                while (null != entries[index])
                {
                    if (key.Equals(entries[index].key))
                    {
                        break;
                    }
                    index = (index + 1) % SIZE;
                }

                entries[index] = new ArrayEntry<TKey, TValue>(key, value);
            }
        }
        public void Delete(TKey key)
        {
            int index = Hash(key);

            while (null != entries[index])
            {
                if (key.Equals(entries[index].key))
                {
                    break;
                }
                else
                {
                    index = (index + 1) % SIZE;
                }
            }

            if (null == entries[index])
            {
                return;
            }

            entries[index] = null;
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (ArrayEntry<TKey, TValue> entry in entries)
            {
                if (entry != null)
                {
                    yield return new KeyValuePair<TKey, TValue>(entry.key, entry.value);
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach(var entry in entries)
            {
                if(entry != null)
                    sb.Append($"{entry.key}: {entry.value} \n");
            }
            return sb.ToString();
        }
    }
}
