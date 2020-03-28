using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace ZEngine.Engine.Utility
{
    public enum LogType
    {
        Info,
        Warning,
        Error
    }

    /// <summary>
    /// Inherit from this class to define custom logging categories
    /// </summary>
    public static class DebugLogCategories
    {
        public const string Engine = "ENGINE";

    }

    public class Debug
    {
        private readonly ConcurrentQueue<Tuple<string, string, LogType>> _queue;
        private static Debug Instance { get; } = new Debug();

        public delegate void ProcessElement(Tuple<string, string, LogType> element);

        public event ProcessElement OnProcessElement;

        private bool _printToConsole;

        public static bool PrintToConsole
        {
            get => Instance._printToConsole;
            set
            {
#if DEBUG

                lock (Instance)
                {
                    if (value && !Instance._printToConsole)
                        Instance.OnProcessElement += ProcessToConsole;
                    else if (!value && Instance._printToConsole)
                        Instance.OnProcessElement -= ProcessToConsole;
                    Instance._printToConsole = value;
                }
#endif
            }
        }

        private Debug()
        {
#if DEBUG

            _queue = new ConcurrentQueue<Tuple<string, string, LogType>>();
#endif
        }

        [Conditional("DEBUG")]
        public static void Log(string message, string category = "")
        {
            Instance._queue.Enqueue(new Tuple<string, string, LogType>(message, category, LogType.Info));
        }

        [Conditional("DEBUG")]
        public static void LogWarning(string message, string category = "")
        {
            Instance._queue.Enqueue(new Tuple<string, string, LogType>(message, category, LogType.Warning));
        }

        [Conditional("DEBUG")]
        public static void LogError(string message, string category = "")
        {
            Instance._queue.Enqueue(new Tuple<string, string, LogType>(message, category, LogType.Error));
        }

        /// <summary>
        /// ONLY CALL FROM MAIN THREAD
        /// </summary>
        [Conditional("DEBUG")]
        internal static void FlushQueue()
        {
            while (!Instance._queue.IsEmpty)
            {
                if (Instance._queue.TryDequeue(out var element))
                    Instance.OnProcessElement(element);
            }
        }


        private static void ProcessToConsole(Tuple<string, string, LogType> element)
        {
            var (message, category, type) = element;
            switch (type)
            {
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.WriteLine($"[{type.ToString().ToUpperInvariant()}][{category}]{message}");
            Console.ResetColor();
        }
    }
}