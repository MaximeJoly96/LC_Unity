using UnityEngine;
using System;

namespace Logging
{
    public class LogsHandler
    {
        public enum LogSeverity { Information, Warning, Error, Fatal }

        private static LogsHandler _instance;

        public static LogsHandler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LogsHandler();

                return _instance;
            }
        }

        private LogsHandler() { }

        public void LogWarning(string message)
        {
            Log(message, LogSeverity.Warning);
        }

        public void LogError(string message)
        {
            Log(message, LogSeverity.Error);
        }

        public void LogFatalError(string message)
        {
            Log(message, LogSeverity.Fatal);
        }

        public void Log(string message)
        {
            Log(message, LogSeverity.Information);
        }

        public void Log(string message, LogSeverity severity)
        {
            switch(severity)
            {
                case LogSeverity.Information:
                    Debug.Log(message);
                    break;
                case LogSeverity.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogSeverity.Error:
                    Debug.LogError(message);
                    break;
                case LogSeverity.Fatal:
                    Debug.LogException(new Exception(message));
                    break;
            }
        }
    }
}
