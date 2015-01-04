using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace _70_483
{
    public class SomeDisposableClass : IDisposable
    {
        private WeakReference _data;
        private bool disposed;

        public void PrintData()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Data {0}",_data);
        }

        public SomeDisposableClass()
        {
            //pre caching data to the weak reference
            var result = GetData();
        }

        public void Run()
        {
            //pre caching data to the weak reference
            var result = GetData();
        }

        public  object GetData()
        {
            if (_data == null)
            {
                _data = new WeakReference(LoadLargeList());
            }
            return _data.Target ?? (_data.Target = LoadLargeList());
        }

        private  List<int> LoadLargeList()
        {
            Console.WriteLine("Loading the large list");
            var data = new List<int>();
            for (var i = 0; i < 1000000; i++)
            {
                data.Add(i);
            }
            return data;
        }
        //Dispose pattern for managed resources
        protected void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                _data = null;
                Console.WriteLine("Object disposed");
            }
            disposed = true;
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose called");
            Dispose(true);
            //supress finalize method for not to keep reference in GC Finalization list
            GC.SuppressFinalize(this);
        }
        //Finalize method for unmanaged resources
        ~SomeDisposableClass()
        {
            //finalize method must call Dispose(false)
            Dispose(false);
            Console.WriteLine("Destructor called from {0} thread", Thread.CurrentThread.ManagedThreadId);
        }
    }

    public class Program
    {

        private static void Main(string[] args)
        {
            var disposableObject = new SomeDisposableClass();
            var someThread = new Thread(ThreadMethod);
            someThread.Start(disposableObject);
            disposableObject.PrintData();

            //using (var disposableNewObject = new SomeDisposableClass(12))
            //{
            //    //due to dispose pattern resources frees only once
            //    disposableObject.Dispose();
            //}

            //next line removes the data from weak reference cache
            //GC.Collect();
            disposableObject.Run();

            //next line removes the data from weak reference cache
            //GC.Collect();
            var someNewResult = disposableObject.GetData();
        }

        private static void ThreadMethod(object obj)
        {
            Thread.Sleep(1000);
            var someDisposableClass = obj as SomeDisposableClass;
            if (someDisposableClass == null) return;

            someDisposableClass.PrintData();
            new Thread(someDisposableClass.PrintData).Start();
        }
    }
}
