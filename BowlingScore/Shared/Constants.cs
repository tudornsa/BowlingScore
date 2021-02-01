namespace BowlingScore.Shared
{
    public class Constants
    {
        public const int MaxPinNumber = 10;
        public const int LastFrame = 10;
        public const int LastFrameLength = 7;
        public const int NormalFrameLength = 4;
        public const string SpareSymbol = "/";
        public const string StrikeSymbol = "X";
        public const string ZeroSymbol = "-";
        public const string ScoreBoardHeader = "| f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |";
        public const string Separator = "|";
        public static bool IsSpare(int firstRoll, int secondRoll) => firstRoll + secondRoll == MaxPinNumber;
        public static bool IsStrike(int firstRoll) => firstRoll == MaxPinNumber;
    }
}
