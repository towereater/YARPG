using System;
using System.Collections.Generic;
using YARPG.Core;

namespace YARPG_ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Hero creation
            Console.Write("Insert hero name: ");
            string heroName = Console.ReadLine();
            Hero h = new Hero()
            {
                Name = heroName,
                MaxHealth = 15,
                CurrentHealth = 15,
                Skills = new List<string>()
                {
                    "Unarmed attack",
                    "Fireball"
                }
            };

            // Game set up
            GameManager game = new GameManager();
            game.InitializeHero(h);
            game.SetIOManager(new ConsoleManager(game));
            SkillService.Initialize();

            // Gameplay
            game.Encounter();
        }
    }
}
