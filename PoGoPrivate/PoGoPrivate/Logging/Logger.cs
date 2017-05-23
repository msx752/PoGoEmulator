using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PoGoPrivate.Enums;
using PoGoPrivate.Interfaces;

namespace PoGoPrivate.Logging
{
    public static class Logger
    {
        private static List<ILogger> _loggers = new List<ILogger>();

        private static ConcurrentQueue<string> LogbufferList = new ConcurrentQueue<string>();
        private static string _lastLogMessage;

        /// <summary>
        /// Add a logger. 
        /// </summary>
        /// <param name="logger">
        /// </param>
        public static void AddLogger(ILogger logger, string subPath = "", bool isGui = false)
        {
            if (!_loggers.Contains(logger))
                _loggers.Add(logger);
        }

        public static void Debug(string message, Exception ex = null)
        {
#if DEBUG
            Write(message, color: ConsoleColor.DarkRed);
            if (ex != null)
            {
                Write(ex.Message, color: ConsoleColor.DarkRed);
            }
#endif
        }

        /// <summary>
        /// Log a specific message to the logger setup by <see cref="SetLogger(ILogger)" /> . 
        /// </summary>
        /// <param name="message">
        /// The message to log. 
        /// </param>
        /// <param name="level">
        /// Optional level to log. Default <see cref="System.LogLevel.Info" />. 
        /// </param>
        /// <param name="color">
        /// Optional. Default is automatic color. 
        /// </param>
        public static void Write(string message, LogLevel level = LogLevel.Info,
            ConsoleColor color = ConsoleColor.Black, bool force = false)
        {
            if (_loggers.Count == 0/* || _lastLogMessage == message*/)
                return;

            _lastLogMessage = message;
            foreach (var logger in _loggers)
                logger?.Write(message, level, color);
        }

        public static void LineSelect(int lineChar = 0, int linesUp = 1)
        {
            foreach (var logger in _loggers)
                logger?.LineSelect(lineChar, linesUp);
        }

        public static string GetFinalMessage(string message, LogLevel level, ConsoleColor color)
        {
            switch (level)
            {
                case LogLevel.Error:
                    Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.Red : color;
                    break;

                case LogLevel.Response:
                    Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.DarkYellow : color;
                    break;

                case LogLevel.Info:
                    Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.White : color;
                    break;

                case LogLevel.Debug:
                    Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.Gray : color;
                    break;

                case LogLevel.Help:
                    Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.Gray : color;
                    break;

                case LogLevel.Success:
                    Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.Green : color;
                    break;

                default:
                    Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.White : color;
                    break;
            }

            string finalMessage = $"[{DateTime.Now:HH:mm:ss}] ({level.ToString().ToUpper()}) {message}";
            return finalMessage;
        }

        public static string GetHexColor(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Black:
                    return "#002b36";

                case ConsoleColor.Blue:
                    return "#268bd2";

                case ConsoleColor.Cyan:
                    return "#2aa198";

                case ConsoleColor.DarkBlue:
                    return "#000080";

                case ConsoleColor.DarkCyan:
                    return "#008B8B";

                case ConsoleColor.DarkGray:
                    return "#586e75";

                case ConsoleColor.DarkGreen:
                    return "#008000";

                case ConsoleColor.DarkMagenta:
                    return "#800080";

                case ConsoleColor.DarkRed:
                    return "#800000";

                case ConsoleColor.DarkYellow:
                    return "#808000";

                case ConsoleColor.Gray:
                    return "#93a1a1";

                case ConsoleColor.Green:
                    return "#859900";

                case ConsoleColor.Magenta:
                    return "#d33682";

                case ConsoleColor.Red:
                    return "#dc322f";

                case ConsoleColor.White:
                    return "#fdf6e3";

                case ConsoleColor.Yellow:
                    return "#b58900";

                default:
                    // Grey
                    return "#93a1a1";
            }
        }
    }
}