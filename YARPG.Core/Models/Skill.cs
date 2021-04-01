using System;

namespace YARPG.Core
{
    /// <summary>
    /// Class container for each skill properties.
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Personal name of the skill. Used for the search function.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Attack damage of the skill.
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// Complete description of the skill.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Default constructor for this class.
        /// </summary>
        public Skill() { }
    }
}
