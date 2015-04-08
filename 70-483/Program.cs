using System;
using System.Diagnostics;
using System.Text;

namespace _70_483
{
    public class Program
    {
        private static void Main(string[] args)
        {
            using (var pc = new PerformanceCounter("Memory", "Available Bytes"))
            {
                const string text = "Available memory: ";
                Console.Write(text);
                do
                {
                    while (!Console.KeyAvailable)
                    {
                        Console.Write(pc.RawValue);
                        Console.SetCursorPosition(text.Length,Console.CursorTop);
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            }
        }
    }
}
