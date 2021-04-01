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
        /// Default constructor for this class.
        /// </summary>
        public GameManager()
        {
            CombatManager = new CombatManager();
        }

        /// <summary>
        /// Sets the Hero instance associated to the game.
        /// </summary>
        /// <param name="h">Hero instance to use in the game.</param>
        /// <returns>Returns the GameManager instance.</returns>
        public GameManager InitializeHero(Hero h)
        {
            Hero = h;

            return this;
        }

        /// <summary>
        /// Sets the IOManager instance associated to the game.
        /// </summary>
        /// <param name="ioMan">IOManager instance to use in the game.</param>
        /// <returns>Returns the GameManager instance.</returns>
        public GameManager SetIOManager(IOManager ioMan)
        {
            IOManager = ioMan;
            CombatManager.SetIOManager(ioMan);

            return this;
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
            CombatManager.InitializeCombat(Hero, e);
            CombatManager.Combat();
        }
    }
}
