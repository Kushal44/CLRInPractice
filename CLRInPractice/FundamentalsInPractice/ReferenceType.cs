using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace FundamentalsInPractice
{
    internal class ReferenceType
    {
        private static void Swap(ref object first, ref object second)
        {
            Object t = second;
            second = first;
            first = second;
        }

        public static void SwapStrings()
        {
            string s1 = "Kushal";
            string s2 = "Raja";

            //Variables passed by reference to a method must be of the same type as declared in the method
            //Swap(ref s1, ref s2);
        }

        private static void Swap<T>(ref T first, ref T second)
        {
            T temp = first;
            first = second;
            second = temp;
        }

        public static void SwapAnyType()
        {
            int first = 1;
            int second = 2;
            Swap<int>(ref first, ref second);

            Console.WriteLine("{0}, {1}", first, second);
        }
    }
}
