using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.Web.Script.Serialization;

namespace _70_483
{
    public class Program
    {
        public static void TestMethod()
        {
            var traceSource = new TraceSource("myTraceSource");
            traceSource.TraceEvent(TraceEventType.Warning, 1, "This is a warning from TestMethod");
        }

        private static void Main(string[] args)
        {
            // global listeners defined in App.config
            //local textWriter listener
            var textWriter = new TextWriterTraceListener(new StreamWriter("trace.txt"));
            //local XmlWriterListener
            var xmlWriter = new XmlWriterTraceListener(new StreamWriter("trace.xml"));
            var traceSource = new TraceSource("myTraceSource");
            
            //if we want to clear all listeners
            //traceSource.Listeners.Clear();

            //add listeners only for Main method
            traceSource.Listeners.Add(textWriter);
            traceSource.Listeners.Add(xmlWriter);

            //call other method to test global listeners
            TestMethod();
            traceSource.TraceInformation("Start trace");
            var thread = new Thread(() =>
            {
                while (true)
                {
                    //writes data to trace.txt trace.xml 
                    traceSource.TraceInformation("Current datetime:{0}", DateTime.Now.ToLongTimeString());
                    //writes data to output.txt because of filter configured in App.config
                    traceSource.TraceEvent(TraceEventType.Warning, 0, "This is a warning");
                }
            });
            thread.Start();
            //wait for 10 milliseconds
            Thread.Sleep(10);
            thread.Abort();
            //writes down all the data
            traceSource.Flush();
            traceSource.Close();
        } 

    }
}
