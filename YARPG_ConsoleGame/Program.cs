using System;
using YARPG.Core;

namespace YARPG_ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager game = new GameManager("Harbek");
            game.Encounter();
        }
    }
}
