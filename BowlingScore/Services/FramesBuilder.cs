using System;
using BowlingScore.Shared;

/*
 * | f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |
   |-, 3|5, -|9, /|2, 5|3, 2|4, 2|3, 3|4, /|X   |X, 2, 5|
   score: 103

   | f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |
   |7, 1|5, /|2, 7|4, /|-, 5|8, /|8, 1|4, 3|2, 4|5, 2   |
   score: 91
   
 */

namespace BowlingScore.Services
{
    public class FramesBuilder : IFramesBuilder
    {
        private int[] _rolls;
        public FramesBuilder(int[] rolls)
        {
            _rolls = rolls;
        }
        public string CreateScoreboard(int totalScore)
        {
            var scoreBoard = GetRolls();
            return Rules.ScoreBoardHeader + Environment.NewLine + scoreBoard;
        }

        private string GetRolls()
        {
            string rolls = "|";
            for (int i = 0, frameNumber = 1;
                i < _rolls.Length && i + 1 < _rolls.Length && frameNumber <= 10;
                i += 2, frameNumber++)
            {
                if (frameNumber == 10)
                {
                    rolls += BuildLastFrame(i, Rules.LastFrameLength);
                }
                else
                {
                    rolls += BuildFrame(_rolls[i], _rolls[i + 1], Rules.NormalFrameLength);
                    if (Rules.isStrike(_rolls[i])) i--;
                }
                rolls += "|";
            }
            return rolls;
        }

        private string BuildLastFrame(int currentIndex, int frameLength)
        {
            return hasExtraRoll(_rolls[currentIndex], _rolls[currentIndex + 1]) ?
                BuildFrame(_rolls[currentIndex], _rolls[currentIndex + 1], _rolls[currentIndex + 2], frameLength) :
                BuildFrame(_rolls[currentIndex], _rolls[currentIndex + 1], frameLength);
        }

        private string BuildFrame(int firstRoll, int secondRoll, int frameLength)
        {
            if (Rules.isStrike(firstRoll))
            {
                return AddPadding($"{GetSymbol(firstRoll)}", frameLength);
            }
            else if (Rules.isSpare(firstRoll, secondRoll))
            {
                return AddPadding($"{GetSymbol(firstRoll)}, {Rules.SpareSymbol}", frameLength);
            }
            else
            {
                return AddPadding($"{GetSymbol(firstRoll)}, {GetSymbol(secondRoll)}", frameLength);
            }
        }


        private string BuildFrame(int firstRoll, int secondRoll, int thirdRoll, int frameLength)
        {
            if (Rules.isStrike(firstRoll))
            {
                if (Rules.isSpare(secondRoll, thirdRoll))
                {
                    return AddPadding($"{GetSymbol(firstRoll)}, {GetSymbol(secondRoll)}, {Rules.SpareSymbol}", frameLength);
                }
                return AddPadding($"{GetSymbol(firstRoll)}, {GetSymbol(secondRoll)}, {GetSymbol(thirdRoll)}", frameLength);
            }
            else if (Rules.isSpare(firstRoll, secondRoll))
            {
                return AddPadding($"{GetSymbol(firstRoll)}, {Rules.SpareSymbol}, {GetSymbol(thirdRoll)}", frameLength);
            }
            else
            {
                return AddPadding($"{GetSymbol(firstRoll)}, {GetSymbol(secondRoll)}, {GetSymbol(thirdRoll)}", frameLength);
            }
        }

        private static string AddPadding(string str, int totalLength)
        {
            return str.PadRight(totalLength, ' ');
        }

        private static bool hasExtraRoll(int firstRoll, int secondRoll) =>
            Rules.isStrike(firstRoll) || Rules.isSpare(firstRoll, secondRoll);

        private string GetSymbol(int roll)
        {
            if (roll == Rules.MaxPinNumber)
            {
                return Rules.StrikeSymbol;
            } else if (roll == 0)
            {
                return Rules.ZeroSymbol;
            }
            else
            {
                return roll.ToString();
            }
        }
    }
}
