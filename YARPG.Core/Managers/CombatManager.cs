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
        /// Input/Output manager used during combat.
        /// </summary>
        public IOManager IOManager { get; protected set; }

        // Local field needed for random actions generation
        private Random _random;

        /// <summary>
        /// Default constructor for this class.
        /// </summary>
        public CombatManager()
        {
            _random = new Random(DateTime.Now.Millisecond);
        }

        /// <summary>
        /// Sets up the hero and the enemy of the combat.
        /// Error if any of the arguments is not initialized.
        /// </summary>
        public CombatManager InitializeCombat(Hero hero, Enemy enemy)
        {
            // Checks arguments validity
            if (hero == null)
                throw new ArgumentNullException("hero", "The argument is null.");
            else if (enemy == null)
                throw new ArgumentNullException("enemy", "The argument is null.");

            Hero = hero;
            Enemy = enemy;

            return this;
        }

        public CombatManager SetIOManager(IOManager ioMan)
        {
            IOManager = ioMan;

            return this;
        }

        /// <summary>
        /// Checks wether or not the combat is fully initialized.
        /// </summary>
        public bool IsCombatInitialized()
        {
            return Hero != null && Enemy != null && IOManager != null;
        }

        /// <summary>
        /// Resolves the combat until Hero or Enemy die.
        /// </summary>
        public void Combat()
        {
            if (!IsCombatInitialized())
                throw new ArgumentNullException("Parameter name unknown", "A combat parameter was not initialized.");

            IOManager.PushOutput($"{Enemy.Name} appears! Time to combat!");

            while (Hero.IsAlive && Enemy.IsAlive)
                CombatTurn();

            // Checks combat ending condition
            if (!Hero.IsAlive)
                IOManager.PushOutput("Oh no, Hero is dead! GAME OVER!");
            else
                IOManager.PushOutput("Enemy is dead!");
        }

        /// <summary>
        /// Resolves one turn of the combat.
        /// </summary>
        protected void CombatTurn()
        {
            int index;
            bool flag;

            // Gets a skill using input manager
            do
            {
                PrintHeroSkills();

                flag = int.TryParse(IOManager.AskForInput(), out index);
            }
            while (!flag || index < 0 || index >= Hero.Skills.Count);

            // Gets the damage assigned to the chosen skill
            Skill skill = SkillService.GetSkillByName(Hero.Skills[index]);
            int damage = skill.Damage;

            // Inflicts the damage to the enemy health
            Enemy.TakeDamage(damage);
            IOManager.PushOutput($"Enemy took {damage} point(s) of damage!");

            // If enemy is still alive counterattacks
            if (Enemy.IsAlive)
            {
                // Chooses a random skill from the set of the enemy
                index = _random.Next(0, Enemy.Skills.Count);
                skill = SkillService.GetSkillByName(Enemy.Skills[index]);
                damage = skill.Damage;

                // Inflicts the damage to the hero health
                Hero.TakeDamage(damage);
                IOManager.PushOutput($"Hero took {damage} point(s) of damage!");
            }
        }

        /// <summary>
        /// Prints the entire Hero skill set.
        /// </summary>
        protected void PrintHeroSkills()
        {
            for (int i = 0; i < Hero.Skills.Count; i++)
            {
                Skill skill = SkillService.GetSkillByName(Hero.Skills[i]);

                // Skips all null skills
                if (skill == null)
                {
                    IOManager.PushOutput($"{i}. UNKNOWN SKILL NAME");
                    continue;
                }

                IOManager.PushOutput($"{i}. {skill.Name}, Damage: {skill.Damage}");
            }
        }
    }
}
