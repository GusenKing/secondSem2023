using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace hashes
{
	// TODO: Создайте класс ReadonlyBytes
	public class ReadonlyBytes : IEnumerable<byte>
	{
		readonly byte[] bytesArray;
        private int hashValue;
		public int Length { get { return bytesArray.Length; } }
		public ReadonlyBytes(params byte[] array)
		{
			if(array == null) throw new ArgumentNullException();
            this.bytesArray = array;
		}

        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException();
                return bytesArray[index];
            }
            //set
            //{
            //    if (index < 0 || index >= Length) throw new IndexOutOfRangeException();
            //    bytesArray[index] = value;
            //}
        }

        public override string ToString()
        {
            if (Length == 0) return "[]";
            var sb = new StringBuilder();
            sb.Append('[');
            for(int i = 0; i < Length - 1; i++) 
            {
                sb.Append($"{bytesArray[i]}, ");
            }
            sb.Append($"{bytesArray[Length - 1]}]");
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if(obj == null || this.GetType() != obj.GetType()) return false;
            ReadonlyBytes other = obj as ReadonlyBytes;
            if(other.Length != Length) return false;
            for(int i = 0; i < Length; i++)
            {
                if (bytesArray[i] != other[i])
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            if (hashValue != 0) return hashValue;
            unchecked
            {
                int prime = 16777619;
                int hash = 0;
                foreach(var b in bytesArray)
                {
                    hash = hash * prime + b;
                }
                hashValue = hash;
                return hash;
            }
        }

        public IEnumerator<byte> GetEnumerator()
        {
            return new ReadonlyBytesEnum(bytesArray);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }

    public class ReadonlyBytesEnum : IEnumerator<byte>
    {
        private readonly byte[] _array;
        int position = -1;
        public ReadonlyBytesEnum(byte[] array)
        {
            _array = array;
        }
        public byte Current
        {
            get { return _array[position]; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }


        public void Dispose() { }

        public bool MoveNext()
        {
            position++;
            return position < _array.Length;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}