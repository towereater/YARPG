using System;

namespace YARPG.Core
{
    /// <summary>
    /// Default class to manage messages delivery.
    /// </summary>
    public class MessageManager
    {
        /// <summary>
        /// Message held by the manager. If set, triggers an event.
        /// </summary>
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnMessageDelivery();
            }
        }
        private string _message;

        /// <summary>
        /// Event triggered when Message property is set.
        /// </summary>
        public event Action<string> MessageDelivered;

        /// <summary>
        /// Default constructor of this class.
        /// </summary>
        public MessageManager() { }

        /// <summary>
        /// Alternative function to set Message property.
        /// </summary>
        /// <param name="message">Message to save.</param>
        public void NewMessage(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Function triggered when a new message is delivered.
        /// </summary>
        protected void OnMessageDelivery()
        {
            MessageDelivered?.Invoke(Message);
        }
    }
}
