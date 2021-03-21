using System;

namespace YARPG.Core
{
    public class CombatManager
    {
        public Hero Hero { get; set; }

        public Enemy Enemy { get; set; }

        public CombatManager(Hero hero, Enemy enemy)
        {
            Hero = hero;
            Enemy = enemy;
        }

        public void Combat()
        {
            while (Hero.IsAlive() && Enemy.IsAlive())
                CombatTurn();
        }

        protected void CombatTurn()
        {
            Enemy.TakeDamage(Hero.Attack);

            if (Enemy.IsAlive())
                Hero.TakeDamage(Enemy.Attack);
        }
    }
}
