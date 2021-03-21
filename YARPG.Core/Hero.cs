using System;

namespace YARPG.Core
{
    public class Hero
    {
        public int MaxHealth { get; set; }
        public int CurrentHealth {
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
        public int Damage { get; set; }

        public Hero()
        {

        }
    }
}
