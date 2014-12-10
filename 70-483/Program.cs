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
        [ThreadStatic]
        public static int _field;

        public static ThreadLocal<int> _field2 = new ThreadLocal<int>(() => Thread.CurrentThread.ManagedThreadId);

        static void Main(string[] args)
        {            
            new Thread(() =>
            {
                for (int i = 0; i < _field2.Value; i++)
                {                    
                    _field++;
                    Console.WriteLine(Thread.CurrentThread.CurrentCulture.TextInfo.ToString() + _field);
                    
                }
            }).Start();

            new Thread(() =>
            {
                for (int i = 0; i < _field2.Value; i++)
                {
                    _field++;
                    Console.WriteLine("Thread 2: " + _field);
                }
            }).Start();
        }
    }
}
