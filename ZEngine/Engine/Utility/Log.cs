using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace ZEngine.Engine.Utility
{
    public enum LogType
    {
        Trace = 5,
        Debug = 4,
        Info = 3,
        Warning = 2,
        Error = 1,
        Fatal = 0
    }

    /// <summary>
    /// Inherit from this class to define custom logging categories
    /// </summary>
    public static class DebugLogCategories
    {
        public const string Engine = "ENGINE";

    }

    public class Log
    {
        private readonly ConcurrentQueue<Tuple<string, string, string, LogType>> _queue;
        private static Log Instance { get; } = new();

        private delegate void ProcessElement(Tuple<string, string, string, LogType> element);

        private event ProcessElement OnProcessElement;

        private bool _printToConsole;

        public static bool PrintToConsole
        {
            get => Instance._printToConsole;
            set
            {
                lock (Instance)
                {
                    if (value && !Instance._printToConsole)
                        Instance.OnProcessElement += ProcessToConsole;
                    else if (!value && Instance._printToConsole)
                        Instance.OnProcessElement -= ProcessToConsole;
                    Instance._printToConsole = value;
                }
            }
        }

#if DEBUG
        private int _maxLogLevel = (int)LogType.Trace;
#else
        private int _maxLogLevel = (int)LogType.Info;
#endif

        public static LogType MaxLogLevel
        {
            get => (LogType)Instance._maxLogLevel;
            set
            {
                lock (Instance)
                {
                    Instance._maxLogLevel = (int)value;
                }
            }
        }

        private Log()
        {
            _queue = new ConcurrentQueue<Tuple<string, string, string, LogType>>();
        }
        
        public static void Trace(string message, string category = "")
        {
#if DEBUG
            if (MaxLogLevel >= LogType.Trace)
            {
                Instance._queue.Enqueue(new Tuple<string, string, string, LogType>(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message, category, LogType.Trace));
            }
#endif
        }

        public static void Debug(string message, string category = "")
        {
#if DEBUG
            if (MaxLogLevel >= LogType.Debug)
            {
                Instance._queue.Enqueue(new Tuple<string, string, string, LogType>(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message, category, LogType.Debug));
            }
#endif
        }
        
        public static void Info(string message, string category = "")
        {
            if (MaxLogLevel >= LogType.Info)
            {
                Instance._queue.Enqueue(new Tuple<string, string, string, LogType>(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message, category, LogType.Info));
            }
        }
        
        public static void Warning(string message, string category = "")
        {
            if (MaxLogLevel >= LogType.Warning)
            {
                Instance._queue.Enqueue(new Tuple<string, string, string, LogType>(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message, category, LogType.Warning));
            }
            
        }
        
        public static void Error(string message, string category = "")
        {
            if (MaxLogLevel >= LogType.Error)
            {
                Instance._queue.Enqueue(new Tuple<string, string, string, LogType>(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message, category, LogType.Error));
            }
        }
        
        public static void Fatal(string message, string category = "")
        {
            if (MaxLogLevel >= LogType.Fatal)
            {
                Instance._queue.Enqueue(new Tuple<string, string, string, LogType>(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message, category, LogType.Fatal));
            }
        }

        /// <summary>
        /// ONLY CALL FROM MAIN THREAD
        /// </summary>
        internal static void FlushQueue()
        {
            while (!Instance._queue.IsEmpty)
            {
                if (Instance._queue.TryDequeue(out var element))
                    Instance.OnProcessElement(element);
            }
        }


        private static void ProcessToConsole(Tuple<string, string, string, LogType> element)
        {
            var (timestamp, message, category, type) = element;
            Console.ForegroundColor = type switch
            {
                LogType.Trace => ConsoleColor.Cyan,
                LogType.Debug => ConsoleColor.White,
                LogType.Info => ConsoleColor.Gray,
                LogType.Warning => ConsoleColor.Yellow,
                LogType.Error => ConsoleColor.DarkRed,
                LogType.Fatal => ConsoleColor.Magenta,
                _ => ConsoleColor.White
            };
            if (category != string.Empty)
            {
                Console.WriteLine($"[{timestamp}][{type.ToString().ToUpperInvariant()}][{category}]{message}");
            }
            else
            {
                Console.WriteLine($"[{timestamp}][{type.ToString().ToUpperInvariant()}]{message}");
            }
            
            Console.ResetColor();
        }
    }
}