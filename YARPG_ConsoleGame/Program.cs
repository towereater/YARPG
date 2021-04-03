using System;
using System.Collections.Generic;
using System.IO;
using YARPG.Core;

namespace YARPG_ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            SaveData saveData;
            Hero h = null;

            bool newGame = true;

            // Checks the existance of a save data file and loads it if it's the case
            if (File.Exists(DataPathService.SaveFile))
            {
                saveData = LocalDataService.LoadGameData();
                h = saveData.Hero;
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
            GameManager game = new GameManager(h, new ConsoleManager(null));
            SkillService.Initialize();

            // Gameplay start
            game.Encounter();

            // Saves the game localy
            saveData = new SaveData()
            {
                Hero = h
            };
            LocalDataService.SaveGameData(saveData);
        }
    }
}
