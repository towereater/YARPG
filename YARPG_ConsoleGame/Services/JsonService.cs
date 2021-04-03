using System.IO;
using System.Text.Json;

namespace YARPG_ConsoleGame
{
    /// <summary>
    /// Class used for JSON serialization and deserialization of data.
    /// </summary>
    public static class JsonService
    {
        /// <summary>
        /// Reads data from a JSON file and converts it to a given type.
        /// </summary>
        /// <typeparam name="T">Type to convert data to.</typeparam>
        /// <param name="fileName">Path of the file to read.</param>
        /// <returns>Returns the data read from file converted to the chosen type.</returns>
        public static T DeserializeData<T>(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            T jsonTraslated = JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return jsonTraslated;
        }

        public static void SerializeData<T>(T data, string fileName)
        {
            string jsonString = JsonSerializer.Serialize<T>(data);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
