using System;

namespace YARPG.Core
{
    /// <summary>
    /// Default class to manage input requests.
    /// </summary>
    public class InputManager<T>
    {
        /// <summary>
        /// Result of the input.
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Event triggered when an input is requested.
        /// </summary>
        public event Func<T> InputRequested;

        /// <summary>
        /// Default constructor of this class.
        /// </summary>
        public InputManager() { }

        /// <summary>
        /// Triggers the InputRequested event and returns the result the associated function.
        /// </summary>
        public T AskForInput()
        {
            OnInputRequested();
            return Result;
        }

        /// <summary>
        /// Function triggered when an input is requested.
        /// </summary>
        protected void OnInputRequested()
        {
            Func<T> handler = InputRequested;
            if (handler != null)
                Result = handler.Invoke();
        }
    }
}
