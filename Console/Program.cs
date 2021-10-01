using System;
using System.Collections.Generic;
using System.Linq;

namespace Algoritms
{
    class Program
    {
        static void Main_()
        {
            //Empty();
            //IsValid();
            //RemoveDuplicates();
            //SearchInsert();
            //SingleNumber();
            //IsPalindrome();
            //Merge();
            //ContainsDuplicate();
            //MissingNumber();
            //CountPrimes();
            //GetConcatenation();     // 1929. Concatenation of Array
            IsHappy();              // 202. Happy Number
        }

        public static void IsValid()
        {
            Console.WriteLine(IsValid("()"));       // true
            Console.WriteLine(IsValid("()[]{}"));   // true
            Console.WriteLine(IsValid("(]"));       // false
            Console.WriteLine(IsValid("([)]"));     // false
            Console.WriteLine(IsValid("{[]}"));     // true
            Console.WriteLine(IsValid("[[["));      // false
            Console.WriteLine(IsValid("]]]{{{"));   // false
            Console.WriteLine(IsValid("]]]"));      // false

            bool IsValid(string s)
            {
                Stack<char> stck = new Stack<char>();
                foreach (var i in s)
                {
                    if (i == '(' || i == '[' || i == '{')
                        stck.Push(i);
                    else if (stck.Count > 0 && ((i == ')' && stck.Peek() == '(') ||
                            (i == '}' && stck.Peek() == '{') ||
                            (i == ']' && stck.Peek() == '[')))
                    {
                        if (stck.Count == 0)
                            return false;
                        else
                            stck.Pop();
                    }
                    else return false;
                }

                if (stck.Count != 0)
                    return false;

                return true;
            }
        }

        public static void RemoveDuplicates()
        {
            int[] arr = new int[] { 1, 2, 2 };
            Console.WriteLine(RemoveDuplicates(arr));
            int[] arr2 = new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 };
            Console.WriteLine(RemoveDuplicates(arr2));

            int RemoveDuplicates(int[] nums)
            {
                const int valueToDelete = 10;
                int currentNumber = valueToDelete;
                for (int i = 0; i < nums.Length; i++)
                {
                    if (currentNumber == nums[i])
                        nums[i] = valueToDelete;
                    else
                        currentNumber = nums[i];
                }

                return nums.Where(z => z != valueToDelete).Count();
            }
        }

        public static void SearchInsert()
        {
            Console.WriteLine(SearchInsert(new int[] { 1, 3, 5, 6 }, 5));
            Console.WriteLine(SearchInsert(new int[] { 1, 3, 5, 6 }, 2));
            Console.WriteLine(SearchInsert(new int[] { 1, 3, 5, 6 }, 7));
            Console.WriteLine(SearchInsert(new int[] { 1, 3, 5, 6 }, 0));
            Console.WriteLine(SearchInsert(new int[] { 1 }, 0));
            Console.WriteLine(SearchInsert(new int[] { 1, 3, 5 }, 4));

            int SearchInsert(int[] nums, int target)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (target == 0)
                        return 0;

                    if (nums[i] >= target)
                        return i;
                    else if (nums[nums.Length - 1] < target)
                        return nums.Length;
                }

