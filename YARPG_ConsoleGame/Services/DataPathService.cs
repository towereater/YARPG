using System;

namespace YARPG_ConsoleGame
{
    public static class DataPathService
    {
        public static string AppDataFolder { get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\YARPG"; }

        public static string SaveFile { get => AppDataFolder + "\\Data\\save.json"; }

        public static string AssetsFile { get => AppDataFolder + "\\Assets"; }

        public static string SkillsFile { get => AppDataFolder + "\\skill.json"; }
    }
}
