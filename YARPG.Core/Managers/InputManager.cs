using System;

namespace YARPG.Core
{
    /// <summary>
    /// Default class to manage input requests.
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// Result of the input.
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// Event triggered when an input is requested.
        /// </summary>
        public event Func<object> InputRequested;

        /// <summary>
        /// Default constructor of this class.
        /// </summary>
        public InputManager() { }

        /// <summary>
        /// Triggers the InputRequested event and returns the result the associated function.
        /// </summary>
        public object AskForInput()
        {
            OnInputRequested();
            return Result;
        }

        /// <summary>
        /// Function triggered when an input is requested.
        /// </summary>
        protected void OnInputRequested()
        {
            Result = InputRequested?.Invoke();
        }
    }
}
