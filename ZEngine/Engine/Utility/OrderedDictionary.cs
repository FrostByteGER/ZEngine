using System;
using System.Collections;
using System.Collections.Generic;

namespace ZEngine.Engine.Utility
{
    /// <summary>
    /// Represents a dictionary where the items are in an explicit and indexable order
    /// Source: https://github.com/codewitch-honey-crisis/OrderedDictionary
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in this dictionary</typeparam>
    /// <typeparam name="TValue">The type of the values in this dictionary</typeparam>
    public class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IList<KeyValuePair<TKey, TValue>>
    {
        private sealed class KeysCollection : ICollection<TKey>
        {
            private readonly OrderedDictionary<TKey, TValue> _outer;

            public int Count => _outer.Count;

            public KeysCollection(OrderedDictionary<TKey, TValue> outer)
            {
                _outer = outer;
            }

            
            public bool Contains(TKey key) => _outer.ContainsKey(key);
            public void CopyTo(TKey[] array, int index)
            {
                var count = _outer.Count;
                // check our parameters for validity
                if (null == array)
                    throw new ArgumentNullException(nameof(array));
                if (1 != array.Rank || 0 != array.GetLowerBound(0))
                    throw new ArgumentException("The array is not an SZArray", nameof(array));
                if (0 > index)
                    throw new ArgumentOutOfRangeException(nameof(index),
                          "The index cannot be less than zero.");
                if (array.Length <= index)
                    throw new ArgumentOutOfRangeException(nameof(index),
                          "The index cannot be greater than the length of the array.");
                if (count > array.Length + index)
                    throw new ArgumentException
                    ("The array is not big enough to hold the collection entries.", nameof(array));
                for (var i = 0; i < count; ++i)
                    array[i + index] = _outer._innerList[i].Key;
            }

            public IEnumerator<TKey> GetEnumerator() => new Enumerator(_outer);
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            void ICollection<TKey>.Add(TKey key) => throw new NotSupportedException("The collection is read only.");
            bool ICollection<TKey>.Remove(TKey key) => throw new NotSupportedException("The collection is read only.");
            void ICollection<TKey>.Clear() => throw new NotSupportedException("The collection is read only.");
            bool ICollection<TKey>.IsReadOnly => true;

            // this is the meat of our enumeration capabilities
            private struct Enumerator : IEnumerator<TKey>
            {
                private readonly IEnumerator<KeyValuePair<TKey, TValue>> _inner;
                public TKey Current => _inner.Current.Key;
                object IEnumerator.Current => Current;

                public Enumerator(OrderedDictionary<TKey, TValue> outer)
                {
                    _inner = outer.GetEnumerator();
                }

                public void Reset() => _inner.Reset();
                void IDisposable.Dispose() => _inner.Dispose();
                public bool MoveNext() => _inner.MoveNext();

            }
        }

        private sealed class ValuesCollection : ICollection<TValue>
        {
            private readonly OrderedDictionary<TKey, TValue> _outer;
            public int Count => _outer.Count;
            bool ICollection<TValue>.IsReadOnly => true;

            public ValuesCollection(OrderedDictionary<TKey, TValue> outer)
            {
                _outer = outer;
            }
            
            public bool Contains(TValue value)
            {
                for (int ic = Count, i = 0; i < ic; ++i)
                    if (Equals(_outer._innerList[i].Value, value))
                        return true;
                return false;
            }

            public void CopyTo(TValue[] array, int index)
            {
                var count = _outer.Count;
                // check our parameters for validity
                if (null == array)
                    throw new ArgumentNullException(nameof(array));
                if (1 != array.Rank || 0 != array.GetLowerBound(0))
                    throw new ArgumentException("The array is not an SZArray", nameof(array));
                if (0 > index)
                    throw new ArgumentOutOfRangeException(nameof(index),
                          "The index cannot be less than zero.");
                if (array.Length <= index)
                    throw new ArgumentOutOfRangeException(nameof(index),
                          "The index cannot be greater than the length of the array.");
                if (count > array.Length + index)
                    throw new ArgumentException
                    ("The array is not big enough to hold the collection entries.", nameof(array));
                for (var i = 0; i < count; ++i)
                    array[i + index] = _outer._innerList[i].Value;
            }
            public IEnumerator<TValue> GetEnumerator() => new Enumerator(_outer);
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            void ICollection<TValue>.Add(TValue value) => throw new NotSupportedException("The collection is read only.");
            bool ICollection<TValue>.Remove(TValue value) => throw new NotSupportedException("The collection is read only.");
            void ICollection<TValue>.Clear() => throw new NotSupportedException("The collection is read only.");

