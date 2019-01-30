using System;
using System.Collections.Concurrent;

namespace ZEngine.Engine.Utility
{
    public enum LogType
    {
        Info,
        Warning,
        Error
    }

    public static class DebugCategories
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

        private Debug()
        {
            _queue = new ConcurrentQueue<Tuple<string, string, LogType>>();
        }

        public static void Log(string message, string category = "")
        {
            Instance._queue.Enqueue(new Tuple<string, string, LogType>(message, category, LogType.Info));
        }

        public static void LogWarning(string message, string category = "")
        {
            Instance._queue.Enqueue(new Tuple<string, string, LogType>(message, category, LogType.Warning));
        }

        public static void LogError(string message, string category = "")
        {
            Instance._queue.Enqueue(new Tuple<string, string, LogType>(message, category, LogType.Error));
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

        private static void ProcessToConsole(Tuple<string, string, LogType> element)
        {
            switch (element.Item3)
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
            }

            Console.WriteLine($"[{element.Item3.ToString().ToUpperInvariant()}][{element.Item2}]{element.Item1}");
            Console.ResetColor();
        }
    }
}