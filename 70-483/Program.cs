using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace _70_483
{
   
    class Program
    {
       [Flags]
        public enum MyEnum
        {
            First = 0x0,
            Second = 0x1,
            Third = 0x2,
            Fourth = 0x3,
            Fifth = 0x4
        }
       
        private static void Main(string[] args)
        {
            //Enums marked with [Flags] can be piped
            var somEnum = MyEnum.Fifth | MyEnum.Second;
            Console.WriteLine(somEnum);
        }
    }
}
