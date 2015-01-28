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
            return Name.CompareTo(temp.Name) == 0 && Surname.CompareTo(temp.Surname) == 0 &&
                   SecondName.CompareTo(temp.SecondName) == 0
                ? 0
                : 1;
        }       
    }
    public class Set<T> where T : IComparable
    {
        //Create buckets for data
        public List<T>[] buckets = new List<T>[100];

        public void Insert(T item)
        {
            int bucket = GetBucket(item.GetHashCode());
            if (Contains(item, bucket))
                return;
            if (buckets[bucket] == null)
                buckets[bucket] = new List<T>();
            buckets[bucket].Add(item);
        }
        

        private bool Contains(T item, int bucket)
        {
            //If there is no bucket
            if (buckets[bucket] == null) return false;
            //otherwise looking for a data in current bucket
            foreach (T member in buckets[bucket])
            {
                if (member.CompareTo(item) == 0)
                {
                    return true;
                }
            }
            return false;
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
            for (int i = 0; i < 10000; i++)
            {
                set.Insert(new Person(i.ToString(), "Petrovich", "Pupkin"));
            }
            sw.Stop();
            Console.WriteLine("Elapsed {0}", sw.Elapsed);

        }

    }
}
