using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace _70_483
{
    public interface ISomeAnotherInterface
    {
        void FromSomeAnotherInterface();
    }

    public interface ITestInterface:ISomeAnotherInterface
    {
        int Data { get; }
        void ShowSomething();
        int GetSomething();
    }

    internal class SomeClass:ITestInterface
    {
        //Implementation from derived interface
        public void FromSomeAnotherInterface()
        {
            throw new NotImplementedException();
        }
        //Data will be available to set only from class varaible
        public int Data { get; set; }

        //Explicit implementation
        void ITestInterface.ShowSomething()
        {
            Console.WriteLine(Data);
        }
        //Implicit implementation
        public int GetSomething()
        {
            return Data;
        }
    }

    class Program
    {
        private static void Main(string[] args)
        {    
            //implement as a class instance
            var someClass = new SomeClass();
            //implement as interface instance
            ITestInterface someTestInterface = new SomeClass();
            //cannot acces method ShowSomething but can access GetSomething
            someClass.GetSomething();
            //can access both GetSomething and ShowSomething methods
            someTestInterface.ShowSomething();
        }
    }
}
