using System.Drawing;
using Pastel;

namespace CockroachDbWorld.Libreria
{
    public class Log
    {
        public static void Print(string msg, LogType type = LogType.Normal)
        {
            switch (type)
            {
                case LogType.Info:
                    Console.Write($"{msg.Pastel("#1B96EC")}");
                    break;

                case LogType.Warning:
                    Console.Write($"{msg.Pastel("#FF9800")}");
                    break;

                case LogType.Error:
                    Console.Write($"{msg.Pastel("#E93519")}");
                    break;

                default:
                    Console.Write($"{msg.Pastel("#BABBB9")}");
                    break;
            }
        }
    }

    public enum LogType
    {
        Normal,
        Info,
        Warning,
        Error,
    }
}
