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
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    class MyMegaAttribute : Attribute
    {
        public string Data { get; set; }
        public static int Count { get; set; }

    }

    [MyMega(Data = "Some data")]
    internal class Person
    {
        [MyMega]
        public void DoSome()
        {
            Console.WriteLine("some thing");
        }
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            var person = new Person();
            
            person.DoSome();
            if (Attribute.IsDefined(typeof(Person), typeof(SerializableAttribute)))
            {
                Console.WriteLine("Person has SerializableAttribute");
            }

            var attribute =
                (MyMegaAttribute)Attribute.GetCustomAttribute(
                typeof(Person),
                typeof(MyMegaAttribute));
            Console.WriteLine(attribute.Data);

            Assembly pluginAssembly = Assembly.Load("Plugins");
            var plugins = from type in pluginAssembly.GetTypes()
                where typeof (Plugins.IPlugin).IsAssignableFrom(type) && !type.IsInterface
                select type;

            foreach (var pluginType in plugins)
            {
                var plugin = Activator.CreateInstance(pluginType) as Plugins.IPlugin;
                Console.WriteLine("Name: {0} Description: {1}",plugin.Name, plugin.Description);
            }

        }
    }
}
