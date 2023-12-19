using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService
{
    /// <summary>
    /// Коды событий для журнала событий Windows
    /// </summary>
    public enum EventID
    {
        None = 0,
        Start,
        Stop,
        Pause,
        Continue,
        Exception = 1000
    }

    /// <summary>
    /// Эмуляция перечисления - статический класс с константами
    /// </summary>
    public static class EventClassID
    {
        public const int Start = 1;
        public const int Stop = 2;
        public const int Pause = 3;
        public const int Continue = 4;
        public const int Exception = 1000;
    }
}
