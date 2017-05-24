using PoGoPrivate.Enums;
using System;

namespace PoGoPrivate.Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Log a specific message by LogLevel. 
        /// </summary>
        /// <param name="message">
        /// The message to log. 
        /// </param>
        /// <param name="level">
        /// Optional. Default <see cref="System.LogLevel.Info" />. 
        /// </param>
        /// <param name="color">
        /// Optional. Default automatic color. 
        /// </param>
        void Write(string message, LogLevel level = LogLevel.Info, ConsoleColor color = ConsoleColor.Black);

        void LineSelect(int lineChar = 0, int linesUp = 1);
    }
}