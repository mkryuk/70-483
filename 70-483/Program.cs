using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _70_483
{

    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 10, (i) => Thread.Sleep(1000));

            var numbers = Enumerable.Range(0, 10);
            Parallel.ForEach(numbers, i =>
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            });
        }
    }
}
