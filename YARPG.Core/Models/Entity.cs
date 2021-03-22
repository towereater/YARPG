using System;

namespace YARPG.Core
{
    /// <summary>
    /// Base class for any object describing a living entity.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Max health of the entity.
        /// </summary>
        public int MaxHealth { get; set; }

        /// <summary>
        /// Current health of the entity. Capped at MaxHealth and 0.
        /// </summary>
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

        /// <summary>
        /// States wether or not the entity is alive.
        /// </summary>
        public virtual bool IsAlive => CurrentHealth > 0;

        /// <summary>
        /// Attack damage of the entity.
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// Default constructor for this class.
        /// </summary>
        public Entity() { }

        /// <summary>
        /// Decreases CurrentHealth by some value.
        /// </summary>
        /// <param name="damage">Value to remove from CurrentHealth.</param>
        public virtual void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
        }
    }
}
