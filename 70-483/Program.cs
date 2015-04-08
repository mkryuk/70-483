using System;
using System.Diagnostics;
using System.Text;

namespace _70_483
{
    public class Program
    {
        private static void Main(string[] args)
        {
            if (CreatePerformanceCounters())
            {
                Console.WriteLine("Created performance counters");
                Console.WriteLine("Please restart application");
                Console.ReadKey();
                return;
            }
            var totalOperationsCounter = new PerformanceCounter("MyCategory","# operations executed","",false);
            var operationsPerSecondCounter = new PerformanceCounter("MyCategory","# operations / sec","",false);
            totalOperationsCounter.Increment();
            operationsPerSecondCounter.Increment();            
        }

        private static bool CreatePerformanceCounters()
        {
            if (!PerformanceCounterCategory.Exists("MyCategory")){
                CounterCreationDataCollection counters =
                    new CounterCreationDataCollection
                    {
                        new CounterCreationData(
                            "# operations executed",
                            "Total number of operations executed",
                            PerformanceCounterType.NumberOfItems32),
                        new CounterCreationData(
                            "# operations / sec",
                            "Number of operations executed per second",
                            PerformanceCounterType.RateOfCountsPerSecond32)};
                //create performance counter, to see run perfmon.exe
                PerformanceCounterCategory.Create("MyCategory","Sample category for Codeproject",counters);                
                return true;
            }
            return false;
        }
    }
}