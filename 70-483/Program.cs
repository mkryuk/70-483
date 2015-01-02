using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
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

            var compileUnit = new CodeCompileUnit();
            var myNamespace = new CodeNamespace("MyNamespace");
            myNamespace.Imports.Add(new CodeNamespaceImport("System"));
            var myClass = new CodeTypeDeclaration("MyClass");
            var start = new CodeEntryPointMethod();
            var cs1 = new CodeMethodInvokeExpression(
            new CodeTypeReferenceExpression("Console"),
            "WriteLine", new CodePrimitiveExpression("Hello World!"));
            compileUnit.Namespaces.Add(myNamespace);
            myNamespace.Types.Add(myClass);
            myClass.Members.Add(start);
            start.Statements.Add(cs1);

            var provider = new CSharpCodeProvider();
            using (var sw = new StreamWriter("HelloWorld.cs", false))
            {
                var tw = new IndentedTextWriter(sw, " ");
                provider.GenerateCodeFromCompileUnit(compileUnit, tw,
                new CodeGeneratorOptions());
                tw.Close();
            }
        }
    }
}
