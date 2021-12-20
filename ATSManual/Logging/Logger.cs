using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSManual.Logging
{
    public static class Logger
    {
        public enum MessageType
        {
            Info,
            Warning,
            Error,
            System,
            Debug,
        }

        public static void Log(object content, MessageType type = MessageType.Info)
        {
            var msgTime = DateTime.Now;
            var msgType = "Инфо";
            switch (type)
            {
                case MessageType.Debug:
                    msgType = "Отладка";
                    break;
                case MessageType.Error:
                    msgType = "Ошибка";
                    break;
                case MessageType.Warning:
                    msgType = "Внимание";
                    break;
                case MessageType.System:
                    msgType = "Cистема";
                    break;
            }


            Console.WriteLine($"[{msgTime.ToShortDateString()} {msgTime.ToShortTimeString()} {msgType}] {content}");
        }
    }
}
