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
        private static void Main(string[] args)
        {
            var byteConverter = new UnicodeEncoding();
            var sha256 = SHA256.Create();
            
            var data = "A paragraph of text";
            var hashA = sha256.ComputeHash(byteConverter.GetBytes(data));
            data = "A paragraph of changed text";
            var hashB = sha256.ComputeHash(byteConverter.GetBytes(data));
            data = "A paragraph of text";
            var hashC = sha256.ComputeHash(byteConverter.GetBytes(data));

            Console.WriteLine("Is hashA and hashB are equal: {0}",hashA.SequenceEqual(hashB));
            Console.WriteLine("Is hashA and hashC are equal: {0}",hashA.SequenceEqual(hashC));
        }

    }
}