            // this is the meat of our enumeration capabilities
            private struct Enumerator : IEnumerator<TValue>
            {
                private readonly IEnumerator<KeyValuePair<TKey, TValue>> _inner;
                public TValue Current => _inner.Current.Value;
                object IEnumerator.Current => Current;

                public Enumerator(OrderedDictionary<TKey, TValue> outer)
                {
                    _inner = outer.GetEnumerator();
                }

                public void Reset() => _inner.Reset();
                void IDisposable.Dispose() => _inner.Dispose();
                public bool MoveNext() => _inner.MoveNext();

            }
        }

        // we keep these synced
        private readonly List<KeyValuePair<TKey, TValue>> _innerList;
        private readonly Dictionary<TKey, TValue> _innerDictionary;
        private readonly IEqualityComparer<TKey> _comparer = null;

        /// <summary>
        /// Returns the count of items in the dictionary
        /// </summary>
        public int Count => _innerList.Count;
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        /// <summary>
        /// Indicates the keys in this dictionary
        /// </summary>
        public ICollection<TKey> Keys => new KeysCollection(this);

        /// <summary>
        /// Indicates the value in this dictionary
        /// </summary>
        public ICollection<TValue> Values => new ValuesCollection(this);

        /// <summary>
        /// Creates an ordered dictionary with the specified capacity and comparer
        /// </summary>
        /// <param name="capacity">The initial capacity</param>
        /// <param name="comparer">The comparer</param>
        public OrderedDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            _innerDictionary = new Dictionary<TKey, TValue>(capacity, comparer);
            _innerList = new List<KeyValuePair<TKey, TValue>>(capacity);
            _comparer = comparer;
        }

        /// <summary>
        /// Creates an ordered dictionary with the specified items and the specified comparer
        /// </summary>
        /// <param name="collection">The collection or dictionary to copy from</param>
        /// <param name="comparer">The comparer to use</param>
        public OrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
        {
            _innerDictionary = new Dictionary<TKey, TValue>(comparer);
            _innerList = new List<KeyValuePair<TKey, TValue>>();
            AddValues(collection);
            _comparer = comparer;
        }

        /// <summary>
        /// Creates an ordered dictionary with the specified capacity
        /// </summary>
        /// <param name="capacity">The initial capacity</param>
        public OrderedDictionary(int capacity)
        {
            _innerDictionary = new Dictionary<TKey, TValue>(capacity);
            _innerList = new List<KeyValuePair<TKey, TValue>>(capacity);
        }

