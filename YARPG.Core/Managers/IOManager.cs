using System;

namespace YARPG.Core
{
    /// <summary>
    /// Complete class to managed both input and output.
    /// </summary>
    public class IOManager
    {
        /// <summary>
        /// Result of the input.
        /// </summary>
        public string Input { get; protected set; }

        /// <summary>
        /// Output held by the manager. If set, triggers an event.
        /// </summary>
        public string Output
        {
            get => _output;
            protected set
            {
                _output = value;
                OnMessageDelivery();
            }
        }
        private string _output;

        public GameManager GameManager { get; protected set; }

        /// <summary>
        /// Event triggered when an input is requested.
        /// </summary>
        public event Func<string> InputRequested;

        /// <summary>
        /// Event triggered when Output property is set.
        /// </summary>
        public event Action<string> OutputReceived;

        /// <summary>
        /// The manager is associated to an instance of GameManager class to handle data.
        /// </summary>
        /// <param name="gameMan">GameManager instance containing data to display.</param>
        public IOManager(GameManager gameMan)
        {
            GameManager = gameMan;
        }

        /// <summary>
        /// Triggers the InputRequested event and returns the result the associated function.
        /// </summary>
        public virtual string AskForInput()
        {
            OnInputRequested();
            return Input;
        }

        /// <summary>
        /// Setter function for the Output property.
        /// </summary>
        /// <param name="output">Output to save.</param>
        public virtual void PushOutput(string output)
        {
            Output = output;
        }

        /// <summary>
        /// Function triggered when an input is requested.
        /// </summary>
        protected void OnInputRequested()
        {
            Func<string> handler = InputRequested;
            if (handler != null)
                Input = handler.Invoke();
        }

        /// <summary>
        /// Function triggered when Output property is set.
        /// </summary>
        protected void OnMessageDelivery()
        {
            Action<string> handler = OutputReceived;
            if (handler != null)
                handler.Invoke(Output);
        }
    }
}
