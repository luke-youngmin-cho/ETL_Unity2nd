using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class AVLTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public T Value;
            public Node Left;
            public Node Right;
            public int Height;
        }

        private Node _root;


        public bool Contains(T value)
        {
            Node node = _root;
            while (node != null)
            {
                if (value.CompareTo(node.Value) < 0)
                    node = node.Left;
                else if (value.CompareTo(node.Value) > 0)
                    node = node.Right;
                else
                    return true;
            }
            return false;
        }

        public void Add(T value)
        {
            _root = Add(_root, value);
        }

        private Node Add(Node node, T value)
        {
            if (node == null)
            {
                return new Node { Value = value, Height = 1 };
            }

            int compare = value.CompareTo(node.Value);
            if (compare < 0)
            {
                node.Left = Add(node.Left, value);
            }
            else if (compare > 0)
            {
                node.Right = Add(node.Right, value);
            }

            // ?? Null 병합연산자 
            // null 이 아닐경우 왼쪽 값 반환, null 이면 오른쪽 값 반환
            node.Height = 1 + Math.Max(node?.Left?.Height ?? 0, node?.Right?.Height ?? 0);
            int balance = Balance(node);

            // 왼쪽으로 치우쳐져있으면
            if (balance > 1)
            {
                if (value.CompareTo(node.Left.Value) > 0)
                {
                    node.Left = RotateLeft(node.Left);
                }

                return RotateRight(node);
            }
            // 오른쪽으로 치우쳐져있으면
            else if (balance < -1)
            {
                if (value.CompareTo(node.Right.Value) < 0)
                {
                    node.Right = RotateRight(node.Right);
                }

                return RotateLeft(node);
            }

            return node;
        }

        public void Remove(T value)
        {
            _root = Remove(_root, value);
        }

        private Node Remove(Node node, T value)
        {
            if (node == null)
                return null;

            int compare = value.CompareTo(node.Value);
            if (compare < 0)
            {
                node.Left = Remove(node.Left, value);
            }
            else if (compare > 0)
            {
                node.Right = Remove(node.Right, value);
            }
            else
            {
                // 자식이 하나이던지 없던지 체크
                if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }
                else
                {
                    Node tmp = node.Right;
                    Node tmpParent = tmp;
                    while (tmp.Left != null)
                    {
                        tmp = tmp.Left;
                        tmpParent = tmp;
                    }
                    node.Value = tmp.Value;
                    tmpParent.Left = null;
                }
            }

            node.Height = 1 + Math.Max(node?.Left?.Height ?? 0, node?.Right?.Height ?? 0);

            int balance = Balance(node);

            // 왼쪽으로 치우쳐져있으면
            if (balance > 1)
            {
                if (value.CompareTo(node.Left.Value) > 0)
                {
                    node.Left = RotateLeft(node.Left);
                }

                return RotateRight(node);
            }
            // 오른쪽으로 치우쳐져있으면
            else if (balance < -1)
            {
                if (value.CompareTo(node.Right.Value) < 0)
                {
                    node.Right = RotateRight(node.Right);
                }

                return RotateLeft(node);
            }

            return node;
        }


        /// <summary>
        /// 기준노드 중심으로 어느쪽으로 자식노드들이 치우쳐져있는지 판단
        /// </summary>
        /// <param name="node"> 기준노드 </param>
        /// <returns> 왼쪽 : > 1 , 오른쪽 : < - 1  </returns>
        private int Balance(Node node)
        {
            return node != null ? ((node?.Left?.Height ?? 0) - (node?.Right?.Height ?? 0)) : 0;
        }

        private Node RotateLeft(Node node)
        {
            if (node == null || node.Right == null)
                return node;

            Node newRoot = node.Right;
            node.Right = newRoot.Left;
            newRoot.Left = node;

            // ? Null check 연산자 : 참조가 Null 이 아닐때만 멤버접근함
            // ?? Null 병합 연산자 : 왼쪽 피연산자가 Null 이 아닐경우 왼쪽 피연산자값을 반환, Null 일 경우 오른쪽 피연산자 값을 반환
            node.Height = 1 + Math.Max(node?.Left?.Height ?? 0, node?.Right?.Height ?? 0);
            newRoot.Height = 1 + Math.Max(newRoot?.Left.Height ?? 0, newRoot?.Right?.Height ?? 0);
            return newRoot;
        }

        private Node RotateRight(Node node)
        {
            if (node == null || node.Left == null)
                return node;

            Node newRoot = node.Left;
            node.Left = newRoot.Right;
            newRoot.Right = node;

            node.Height = 1 + Math.Max(node?.Left?.Height ?? 0, node?.Right?.Height ?? 0);
            newRoot.Height = 1 + Math.Max(newRoot?.Left?.Height ?? 0, newRoot?.Right?.Height ?? 0);
            return newRoot;
        }
    }
}