using System;
using YARPG.Core;

namespace YARPG_ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Hero h = new Hero()
            {
                Attack = 3,
                MaxHealth = 15,
                CurrentHealth = 15
            };
            Enemy e = new Enemy()
            {
                Attack = 1,
                MaxHealth = 10,
                CurrentHealth = 10
            };

            CombatManager cmbMan = new CombatManager(h, e);
            MessageManager msgMan = cmbMan.MessageManager;

            msgMan.MessageDelivered += PrintFunction;

            cmbMan.Combat();
        }

        static void PrintFunction(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
