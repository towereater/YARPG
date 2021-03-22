using System;

namespace YARPG.Core
{
    /// <summary>
    /// Default class to start and manage combats between Hero and Enemy.
    /// </summary>
    public class CombatManager
    {
        /// <summary>
        /// Hero instance in the combat.
        /// </summary>
        public Hero Hero { get; protected set; }

        /// <summary>
        /// Enemy instance in the combat.
        /// </summary>
        public Enemy Enemy { get; protected set; }

        /// <summary>
        /// Message manager used to deliver messages during combat.
        /// </summary>
        public MessageManager MessageManager { get; set; }

        /// <summary>
        /// Input manager used to ask for actions during combat.
        /// </summary>
        public InputManager InputManager { get; set; }

        /// <summary>
        /// Defines a new combat scene with a Hero and an Enemy.
        /// Error if one of the is not initialised.
        /// </summary>
        /// <param name="hero">Hero part in the combat.</param>
        /// <param name="enemy">Enemy part in the combat.</param>
        public CombatManager(Hero hero, Enemy enemy)
        {
            if (hero == null)
                throw new ArgumentNullException("hero", "The argument is null");
            else if (enemy == null)
                throw new ArgumentNullException("enemy", "The argument is null");

            Hero = hero;
            Enemy = enemy;

            MessageManager = new MessageManager();
            InputManager = new InputManager();
        }

        /// <summary>
        /// Resolves the combat until the Hero or the Enemy die.
        /// </summary>
        public void Combat()
        {
            MessageManager.NewMessage("Combat started!");

            while (Hero.IsAlive && Enemy.IsAlive)
                CombatTurn();

            if (!Hero.IsAlive)
                MessageManager.NewMessage("Oh no, Hero is dead! GAME OVER!");
            else if (!Enemy.IsAlive)
                MessageManager.NewMessage("Enemy is dead!");
            else
                throw new NotImplementedException("Nor Hero nor Enemy is dead inside Combat function.");
        }

        /// <summary>
        /// Resolves one turn of the combat.
        /// </summary>
        protected void CombatTurn()
        {
            string action;

            do
            {
                action = (InputManager.AskForInput() as string).ToLower();
            } while (action != "l" && action != "h");

            int damage = Hero.Attack;
            if (action == "h")
                damage += 2;

            Enemy.TakeDamage(damage);
            MessageManager.NewMessage($"Enemy took {damage} point(s) of damage!");

            if (Enemy.IsAlive)
            {
                Hero.TakeDamage(Enemy.Attack);
                MessageManager.NewMessage($"Hero took {Enemy.Attack} point(s) of damage!");
            }
        }
    }
}
