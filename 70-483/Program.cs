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
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace _70_483
{
    public class Program
    {

        private static void Main(string[] args)
        {
            var blockExpr = Expression.Block(
                Expression.Call(
                    null,
                    typeof (Console).GetMethod("Write", new Type[] {typeof (String)}),
                    Expression.Constant("Hello ")
                    ),
                Expression.Call(
                    null,
                    typeof (Console).GetMethod("WriteLine", new Type[] {typeof (String)}),
                    Expression.Constant("World!")
                    )
                );
            Expression.Lambda<Action>(blockExpr).Compile()();
        }
    }
}
