using System.IO;
using System.Text.Json;

namespace YARPG.Core
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
        /// <param name="filePath">Path of the file to read.</param>
        /// <returns>Returns the data read from file converted to the chosen type.</returns>
        public static T DeserializeData<T>(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            T jsonTraslated = JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return jsonTraslated;
        }
    }
}
