using System;
using System.IO;
using System.Linq;

namespace Provod
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleFileManager consoleFileManager = new ConsoleFileManager();
            consoleFileManager.Start();
        }
    }
}