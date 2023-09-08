using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class MyDynamicArray<T> : IEnumerable<T>
    {
        public T this[int index]
        {
            get
            {
                if (index >= _count || index < 0)
                    throw new IndexOutOfRangeException();
                return _items[index];
            }
            set
            {
                if (index >= _count || index < 0)
                    throw new IndexOutOfRangeException();
                _items[index] = value;
            }
        }

        public int Count => this._count;
        public int Capacity => _items.Length;

        private const int DEFAULT_SIZE = 1;
        private T[] _items = new T[DEFAULT_SIZE];
        private int _count;

        public void Add(T item)
        {
            // 공간이 모자라다면
            if (_count >= _items.Length)
            {
                T[] tmp = new T[_count * 2];
                Array.Copy(_items, 0, tmp, 0, _count);
                _items = tmp;
            }

            _items[_count++] = item;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (Comparer<T>.Default.Compare(_items[i], item) == 0)
                    return i;
            }
            return -1;
        }

        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return _items[i];
            }
            return default;
        }

        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return i;
            }
            return -1;
        }

        public void RemoveAt(int index)
        {
            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<T>
        {
            public T Current => _data._items[_index]; // 현재 페이지의 내용

            object IEnumerator.Current => _data._items[_index];

            private MyDynamicArray<T> _data; // 책
            private int _index; // 현재 책 페이지

            public Enumerator(MyDynamicArray<T> data)
            {
                _data = data;
                _index = -1;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                _index++; // 책장 넘기기
                return _index < _data._count; // 책장 넘기는거 OK ? 
            }

            public void Reset()
            {
                _index = -1; // 책 덮어서 책표지로 돌아감
            }
        }
    }
}

public class A
{
    public int value => 3;
}

public class B : A
{
    new public int value => 5;
}


public class Test
{
    void DoSomething()
    {
        A a = new A();
        B b = new B();
        Console.WriteLine(a.value); // 3
        Console.WriteLine(b.value); // 5

        A a2 = b;
        Console.WriteLine(a2.value); // 3 
    }
}
