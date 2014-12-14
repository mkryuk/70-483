using System;
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

        public void DoSomeWork( EventHandler _event)
        {
            SomeEvent += _event;
            Console.WriteLine("Some work started");
            Thread.Sleep(1000);
            Console.WriteLine("Some work complete");
            someEvent(this, EventArgs.Empty);
        }
    }

    internal class Program
    {
        
        private static void Main(string[] args)
        {
            SomeClass myClass = new SomeClass();
            myClass.DoSomeWork((caller,eventArgs) 
                => Console.WriteLine("Called event raise"));
        }
    }
}
