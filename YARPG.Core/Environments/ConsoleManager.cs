using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YARPG.Core
{
    /// <summary>
    /// Default IO manager for the console environment.
    /// </summary>
    public sealed class ConsoleManager : IOManager
    {
        /// <summary>
        /// Contains the default GUI model.
        /// </summary>
        public string GUIModel { get; set; }

        // Maximum lines managed by the console GUI
        private int MAX_BUFFER = 19;

        // Contains the output messages up to MAX_BUFFER
        private List<string> logBuffer;

        /// <summary>
        /// Returns a default IOManager for the console environment.
        /// </summary>
        public ConsoleManager (GameManager gameMan)
            : base (gameMan)
        {
            // Sets up the events with standard functions
            InputRequested += InputFunction;
            OutputReceived += OutputFunction;

            // Creates a new buffer
            logBuffer = new List<string>();

            // Reads from the specified file the GUI model
            using (StreamReader sr = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "YARPG.Core", "Environments", "ConsoleGUI.txt")))
                GUIModel = sr.ReadToEnd();
        }

        /// <summary>
        /// Default input function for this environment.
        /// </summary>
        public string InputFunction()
        {
            // Gets the user input and adds it to the buffer
            string input = Console.ReadLine();
            AddToBuffer(input);

            PrintGUI();

            return input;
        }

        /// <summary>
        /// Default output function for this environment.
        /// </summary>
        /// <param name="output">Value to display on console.</param>
        public void OutputFunction(string output)
        {
            // Adds the new message to the buffer
            AddToBuffer(output);

            // Prints the updated GUI
            PrintGUI();
        }

        public void AddToBuffer(string output)
        {
            // Cuts the maximum elements of the buffer if necessary
            if (logBuffer.Count >= MAX_BUFFER)
                logBuffer.RemoveAt(0);

            logBuffer.Add(output);
        }

        /// <summary>
        /// Prints the GUI with all the tags replaced by the correct data.
        /// Padding and trimming are involved when necessary.
        /// </summary>
        public void PrintGUI()
        {
            // Prepares the new GUI
            StringBuilder gui = new StringBuilder(GUIModel);

            // Sets up the key-value pairs to replace
            Hero h = GameManager.CombatManager.Hero;
            Enemy e = GameManager.CombatManager.Enemy;
            Dictionary<string, object> tagValues = new Dictionary<string, object>()
            {
                { "[HRNAME]", h.Name },
                { "[HRLIFE]", $"{h.CurrentHealth}/{h.MaxHealth}" },
                { "[ENNAME]", e.Name },
                { "[ENLIFE]", $"{e.CurrentHealth}/{e.MaxHealth}" },
                { "[INPUT]", string.Empty }
            };

            foreach(KeyValuePair<string, object> pair in tagValues)
            {
                string key = pair.Key;
                string value = pair.Value.ToString();

                int spacesNum = value.Length - key.Length;

                // The replaced string is longer than the original one
                if (spacesNum > 0)
                {
                    int keyIndex = GUIModel.IndexOf(key);
                    gui.Remove(keyIndex, key.Length + spacesNum);
                    gui.Insert(keyIndex, value);
                }
                // The replaced string is shorter or equal to the original one
                else
                    gui.Replace(key, value.PadRight(key.Length));
            }

            // Log tag replacement
            for (int i = 0; i < logBuffer.Count; i++)
            {
                string tag = $"[LOG{i}]";
                int spacesNum = logBuffer[i].Length - tag.Length;

                // The replaced string is longer than the original one
                if (spacesNum > 0)
                {
                    int tagIndex = GUIModel.IndexOf(tag);
                    gui.Remove(tagIndex, tag.Length + spacesNum);
                    gui.Insert(tagIndex, logBuffer[i]);
                }
                // The replaced string is shorter or equal to the original one
                else
                    gui.Replace(tag, logBuffer[i].PadRight(tag.Length));
            }
            // Removes all the other log entries
            for (int i = logBuffer.Count; i < MAX_BUFFER; i++)
                gui.Replace($"[LOG{i}]", string.Empty.PadRight($"[LOG{i}]".Length));

            // Prints the new GUI
            Console.Clear();
            Console.WriteLine(gui.ToString());
        }
    }
}
