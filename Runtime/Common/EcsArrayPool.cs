using System;
using System.Collections.Generic;

namespace ECS.Common
{
    public class EcsArrayPool<T>
    {
        public int Count { get; private set; }

        private T[] _items;
        private readonly Stack<int> _pool;

        public EcsArrayPool(int capacity)
        {
            _items = new T[capacity];
            _pool = new Stack<int>(capacity);
            Fill(0, capacity);
        }

        public int Take()
        {
            TryResize();
            var index = _pool.Pop();
            Count++;

            return index;
        }

        public void Drop(T item, int index)
        {
            _items[index] = item;
        }

        public ref T Find(int index)
        {
            return ref _items[index];
        }

        public void Free(int index)
        {
            _items[index] = default(T);
            _pool.Push(index);
            Count--;
        }

        public void Clear()
        {
            for (var i = 0; i < Count; i++)
            {
                _items[i] = default(T);
                _pool.Push(i);
            }

            Count = 0;
        }

        private void TryResize()
        {
            if (_pool.Count > 0)
            {
                return;
            }

            var oldCapacity = _items.Length;
            var newCapacity = oldCapacity * 2;
            Array.Resize(ref _items, newCapacity);
            Fill(oldCapacity, newCapacity);
        }

        private void Fill(int from, int to)
        {
            for (var i = to - 1; i >= from; i--)
            {
                _pool.Push(i);
            }
        }
    }
}
