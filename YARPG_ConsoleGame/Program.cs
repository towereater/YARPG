using System;
using System.Collections.Generic;
using System.IO;
using YARPG.Core;

namespace YARPG_ConsoleGame
{
    class Program
    {
        // Specifies the path of the GUI model
        static string GUIFilePath = "C:/Users/Andnic-PC/source/repos/YARPG/YARPG_ConsoleGame/ConsoleGUI.txt";
        // Contains the GUI model inside the file specified above
        static string GUIModel;
        // Contains the output messages up to MAX_BUFFER
        static List<string> logBuffer;
        // Maximum lines managed by the console GUI
        const int MAX_BUFFER = 19;

        // Contains all the data of the combat
        static CombatManager cmbMan;

        static void Main(string[] args)
        {
            // Initialises the hero
            Hero h = new Hero()
            {
                Attack = 3,
                MaxHealth = 15,
                CurrentHealth = 15
            };
            // Initialises the enemy
            Enemy e = new Enemy()
            {
                Attack = 1,
                MaxHealth = 10,
                CurrentHealth = 10
            };

            cmbMan = new CombatManager(h, e);
            // Sets the output function
            MessageManager<string> msgMan = cmbMan.MessageManager;
            msgMan.MessageDelivered += PrintFunction;
            // Sets the input function
            InputManager<string> inpMan = cmbMan.InputManager;
            inpMan.InputRequested += InputFunction;

            // Sets up the log buffer
            logBuffer = new List<string>();

            // Reads from the specified file the GUI model
            using (StreamReader sr = new StreamReader(GUIFilePath))
                GUIModel = sr.ReadToEnd();

            // Starts a combat
            cmbMan.Combat();

            // Final GUI output
            PrintGUI();
        }

        /// <summary>
        /// Prints the GUI with all the tags replaced by the correct information.
        /// Padding and trimming are involved when necessary.
        /// </summary>
        static void PrintGUI()
        {
            string gui = GUIModel;
            Enemy e = cmbMan.Enemy;
            Hero h = cmbMan.Hero;

            // Life tag replacement
            gui = gui.Replace("[ENLIFE]", $"{e.CurrentHealth}/{e.MaxHealth}".PadRight(8));
            gui = gui.Replace("[HRLIFE]", $"{h.CurrentHealth}/{h.MaxHealth}".PadRight(8));

            // Log tag replacement
            for (int i = 0; i < logBuffer.Count; i++)
            {
                string tag = $"[LOG{i}]";
                int spacesNum = logBuffer[i].Length - tag.Length;

                // The replaced string is longer than the original one
                if (spacesNum > 0)
                {
                    int tagIndex = gui.IndexOf(tag);
                    gui = gui.Substring(0, tagIndex) + logBuffer[i] + gui.Substring(tagIndex + tag.Length + spacesNum);
                }
                // The replaced string is shorter or equal to the original one
                else
                    gui = gui.Replace(tag, logBuffer[i].PadRight(tag.Length));
            }
            // Removes all the other log entries
            for (int i = logBuffer.Count; i < MAX_BUFFER; i++)
                gui = gui.Replace($"[LOG{i}]", string.Empty.PadRight($"[LOG{i}]".Length));

            // Prints the new GUI
            Console.Clear();
            Console.WriteLine(gui);
        }

        static void PrintFunction(string msg)
        {
            // Adds the new message to the buffer
            AddToBuffer(msg);
            // Prints the new GUI
            PrintGUI();
        }

        static string InputFunction()
        {
            // Adds the new message to the buffer
            AddToBuffer("Attack with a [L]ight or [H]eavy move?");
            // Prints the new GUI
            PrintGUI();

            // Gets the user input
            return Console.ReadLine();
        }

        static void AddToBuffer(string msg)
        {
            // Cuts the maximum elements of the buffer if necessary
            if (logBuffer.Count >= MAX_BUFFER)
                logBuffer.RemoveAt(0);

            logBuffer.Add(msg);
        }
    }
}
