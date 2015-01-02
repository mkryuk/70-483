using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace _70_483
{
    public class SomeClass
    {
        private int Id { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }

        public void SomeMethod(string someParam)
        {
            Console.WriteLine("Some Method called with param: {0}", someParam);
        }
    }

    public class Program
    {
        //reflect object and find all integer fields
        public static void DumpObject(object obj)
        {
            var fieldInfos = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var info in fieldInfos
                .Where(info => info.FieldType == typeof(int)))
            {
                Console.WriteLine("{0} {1}",info.Name, info.GetValue(obj));
            }
        }

        private static void Main(string[] args)
        {

            var i = 42;
            //call method via reflection
            var methodInfo = i.GetType().GetMethod("CompareTo",new[]{typeof(int)});
            var result = (int) methodInfo.Invoke(i, new object[] {41});
            Console.WriteLine("the result is {0}",result);
            //call method via reflection
            var someClass = new SomeClass() { Position = 10 };
            var someMethod = someClass.GetType().GetMethod("SomeMethod");
            someMethod.Invoke(someClass,new object[]{"param"});

            DumpObject(someClass);
        }
    }
}
