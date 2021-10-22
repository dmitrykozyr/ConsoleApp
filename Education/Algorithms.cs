using System;
using System.Collections.Generic;

namespace SharpEdu
{
    class Algorithms
    {
        public static void ShowCollectionElements(IEnumerable<int> collection)
        {
            string s = "";
            foreach (var i in collection)
                s += i + " ";

            Console.WriteLine(s);
        }

        #region Бинарный поиск O(log N)
        // На каждом шаге сравнивает искомый ключ со средним элементом в массиве,
        // а потом идет налево/направо и опять разбивает массив пополам
        public static void BinarySearch()
        {
            int[] array = new int[] { 1, 7, 8, 4, 6, 3 };
            Console.WriteLine(BS(array, 9));

            string BS(int[] inputArray, int key)
            {
                Console.WriteLine("Input array:");
                ShowCollectionElements(array);

                Array.Sort(inputArray);

                Console.WriteLine("Sorted array:");
                ShowCollectionElements(array);

                int min = 0;
                int max = inputArray.Length - 1;
                while (min <= max)
                {
                    int mid = (min + max) / 2;
                    if (key == inputArray[mid]) return $"Index of key {key} is {mid}";
                    else if (key < inputArray[mid]) max = mid - 1;
                    else min = mid + 1;
                }
                return $"Key {key} not found";
            }
        }
        #endregion

        static void Main_()
        {
            
        }
    }
}
