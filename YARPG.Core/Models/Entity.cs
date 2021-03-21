using System;

namespace YARPG.Core
{
    public class Entity
    {
        public int MaxHealth { get; set; }

        public int CurrentHealth
        {
            get => _health;
            set
            {
                if (value < 0)
                    _health = 0;
                else
                    _health = value > MaxHealth ? MaxHealth : value;
            }
        }
        private int _health;

        public int Attack { get; set; }

        public Entity() { }

        public virtual bool IsAlive()
        {
            return CurrentHealth > 0;
        }

        public virtual void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
        }
    }
}
