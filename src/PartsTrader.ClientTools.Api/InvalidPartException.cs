using System;

namespace PartsTrader.ClientTools.Api
{
    /// <summary>
    /// Exception thrown when a part is invalid.
    /// </summary>
    public class InvalidPartException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPartException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidPartException(string message) : base(message)
        { }
    }
}