using System.Collections.Generic;
using System.Collections;

namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            ArrayList al = new ArrayList();
            object num = 1;
            al.Add(num);
            al.Add("철수");
            al.Remove(num);



            MyDynamicArray<int> da1 = new MyDynamicArray<int>();
            da1.Add(4);
            da1.Add(5);
            da1.Add(7);
            da1.Add(9);
            da1.Add(4);
            if (da1.Remove(1))
                Console.WriteLine( "Removed 1");
            Console.WriteLine($"index of 4 is {da1.IndexOf(4)}");

            if (da1.FindIndex(x => x > 3) >= 0)
                Console.WriteLine("I found something bigger than 3 !");

            da1[0] = 5;

            MyDynamicArray<int> da2 =new MyDynamicArray<int>();
            foreach (var item in da1)
            {
                Console.WriteLine(item);
            }

            using (IEnumerator<int> e = da1.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    Console.WriteLine(e.Current);
                }
                e.Reset();
            }



            MyLinkedList<float> ll1 = new MyLinkedList<float>();
            ll1.AddFirst(5.0f);
            ll1.AddLast(3.0f);
            MyLinkedListNode<float> node = ll1.Find(x => x > 1.0f);
            ll1.AddBefore(node, 0.1f);
            if (ll1.RemoveLast(5.0f))
                Console.WriteLine("Removed last 5.0f");
        }
    }
}