using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScore.Shared
{
    public class Rules
    {
        public const int MaxPinNumber = 10;
        public const int LastFrame = 10;
        public const int LastFrameLength = 7;
        public const int NormalFrameLength = 4;
        public static bool isSpare(int firstThrow, int secondThrow) => firstThrow + secondThrow == MaxPinNumber;
        public static bool isStrike(int firstThrow) => firstThrow == MaxPinNumber;

    }
}
