using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _70_483
{
    public class SomeClass
    {
        private event EventHandler someEvent = delegate { };

        public event EventHandler SomeEvent
        {
            add
            {
                lock (someEvent)
                {
                    someEvent += value;
                } 
            }
            remove
            {
                lock (someEvent)
                {
                    someEvent -= value;
                }
            }
        }

        public void DoSomeWork()
        {
            
            Console.WriteLine("Some work started");
            Thread.Sleep(1000);
            Console.WriteLine("Some work complete");
            var exceptions = new List<Exception>();

            foreach (var handler in someEvent.GetInvocationList())
            {
                try
                {
                    handler.DynamicInvoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
    }

    internal class Program
    {
        
        private static void Main(string[] args)
        {
            var myClass = new SomeClass();
            //Adding invocation list
            myClass.SomeEvent += (sender, eventArgs) => Console.WriteLine("First invocation");
            myClass.SomeEvent += (sender, eventArgs) => { throw new Exception("This is exception"); };
            myClass.SomeEvent += (sender, eventArgs) => Console.WriteLine("Third invocation");

            try
            {
                myClass.DoSomeWork();
            }
            catch (AggregateException ex)
            {
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine(innerException.InnerException.Message);
                }
            }
        }
    }
}
