using BowlingScore.Interfaces;
using BowlingScore.Shared;
namespace BowlingScore.Services
{
    public class ScoreCalculator : IScoreCalculator
    {
        private readonly int[] _rolls;

        public ScoreCalculator(int[] rolls)
        {
            _rolls = rolls;
        }

        public int CalculateScore()
        {
            var totalScore = 0;
            for (int i = 0, frameNumber = 1; i < _rolls.Length && i + 1 < _rolls.Length && frameNumber <= 10; i += 2, frameNumber++)
            {
                var firstRollInFrame = _rolls[i];
                var secondRollInFrame = _rolls[i + 1];

                if (frameNumber == Constants.LastFrame)
                {
                    totalScore += HandleLastFrame(firstRollInFrame, secondRollInFrame);
                }
                else
                {
                    if (Constants.IsStrike(firstRollInFrame))
                    {
                        var nextRollOne = _rolls[i + 1];
                        var nextRollTwo = _rolls[i + 2];
                        totalScore += HandleStrike(nextRollOne, nextRollTwo);
                        i--;
                    }
                    else if (Constants.IsSpare(firstRollInFrame, secondRollInFrame))
                    {
                        var nextRoll = _rolls[i + 2];
                        totalScore += HandleSpare(nextRoll);
                    }
                    else
                    {
                        totalScore += HandleNormalRoll(firstRollInFrame, secondRollInFrame);
                    }
                }
            }
            return totalScore;
        }

        private int HandleLastFrame(int firstRollInFrame, int secondRollInFrame)
        {
            if (HasExtraRoll(firstRollInFrame, secondRollInFrame))
            {
                var extraRoll = GetExtraRoll();
                return firstRollInFrame + secondRollInFrame + extraRoll;
            }
            return firstRollInFrame + secondRollInFrame;
        }

        private static bool HasExtraRoll(int firstRollInFrame, int secondRollInFrame)
        {
            return Constants.IsStrike(firstRollInFrame) || Constants.IsSpare(firstRollInFrame, secondRollInFrame);
        }

        private int GetExtraRoll()
        {
            return _rolls[^1];
        }

        private static int HandleStrike(int nextRollOne, int nextRollTwo)
        {
            return Constants.MaxPinNumber + nextRollOne + nextRollTwo;
        }

        private static int HandleSpare(int nextRoll)
        {
            return Constants.MaxPinNumber + nextRoll;
        }

        private static int HandleNormalRoll(int firstRoll, int secondRoll)
        {
            return firstRoll + secondRoll;
        }
    }
}
