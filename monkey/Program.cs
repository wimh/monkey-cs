using System;
using System.IO;

namespace monkey
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello Monkey!");
            
            var repl = new libmonkey.repl.Repl();
            repl.Start(new StreamReader(Console.OpenStandardInput()),
                new StreamWriter(Console.OpenStandardOutput()));
        }
    }
}