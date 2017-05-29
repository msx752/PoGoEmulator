#region using directives

using System;
using PoGoEmulator.Forms;
using System.Text;
using System.Collections.Generic;
using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using System.Drawing;
using PoGoEmulator.Interfaces;

#endregion

namespace PoGoEmulator
{
    /// <summary>
    ///     The ConsoleLogger is a simple logger which writes all logs to the Console.
    /// </summary>
    internal class ConsoleLogger :ILogger    {
        // Log write event definition.
        private delegate void LogWriteHandler(object sender, LogWriteEventArgs e);

        private readonly LogLevel _maxLogLevel;
		//private ISession _session;

        /// <summary>
        ///     To create a ConsoleLogger, we must define a maximum log level.
        ///     All levels above won't be logged.
        /// </summary>
        /// <param name="maxLogLevel"></param>
        internal ConsoleLogger(LogLevel maxLogLevel)
        {
            _maxLogLevel = maxLogLevel;
        }

        public void TurnOffLogBuffering()
        {
            // No need for buffering
        }
        /// <summary>
        ///     Log a specific message by LogLevel. Won't log if the LogLevel is greater than the maxLogLevel set.
        /// </summary>
        /// <param name="message">The message to log. The current time will be prepended.</param>
        /// <param name="level">Optional. Default <see cref="LogLevel.Info" />.</param>
        /// <param name="color">Optional. Default is auotmatic</param>
        public void Write(string message, LogLevel level = LogLevel.Info, ConsoleColor color = ConsoleColor.Black)
        {
            // Remember to change to a font that supports your language, otherwise it'll still show as ???.
            Console.OutputEncoding = Encoding.UTF8;
            if (level > _maxLogLevel)
                return;

            var finalMessage = Logger.GetFinalMessage(message.Replace("NecroBot", "RocketBot"), level, color);
            Console.WriteLine(finalMessage);

            // Fire log write event.
            OnLogWrite?.Invoke(this, new LogWriteEventArgs { Message = finalMessage, Level = level, Color = color });

            // ReSharper disable once SwitchStatementMissingSomeCases
            Color _color = new Color(); 
            //TODO: review
            
            Dictionary<LogLevel, Color> colors = new Dictionary<LogLevel, Color>()
            {
                { LogLevel.Error, Color.Red },
                { LogLevel.Response, Color.FromArgb(255, 128, 128, 0) },
                { LogLevel.Info, Color.White } ,
                { LogLevel.Debug, Color.Gray } ,
                { LogLevel.Help, Color.Gray }  ,
                { LogLevel.Success, Color.Green },
            };

            _color = colors[level];
            

            if (string.IsNullOrEmpty(color.ToString()) || color.ToString() != "Black")
            {
                _color = FromColor(color);
            }

            if (string.IsNullOrEmpty(_color.ToString())) _color = Color.White;
            

            MainForm.ColoredConsoleWrite(_color, finalMessage);
        }

        public static Color FromColor(ConsoleColor c)
        {
            switch (c)
            {
                case ConsoleColor.DarkYellow:
                    return Color.FromArgb(255, 128, 128, 0);
                default:
                    return Color.FromName(c.ToString());
            }
        }

        public void LineSelect(int lineChar = 0, int linesUp = 1)
        {
            Console.SetCursorPosition(lineChar, Console.CursorTop - linesUp);
        }

        private class LogWriteEventArgs
        {
            public string Message
            {
                get { return Message; }
                set { Message = value; }
            }
            public LogLevel Level
            {
                get { return Level; }
                set { Level = value; }
            }
            public ConsoleColor Color
            {
                get { return Color; }
                set { Color = value; }
            }
        }
        private event LogWriteHandler OnLogWrite;
    }
}