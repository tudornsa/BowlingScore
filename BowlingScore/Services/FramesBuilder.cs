using System;
using System.Text;
using BowlingScore.Interfaces;
using BowlingScore.Shared;

namespace BowlingScore.Services
{
    public class FramesBuilder : IFramesBuilder
    {
        private readonly int[] _rolls;
        public FramesBuilder(int[] rolls)
        {
            _rolls = rolls;
        }

        public string CreateScoreboard(int totalScore)
        {
            StringBuilder scoreBoard = new StringBuilder(Constants.ScoreBoardHeader);
            var frames = CreateFrames();
            scoreBoard.Append(Environment.NewLine);
            scoreBoard.Append(frames);
            scoreBoard.Append(Environment.NewLine);
            scoreBoard.Append($"score: {totalScore}");

            return scoreBoard.ToString();
        }

        private StringBuilder CreateFrames()
        {
            StringBuilder rolls = new StringBuilder(Constants.Separator);
            for (int i = 0, frameNumber = 1; i < _rolls.Length && i + 1 < _rolls.Length && frameNumber <= 10; i += 2, frameNumber++)
            {
                if (frameNumber == Constants.LastFrame)
                {
                    rolls.Append(BuildLastFrame(i, Constants.LastFrameLength));
                }
                else
                {
                    rolls.Append(BuildFrame(_rolls[i], _rolls[i + 1], Constants.NormalFrameLength));
                    if (Constants.IsStrike(_rolls[i])) i--;
                }
                rolls.Append(Constants.Separator);
            }
            return rolls;
        }

        private string BuildLastFrame(int currentIndex, int padding)
        {
            return HasExtraRoll(_rolls[currentIndex], _rolls[currentIndex + 1]) ?
                BuildFrame(_rolls[currentIndex], _rolls[currentIndex + 1], _rolls[currentIndex + 2], padding) :
                BuildFrame(_rolls[currentIndex], _rolls[currentIndex + 1], padding);
        }

        private string BuildFrame(int firstRoll, int secondRoll, int frameLength)
        {
            if (Constants.IsStrike(firstRoll))
            {
                return AddPadding($"{GetSymbol(firstRoll)}", frameLength);
            }
            if (Constants.IsSpare(firstRoll, secondRoll))
            {
                return AddPadding($"{GetSymbol(firstRoll)}, {Constants.SpareSymbol}", frameLength);
            }
            return AddPadding($"{GetSymbol(firstRoll)}, {GetSymbol(secondRoll)}", frameLength);
        }

        private string BuildFrame(int firstRoll, int secondRoll, int thirdRoll, int padding)
        {
            if (Constants.IsStrike(firstRoll) && Constants.IsSpare(secondRoll, thirdRoll))
            {
                return AddPadding($"{GetSymbol(firstRoll)}, {GetSymbol(secondRoll)}, {Constants.SpareSymbol}", padding);
            }
            if (Constants.IsSpare(firstRoll, secondRoll))
            {
                return AddPadding($"{GetSymbol(firstRoll)}, {Constants.SpareSymbol}, {GetSymbol(thirdRoll)}", padding);
            }
            return AddPadding($"{GetSymbol(firstRoll)}, {GetSymbol(secondRoll)}, {GetSymbol(thirdRoll)}", padding);
        }

        private static string AddPadding(string str, int totalLength)
        {
            return str.PadRight(totalLength, ' ');
        }

        private static bool HasExtraRoll(int firstRoll, int secondRoll) =>
            Constants.IsStrike(firstRoll) || Constants.IsSpare(firstRoll, secondRoll);

        private string GetSymbol(int roll)
        {
            if (roll == Constants.MaxPinNumber)
            {
                return Constants.StrikeSymbol;
            }
            if (roll == 0)
            {
                return Constants.ZeroSymbol;
            }
            return roll.ToString();
        }
    }
}
