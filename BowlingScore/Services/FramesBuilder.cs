using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private const int NOEXTRAROLL = 999;
        public FramesBuilder(int[] rolls)
        {
            _rolls = rolls;
        }
        public string CreateScoreboard(int totalScore)
        {
            var rollsString = RenameThis();
            return "| f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |\n" + rollsString; // TODO
        }

        private string RenameThis()
        {
            string stringBuilder = "|";
            for (int i = 0, frameNumber = 1;
                i < _rolls.Length && i + 1 < _rolls.Length && frameNumber <= 10;
                i += 2, frameNumber++)
            {
                //if (frameNumber == 10) // DO NOT DELETE! -> Create new class + interface for this!
                //{
                //    if (ExtraRoll(_rolls[i], _rolls[i + 1]))
                //    {
                //        var extraRoll = GetExtraRoll();
                //        stringBuilder += BuildFrame(_rolls[i], _rolls[i + 1], _rolls[i + 2]);
                //    }
                //    else // 3 throws
                //    {
                //        stringBuilder += BuildFrame(_rolls[i], _rolls[i + 1]);
                //    }
                //}
                //else
                //{
                //    stringBuilder += BuildFrame(_rolls[i], _rolls[i + 1]);
                //}
                //stringBuilder += (frameNumber == 10 && ExtraRoll(_rolls[i], _rolls[i + 1])) ? 
                //    BuildFrame(_rolls[i], _rolls[i + 1], _rolls[i + 2]) : 
                //    BuildFrame(_rolls[i], _rolls[i + 1]);
                if (frameNumber == 10)
                {
                    //if (ExtraRoll(_rolls[i], _rolls[i + 1]))
                    //{
                    //    stringBuilder += BuildFrame(_rolls[i], _rolls[i + 1], _rolls[i + 2], 7);
                    //}
                    //else
                    //{
                    //    stringBuilder += BuildFrame(_rolls[i], _rolls[i + 1], 7);
                    //}
                    stringBuilder += BuildLastFrame(i, Rules.LastFrameLength);
                }
                else
                {
                    stringBuilder += BuildFrame(_rolls[i], _rolls[i + 1], Rules.NormalFrameLength);
                    if (Rules.isStrike(_rolls[i])) i--;
                }

                stringBuilder += "|";
            }

            return stringBuilder;
        }

        private string BuildLastFrame(int currentIndex, int frameLength)
        {
            return ExtraRoll(_rolls[currentIndex], _rolls[currentIndex + 1]) ?
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
                return AddPadding($"{GetSymbol(firstRoll)}, /", frameLength);
            }
            else
            {
                return AddPadding($"{GetSymbol(firstRoll)}, {GetSymbol(secondRoll)}", frameLength);
            }
        }

        private static string AddPadding(string str, int totalLength)
        {
            return str.PadRight(totalLength, ' ');
        }

        private string BuildFrame(int firstRoll, int secondRoll, int thirdRoll, int frameLength)
        {
            if (Rules.isStrike(firstRoll))
            {
                if (Rules.isSpare(secondRoll, thirdRoll))
                {
                    return AddPadding($"{GetSymbol(firstRoll)}, {GetSymbol(secondRoll)}, /", frameLength);
                }
                return AddPadding($"{GetSymbol(firstRoll)}, {GetSymbol(secondRoll)}, {GetSymbol(thirdRoll)}", frameLength);
            }
            else if (Rules.isSpare(firstRoll, secondRoll))
            {
                return AddPadding($"{GetSymbol(firstRoll)}, /, {GetSymbol(thirdRoll)}", frameLength);
            }
            else
            {
                return AddPadding($"{GetSymbol(firstRoll)}, {GetSymbol(secondRoll)}, {thirdRoll}", frameLength);
            }
            //return $"{firstRoll}, {secondRoll}, {thirdRoll}";
        }

        private static bool ExtraRoll(int firstRoll, int secondRoll) =>
            Rules.isStrike(firstRoll) || Rules.isSpare(firstRoll, secondRoll);

        private int GetExtraRoll()
        {
            return _rolls[_rolls.Length - 1];
        }
        private string GetLastFrame(int firstRoll, int secondRoll)
        {
            return $"{firstRoll}, {secondRoll}   ";
        }

        private string GetSymbol(int roll)
        {
            if (roll == 10) // Do this using MAXROLLS or whatever
            {
                return "X";
            } else if (roll == 0)
            {
                return "-";
            }
            else
            {
                return roll.ToString();
            }
        }
    }
}
