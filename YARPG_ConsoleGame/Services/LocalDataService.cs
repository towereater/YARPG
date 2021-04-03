using System;

namespace YARPG_ConsoleGame
{
    public static class LocalDataService
    {
        public static SaveData LoadGameData()
        {
            return JsonService.DeserializeData<SaveData>(DataPathService.SaveFile);
        }

        public static void SaveGameData(SaveData saveData)
        {
            JsonService.SerializeData<SaveData>(saveData, DataPathService.SaveFile);
        }
    }
}
