using System.Diagnostics;
using System.Linq;

namespace SortAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] arr = Enumerable.Repeat(0, 10000000)
                                  .Select(x => random.Next(0, 1000000000))
                                  .ToArray();
             
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //ArraySort.BubbleSort(arr); // 10만개 32초
            //ArraySort.SelectionSort(arr); // 10만개 11초
            //ArraySort.InsertionSort(arr); // 10만개 6.5초
            //ArraySort.MergeSort(arr); // 10만개 0.023초 // 1000만개 2.6초 , 중복없을시 2.6초
            //ArraySort.QuickSort(arr); // 10만개 0.016초 // 1000만개 3.6초 , 중복없을시 1.6초
            //ArraySort.RecursiveQuickSort(arr);
            ArraySort.HeapSort(arr);

            // 요소의 중복이 많으면 MergeSort 가 QuickSort 보다 더 성능이 좋을 수 있다.
            // 재귀형태로 구현된 정렬은 함수 오버헤드 및 공간복잡으로 인해 성능이 약간 더 안좋을 수 있다.

            stopwatch.Stop();
            Console.WriteLine($"소요시간 : {stopwatch.ElapsedMilliseconds}");

            List<int> list = new List<int>();
            list.Sort();
        }
    }
}