        /// <summary>
        /// Creates an ordered dictionary filled with the specified collection or dictionary
        /// </summary>
        /// <param name="collection">The collection or dictionary to copy</param>
        public OrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            _innerDictionary = new Dictionary<TKey, TValue>();
            _innerList = new List<KeyValuePair<TKey, TValue>>();
            AddValues(collection);
        }

        /// <summary>
        /// Creates an ordered dictionary with the specified comparer
        /// </summary>
        /// <param name="comparer">The equality comparer to use for the keys</param>
        public OrderedDictionary(IEqualityComparer<TKey> comparer)
        {
            _innerDictionary = new Dictionary<TKey, TValue>(comparer);
            _innerList = new List<KeyValuePair<TKey, TValue>>();
            _comparer = comparer;
        }

        /// <summary>
        /// Creates a default instance of the ordered dictionary
        /// </summary>
        public OrderedDictionary()
        {
            _innerDictionary = new Dictionary<TKey, TValue>();
            _innerList = new List<KeyValuePair<TKey, TValue>>();
        }

        /// <summary>
        /// Gets the value at the specified index
        /// </summary>
        /// <param name="index">The index of the value to retrieve</param>
        /// <returns>The value of the item at the specified index</returns>
        public TValue GetAt(int index)
        {
            return _innerList[index].Value;
        }

        /// <summary>
        /// Sets the value at the specified index
        /// </summary>
        /// <param name="index">The index of the value to set</param>
        /// <param name="value">The new value to assign</param>
        public void SetAt(int index, TValue value)
        {
            var key = _innerList[index].Key;
            _innerList[index] = new KeyValuePair<TKey, TValue>(key, value);
            _innerDictionary[key] = value;
        }

        /// <summary>
        /// Inserts an item into the ordered dictionary at the specified position
        /// </summary>
        /// <param name="index">The index to insert the item before</param>
        /// <param name="key">The key of the new item</param>
        /// <param name="value">The value of the new item</param>
        public void Insert(int index, TKey key, TValue value) => (this as IList<KeyValuePair<TKey, TValue>>).Insert(index, new KeyValuePair<TKey, TValue>(key, value));

        private void AddValues(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            foreach (var kvp in collection)
            {
                _innerDictionary.Add(kvp.Key, kvp.Value);
                _innerList.Add(kvp);
            }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            _innerDictionary.Add(item.Key, item.Value);
            _innerList.Add(item);
        }

        /// <summary>
        /// Clears all the items from the dictionary
        /// </summary>
        public void Clear()
        {
            _innerDictionary.Clear();
            _innerList.Clear();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            if (null == _comparer)
                return _innerList.Contains(item);
            for (int ic = _innerList.Count, i = 0; i < ic; ++i)
            {
                var kvp = _innerList[i];
                if (_comparer.Equals(item.Key, kvp.Key))
                    if (Equals(item.Value, kvp.Value))
                        return true;
            }
            return false;
        }

        /// <summary>
        /// Copies the items in the dictionary to the specified array, starting at the specified destination index
        /// </summary>
        /// <param name="array">The array to copy to</param>
        /// <param name="arrayIndex">The index into <paramref name="array"/> at which copying begins</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _innerList.CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            if ((_innerDictionary as ICollection<KeyValuePair<TKey, TValue>>).Remove(item))
                return _innerList.Remove(item); // should always return true
            return false;
        }

        /// <summary>
        /// Adds an item to the end of the dictionary
        /// </summary>
        /// <param name="key">The key to add</param>
        /// <param name="value">The value to add</param>
        public void Add(TKey key, TValue value) => (this as ICollection<KeyValuePair<TKey, TValue>>).Add(new KeyValuePair<TKey, TValue>(key, value));

        /// <summary>
        /// Indicates whether the specified key is contained in the dictionary
        /// </summary>
        /// <param name="key">The key to look for</param>
        /// <returns>True if the key is present in the dictionary, otherwise false</returns>
        public bool ContainsKey(TKey key) => _innerDictionary.ContainsKey(key);

        /// <summary>
        /// Removes an item from the dictionary
        /// </summary>
        /// <param name="key">The key of the item to remove</param>
        /// <returns>True if the item was removed, or false if not found</returns>
        public bool Remove(TKey key)
        {
            if (_innerDictionary.TryGetValue(key, out var value))
            {
                _innerDictionary.Remove(key);
                _innerList.Remove(new KeyValuePair<TKey, TValue>(key, value));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Attempts to retrieve the value for the specified key
        /// </summary>
        /// <param name="key">The key to look up</param>
        /// <param name="value">The value to return</param>
        /// <returns>True if the key is present, otherwise false</returns>
        public bool TryGetValue(TKey key, out TValue value) => _innerDictionary.TryGetValue(key, out value);

        /// <summary>
        /// Gets or sets the value at the specified key
        /// </summary>
        /// <param name="key">The key to look up</param>
        /// <returns>The value</returns>
        public TValue this[TKey key]
        {
            get => _innerDictionary[key];
            set
            {
                if (_innerDictionary.TryGetValue(key, out var v))
                {
                    // change an existing key
                    _innerDictionary[key] = value;
                    _innerList[_innerList.IndexOf(new KeyValuePair<TKey, TValue>(key, v))] = new KeyValuePair<TKey, TValue>(key, value);
                }
                else
                {
                    _innerDictionary.Add(key, value);
                    _innerList.Add(new KeyValuePair<TKey, TValue>(key, value));
                }
            }
        }

        /// <summary>
        /// Gets an enumerator for this dictionary
        /// </summary>
        /// <returns>A new enumerator suitable for iterating through the items in the dictionary in stored order</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _innerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Removes the item at the specified index
        /// </summary>
        /// <param name="index">The index of the item to remove</param>
        public void RemoveAt(int index)
        {
            var key = _innerList[index].Key;
            _innerDictionary.Remove(key);
            _innerList.RemoveAt(index);
        }

        int IList<KeyValuePair<TKey, TValue>>.IndexOf(KeyValuePair<TKey, TValue> item) => _innerList.IndexOf(item);

        void IList<KeyValuePair<TKey, TValue>>.Insert(int index, KeyValuePair<TKey, TValue> item)
        {
            _innerDictionary.Add(item.Key, item.Value);
            _innerList.Insert(index, item);
        }

        KeyValuePair<TKey, TValue> IList<KeyValuePair<TKey, TValue>>.this[int index]
        {
            get => _innerList[index];
            set => _innerList[index] = value;
        }
    }
}
