using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        private readonly byte[] _collection;
        public int Length => _collection.Length;
        public byte this[int index] => _collection[index];
        private int _hash;
        private bool _isCalculated;

        public ReadonlyBytes(params byte[] bytes)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            _collection = bytes;
        }

        public override bool Equals(object obj) => obj != null
            && GetType() == obj.GetType()
            && GetHashCode() == obj.GetHashCode();

        public override int GetHashCode()
        {
            unchecked
            {
                if (_isCalculated) return _hash;
                _hash = 2141412;
                int prime = 23212;
                foreach (var b in _collection)
                {
                    _hash ^= b;
                    _hash *= prime;
                }
                _isCalculated = true;
                return _hash;
            }
        }

        public override string ToString() => $"[{string.Join(", ", _collection)}]";

        public IEnumerator<byte> GetEnumerator() => ((IEnumerable<byte>)_collection).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}