using System;
using System.Collections.Generic;
using System.IO;
using YARPG.Core;

namespace YARPG_ConsoleGame
{
    class Program
    {
        static string saveDataPath = "../../../save.json";

        static void Main(string[] args)
        {
            Hero h = null;
            bool newGame = true;
            
            // Checks the existance of a save data file and loads it if it's the case
            if (File.Exists(saveDataPath))
            {
                h = LoadHero();
                newGame = false;
            }
            
            // Create a new hero if no previous one was created
            if (newGame)
            {
                Console.Write("Insert hero name: ");
                string heroName = Console.ReadLine();
                h = new Hero()
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
            }

            // Game set up
            GameManager game = new GameManager();
            game.InitializeHero(h);
            game.SetIOManager(new ConsoleManager(game));
            SkillService.Initialize();

            // Gameplay
            game.Encounter();

            // Save test
            SaveHero(h);
        }

        static Hero LoadHero()
        {
            Hero h = JsonService.DeserializeData<Hero>(saveDataPath);
            return h;
        }

        static void SaveHero(Hero h)
        {
            JsonService.SerializeData<Hero>(h, saveDataPath);
        }
    }
}
