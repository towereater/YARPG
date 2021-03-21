using System;
using YARPG.Core;

namespace YARPG_ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Hero h = new Hero();
            h.MaxHealth = 15;

            Console.WriteLine("Hello World! MAXH=" + h.MaxHealth);
        }
    }
}
