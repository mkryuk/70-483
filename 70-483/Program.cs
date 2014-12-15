using System;
using System.Threading;
using System.Threading.Tasks;

namespace _70_483
{
   
    class Program
    {
       
        private static void Main(string[] args)
        {
           string s = Console.ReadLine();
            try
            {
                int i = int.Parse(s);
                if (i == 42) Environment.FailFast("Special number entered");
            }
            finally
            {
                Console.WriteLine("Program complete.");
            }
        }
    }
}