                return 0;
            }
        }

        public static void SingleNumber()
        {
            Console.WriteLine(SingleNumber(new int[] { 2, 2, 1 }));         // 1
            Console.WriteLine(SingleNumber(new int[] { 4, 1, 2, 1, 2 }));   // 4
            Console.WriteLine(SingleNumber(new int[] { 1 }));               // 1
            Console.WriteLine(SingleNumber(new int[] { 1, 0, 1 }));         // 0

            int SingleNumber(int[] nums)
            {
                List<int> list = new List<int>();
                for (int i = 0; i < nums.Length; i++)
                {
                    if (!list.Any(z => z == nums[i]))
                        list.Add(nums[i]);
                    else
                    {
                        list.RemoveAt(list.IndexOf(nums[i]));
                    }
                }

                return list.FirstOrDefault();
            }
        }

        public static void IsPalindrome()
        {
            Console.WriteLine(IsPalindrome("A man, a plan, a canal: Panama")); // true
            Console.WriteLine(IsPalindrome("race a car"));                     // false

            bool IsPalindrome(string s)
            {
                var stringWithoutSpaces = new List<string>();
                foreach (var i in s)
                {
                    if (Char.IsLetterOrDigit(i))
                        stringWithoutSpaces.Add(i.ToString().ToLower());
                }

                for (int i = 0; i < stringWithoutSpaces.Count; i++)
                    if (stringWithoutSpaces[i] != stringWithoutSpaces[stringWithoutSpaces.Count - i - 1])
                        return false;

                return true;
            }
        }

        //-
        public static void Merge()
        {
            Merge(new int[] { 1, 2, 3, 0, 0, 0 }, 3, new int[] { 2, 5, 6 }, 3); //+ [1,2,2,3,5,6]
            Console.WriteLine();
            //Merge(new int[] { 1 }, 1, new int[] { }, 0);                      //- [1]
            Console.WriteLine();
            //Merge(new int[] { 0 }, 0, new int[] { 1 }, 1);                    //- [1]

            void Merge(int[] nums1, int m, int[] nums2, int n)
            {
                for (int i = 0; i < m; i++)
                    nums1[m + i] = nums2[i];

                Array.Sort(nums1);

                foreach (var i in nums1)
                    Console.Write(i + " ");
            }
        }

        public static void ContainsDuplicate()
        {
            Console.WriteLine(ContainsDuplicate(new int[] { 1, 2, 3, 1 })); // true
            Console.WriteLine(ContainsDuplicate(new int[] { 1, 2, 3, 4 })); // false
            Console.WriteLine(ContainsDuplicate(new int[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 })); // true

            bool ContainsDuplicate(int[] nums)
            {
                Array.Sort(nums);
                for (int i = 0; i < nums.Length - 1; i++)
                    if (nums[i] == nums[i + 1])
                        return true;
                return false;
            }
        }

        // Здесь работает, а не сайте - нет
        public static void MissingNumber()
        {
            Console.WriteLine(MissingNumber(new int[] { 3, 0, 1 }));    // 2
            Console.WriteLine();
            Console.WriteLine(MissingNumber(new int[] { 0, 1 }));       // 2
            Console.WriteLine();
            Console.WriteLine(MissingNumber(new int[] { 9, 6, 4, 2, 3, 5, 7, 0, 1 }));  // 8
            Console.WriteLine();
            Console.WriteLine(MissingNumber(new int[] { 1 }));          // 0
            Console.WriteLine();
            Console.WriteLine(MissingNumber(new int[] { 0 }));          // 1

            int MissingNumber(int[] nums)
            {
                if (nums.Length == 1 && nums[0] == 0)
                    return 1;
                if (nums.Length <= 1)
                    return 0;

                Array.Sort(nums);
                for (int i = 0; i < nums.Length - 1; i++)
                {
                    var tmp = ++nums[i];
                    if (tmp != nums[i + 1])
                        return nums[i]++;
                }

                return nums.Length;
            }
        }

        //-
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x)
            {
                val = x;
                next = null;
            }
        }
        public static void HasCycle()
        {
            var ll = new LinkedList<ListNode>();
            ll.AddLast(new ListNode(3));
            ll.AddLast(new ListNode(2));
            ll.AddLast(new ListNode(0));
            ll.AddLast(new ListNode(-4));

            bool HasCycle(ListNode head)
            {
                var indexes = new List<int>();


                return false;
            }
        }

        //!
        public static void CountPrimes()
        {
            Console.WriteLine(CountPrimes(10));    // 4
            Console.WriteLine(CountPrimes(0));     // 0
            Console.WriteLine(CountPrimes(1));     // 0

            int CountPrimes(int n)
            {
                var primes = new List<int>();
                var arr = new int[n];
                for (int i = 0; i < n; i++)
                    arr[i] = i;

                for (int i = 0; i < n; i++)
                {
                    // 2 3 5 7
                    if (i > 1/* &&   */)
                        primes.Add(i);
                }

                return primes.Count();
            }
        }

        public static void GetConcatenation()
        {
            GetConcatenation(new int[] { 1, 2, 1 });    // [1,2,1,1,2,1]
            Console.WriteLine();
            GetConcatenation(new int[] { 1, 3, 2, 1 }); // [1,3,2,1,1,3,2,1]

            int[] GetConcatenation(int[] nums)
            {
                int length = nums.Length;
                int[] ans = new int[2 * length];
                for (int i = 0; i < length; i++)
                {
                    ans[i] = nums[i];
                    ans[i + length] = nums[i];
                }
                return ans;
            }
        }

        //-
        private const int MAX_RECURSIVE_CALLS = 10;
        static int ctr = 0;
        public static void IsHappy()
        {
            Console.WriteLine(IsHappy(19)); // true
            //Console.WriteLine(IsHappy(2));  // false
            //Console.WriteLine(IsHappy(18)); // false
            //Console.WriteLine(IsHappy(23)); // true
            //Console.WriteLine(IsHappy(1));  // true
            //Console.WriteLine(IsHappy(7));  // true

            bool IsHappy(int n)
            {
                ctr++;
                if (ctr >= MAX_RECURSIVE_CALLS)
                {
                    ctr = 0;
                    return false;
                }

                var digits = n.ToString().Select(z => int.Parse(z.ToString()));

                if (n == 1)
                    return true;

                int sum = 0;
                foreach (var i in digits)
                    sum += i * i;

                if (sum == 1)
                    return true;
                else
                    IsHappy(sum);

                return false;
            }
        }
    }
}

namespace ConsoleEmpty
{
    class Program
    {
        static void Main()
        {

        }
    }
}