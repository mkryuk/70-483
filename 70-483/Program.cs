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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.Web.Script.Serialization;

namespace _70_483
{
    class Person : IComparable
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }        

        public Person(string name, string surname, string secondName)
        {
            Name = name;
            Surname = surname;
            SecondName = secondName;            
        }

        public int CompareTo(object obj)
        {
            var temp = (Person)obj;
            return this.GetHashCode() == temp.GetHashCode()
                ? 0
                : 1;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Surname.GetHashCode() + SecondName.GetHashCode();
        }
    }
    public class Set<T> where T : IComparable
    {
        //Create buckets for data
        public List<T>[] buckets = new List<T>[100];
        public object Length {
            get { return buckets != null ? buckets.Sum(bucket => bucket != null ? bucket.Count : 0) : 0; }
        }

        public void Insert(T item)
        {
            var bucket = GetBucket(item.GetHashCode());
            if (Contains(item, bucket))
                return;
            if (buckets[bucket] == null)
                buckets[bucket] = new List<T>();
            buckets[bucket].Add(item);
        }
        

        private bool Contains(T item, int bucket)
        {
            //If there is no bucket return false
            //otherwise looking for a data in current bucket
            return buckets[bucket] != null && buckets[bucket].Any(member => member.CompareTo(item) == 0);
        }

        private int GetBucket(int hashCode)
        {
            unchecked
            {
                //calculate bucket for the data
                return (int)((uint)hashCode % (uint)buckets.Length);
            }
        }
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            var set = new Set<Person>();
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 1000; i++)
            {
                var person = new Person(i.ToString(), "Petrovich", "Pupkin");
                set.Insert(person);
            }
            sw.Stop();
            Console.WriteLine("Elapsed {0} items in set = {1}", sw.Elapsed, set.Length);
        }

    }
}
