using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        bool Load(Program prgrm);
    }

    public class MyPlugin : IPlugin
    {
        public string Name { get { return "My Plugin"; } }
        public string Description
        {
            get { return "Description"; }
        }
        public bool Load(Program prgrm)
        {
            return true;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
