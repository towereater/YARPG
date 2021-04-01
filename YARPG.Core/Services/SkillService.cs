using System;
using System.Collections.Generic;
using System.IO;

namespace YARPG.Core
{
    /// <summary>
    /// Service used to keep all the data of the skills.
    /// </summary>
    public static class SkillService
    {
        /// <summary>
        /// List of all skills of the game.
        /// </summary>
        public static List<Skill> Skills { get; set; }

        // Default file name for skills list
        private const string _defaultFileName = "skills.json";

        /// <summary>
        /// Reloads the skills from a given json file.
        /// </summary>
        /// <param name="filePath">Path of the skills file.</param>
        public static void Initialize(string filePath = "")
        {
            // Navigates to the default file if none was specified
            if (filePath == string.Empty)
                // BaseDir > Debug > bin > ProjectFolder > YARPG.Core > config > fileName
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "YARPG.Core", "Config", _defaultFileName);

            // Deserializes the specified file
            Skills = JsonService.DeserializeData<List<Skill>>(filePath);
        }

        /// <summary>
        /// Searches the list of the skills the one with the correct name and returns it.
        /// </summary>
        /// <param name="name">Name of the skill to find.</param>
        /// <returns>Return the skill instance with the given name.</returns>
        public static Skill GetSkillByName(string name)
        {
            foreach (Skill s in Skills)
                if (s.Name == name)
                    return s;

            return null;
        }
    }
}
