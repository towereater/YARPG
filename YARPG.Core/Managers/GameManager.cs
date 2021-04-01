using System.Collections.Generic;

namespace YARPG.Core
{
    /// <summary>
    /// Class used to have the total control over the current game.
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// Hero instance associated to the game.
        /// </summary>
        public Hero Hero { get; protected set; }

        /// <summary>
        /// Manager used to have IO interactions with the environment.
        /// </summary>
        public IOManager IOManager { get; protected set; }

        /// <summary>
        /// Manager used to make entities interact using combat.
        /// </summary>
        public CombatManager CombatManager { get; protected set; }

        /// <summary>
        /// Initializes the current game specifing the hero's name and the skill file path.
        /// </summary>
        /// <param name="heroName">Name of the hero.</param>
        /// <param name="skillFilePath">Path of the file containing the skills.</param>
        public GameManager(string heroName, string skillFilePath = "")
        {
            // Complete initialization of the managers
            SkillService.Initialize(skillFilePath);
            IOManager = new ConsoleManager(this);
            CombatManager = new CombatManager(IOManager);

            // Default hero created for the game
            Hero = new Hero() {
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

        /// <summary>
        /// Generates a new combat event and starts it.
        /// </summary>
        public void Encounter()
        {
            // Default enemy created for the event
            Enemy e = new Enemy() {
                Name = "Brute",
                MaxHealth = 10,
                CurrentHealth = 10,
                Skills = new List<string>()
                {
                    "Unarmed attack",
                    "Sword strike"
                }
            };

            // Sets up the combat and starts it
            CombatManager.Initialize(Hero, e);
            CombatManager.Combat();
        }
    }
}
