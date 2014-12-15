using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace _70_483
{
   
    class Program
    {
       
        private static void Main(string[] args)
        {
            ExceptionDispatchInfo possiblException = null;
            try
            {
                string s = Console.ReadLine();
                int.Parse(s);
            }
            catch (FormatException ex)
            {
                possiblException = ExceptionDispatchInfo.Capture(ex);
            }

            if (possiblException != null)
            {
                possiblException.Throw();
            }
        }
    }
}
