using System;
using System.Threading;
using System.Threading.Tasks;

namespace _70_483
{

    class Program
    {

        private static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            Task t = Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }

                cancellationToken.ThrowIfCancellationRequested();
            }, cancellationToken);

            try
            {
                Console.WriteLine("Press enter to stop the task");
                Console.Read();
                cancellationTokenSource.Cancel();
                t.Wait();
            }
            catch (AggregateException exception)
            {
                
                Console.WriteLine(exception.InnerExceptions[0].Message);
            }

            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();

        }
    }
}
