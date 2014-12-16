using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace _70_483
{

    public interface ISomeInterface
    {
        string Data { get; set; }
    }

    public static class SomeExtentions
    {
        public static string GetValue(this ISomeInterface _iface)
        {
            return _iface.Data;
        }
    }

    public class SomeClass : ISomeInterface
    {
        public string Data { get; set; }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            var iface = new SomeClass();
            iface.Data = "Some data";

            Console.WriteLine(iface.GetValue());
        }
    }
}
