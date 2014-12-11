using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace _70_483
{

    class Program
    {

        static void Main(string[] args)
        {
            BlockingCollection<string> collection = new BlockingCollection<string>();

            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine("one: " + collection.Take());
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine("two: " + collection.Take());
                }
            });

            Task write = Task.Run(() =>
            {
                while (true)
                {
                    string data = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(data)) break;
                    collection.Add(data);
                }
            });

            write.Wait();
        }
    }
}
