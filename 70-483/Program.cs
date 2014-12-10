using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _70_483
{

    class Program
    {
        static void Main(string[] args)
        {
            Task<Int32[]> parent = Task.Run(() =>
            {
                var result = new int[3];

                TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent,TaskContinuationOptions.ExecuteSynchronously);
                tf.StartNew(() => result[0] = 0);
                tf.StartNew(() => result[1] = 1);
                tf.StartNew(() => result[2] = 2);

                return result;
            });

            var finalTask = parent.ContinueWith(
               parentTask =>
               {
                   foreach (int i in parentTask.Result)
                       Console.WriteLine(i);
               });


            finalTask.Wait();
        }
    }
}
