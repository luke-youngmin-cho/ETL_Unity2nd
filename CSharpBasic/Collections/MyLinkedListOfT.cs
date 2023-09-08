using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class MyLinkedListNode<T>
    {
        public T Value;
        public MyLinkedListNode<T> Prev;
        public MyLinkedListNode<T> Next;

        public MyLinkedListNode(T value)
        {
            Value = value;
        }
    }

    internal class MyLinkedList<T> : IEnumerable<T>
    {
        public MyLinkedListNode<T> First => _first;
        public MyLinkedListNode<T> Last => _last;
        private MyLinkedListNode<T> _first, _last, _tmp;

        public void AddFirst(T value)
        {
            _tmp = new MyLinkedListNode<T>(value);

            // 하나이상의 노드가 존재한다면
            if (_first != null)
            {
                _tmp.Next = _first;
                _first.Prev = _tmp;
            }
            else
            {
                _last = _tmp;
            }

            _first = _tmp;
        }

        public void AddLast(T value)
        {
            _tmp = new MyLinkedListNode<T>(value);

            // 하나이상의 노드가 존재한다면
            if (_last != null)
            {
                _tmp.Prev = _last;
                _last.Next = _tmp;
            }
            else
            {
                _first = _tmp;
            }

            _last = _tmp;
        }

        public void AddBefore(MyLinkedListNode<T> node, T value)
        {
            _tmp = new MyLinkedListNode<T>(value);

            if (node.Prev != null)
            {
                node.Prev.Next = _tmp;
                _tmp.Prev = node.Prev;
            }
            else
            {
                _first = _tmp;
            }

            node.Prev = _tmp;
            _tmp.Next = node;
        }


        public void AddAfter(MyLinkedListNode<T> node, T value)
        {
            _tmp = new MyLinkedListNode<T>(value);

            if (node.Next != null)
            {
                node.Next.Prev = _tmp;
                _tmp.Next = node.Next;
            }
            else
            {
                _last = _tmp;
            }

            node.Next = _tmp;
            _tmp.Prev = node;
        }

        public MyLinkedListNode<T> Find(Predicate<T> match)
        {
            _tmp = _first;
            while (_tmp != null)
            {
                if (match(_tmp.Value))
                    return _tmp;

                _tmp = _tmp.Next;
            }
            return null;
        }

        public MyLinkedListNode<T> FindLast(Predicate<T> match)
        {
            _tmp = _last;
            while (_tmp != null)
            {
                if (match(_tmp.Value))
                    return _tmp;

                _tmp = _tmp.Prev;
            }
            return null;
        }

        public bool Remove(MyLinkedListNode<T> node)
        {
            if (node == null)
                return false;

            if (node.Prev != null)
            {
                node.Prev.Next = node.Next;
            }
            else
            {
                _first = node.Next;
            }

            if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
            }
            else
            {
                _last = node.Prev;
            }

            return true;
        }

        public bool Remove(T value)
        {
            return Remove(Find(x => Comparer<T>.Default.Compare(x, value) == 0));
        }

        public bool RemoveLast(T value)
        {
            return Remove(FindLast(x => Comparer<T>.Default.Compare(x, value) == 0));
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
            public T Current => _currentNode.Value;

            object IEnumerator.Current => _currentNode.Value;

            private MyLinkedList<T> _data;
            private MyLinkedListNode<T> _currentNode;

            public Enumerator(MyLinkedList<T> data)
            {
                _data = data;
                _currentNode = null;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                _currentNode = _currentNode == null ? _data.First : _currentNode.Next;

                return _currentNode != null;
            }

            public void Reset()
            {
                _currentNode = null;
            }
        }
    }
}
