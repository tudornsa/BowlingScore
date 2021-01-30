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
    public class ScoreCalculator : IScoreCalculator
    {
        //private IInputReader _inputReader;
        //private string _filePath;
        //public int TotalScore { get; set; }
        //public Dictionary<int, int> FrameScores { get; set; }
        //public Dictionary<int, string> FrameThrows { get; set; }
        //public List<int> FrameScoreList { get; set; }
        public int[] Rolls { get; set; } // maybe make private instead of prop?
        //public int[] GroupedThrowsArray { get; set; }

        private const int MaxPinNumber = 10;
        public ScoreCalculator(int[] rolls) // dependency injection needs interface to use for mock testing
        {
            //TotalScore = 0;
            Rolls = rolls;
            //_filePath = filePath;
            //_inputReader = inputReader;
            //Rolls = _inputReader.P(_filePath); // OK? // TODO:RENAMe to rolls
            //ExtractThrowValues(); // TODO: MOve this if necessary
            //FrameScores = new Dictionary<int, int>();
        }

        public int CalculateScore()
        {
            // do sth like for( i,,.....) -> TotalScore += CalculateFrameScore(i) &&&& StringBuilder += BuildFrame(i)
            // var stringBuilder = "|";
            var totalScore = 0;
            for (int i = 0, frameNumber = 1; i < Rolls.Length && i + 1 < Rolls.Length && frameNumber <= 10; i += 2, frameNumber++)
            {
                Console.WriteLine($"Frame {frameNumber}");
                var firstRollInFrame = Rolls[i];
                var secondRollInFrame = Rolls[i + 1];
                Console.WriteLine($"[{firstRollInFrame}][{secondRollInFrame}]");

                if (frameNumber == Rules.LastFrame)
                {
                    Console.WriteLine($"Last frame: [{firstRollInFrame}] [{secondRollInFrame}]");
                    totalScore += HandleLastFrame(firstRollInFrame, secondRollInFrame);
                    //Console.WriteLine($"Total: {TotalScore}");
                }
                else
                {
                    if (Rules.isStrike(firstRollInFrame))
                    {
                        Console.WriteLine("STRIKE");
                        var nextRollOne = Rolls[i + 1];
                        var nextRollTwo = Rolls[i + 2];
                        totalScore += HandleStrike(nextRollOne, nextRollTwo);
                        i--;
                    }
                    else if (Rules.isSpare(firstRollInFrame, secondRollInFrame))
                    {
                        var nextRoll = Rolls[i + 2];
                        Console.WriteLine("SPARE");
                        totalScore += HandleSpare(nextRoll);
                    }
                    else
                    {
                        Console.WriteLine("Normal situation");
                        totalScore += HandleNormalRoll(firstRollInFrame, secondRollInFrame);
                    }
                }
            }
            //Console.WriteLine($"Total score: {TotalScore}");
            return totalScore;
        }

        private int HandleLastFrame(int firstRollInFrame, int secondRollInFrame)
        {
            if (Rules.isStrike(firstRollInFrame))
            {
                // extra roll situation
                // 3 throws -> getExtraThrow => return Rolls[last]
                var extraRoll = GetExtraRoll();
                var frameScore = Rules.MaxPinNumber + secondRollInFrame + extraRoll;
                return frameScore;
                //TotalScore += frameScore;

                //Console.WriteLine($"Total score last: {TotalScore}");
            }
            else if (Rules.isSpare(firstRollInFrame, secondRollInFrame))
            {
                // extra roll situation
                var extraRoll = GetExtraRoll();
                var frameScore = Rules.MaxPinNumber + extraRoll;
                return frameScore;
                //TotalScore += frameScore;
            }
            else
            {
                var frameScore = firstRollInFrame + secondRollInFrame;
                return frameScore;
                //TotalScore += frameScore;
            }
        }

        private int GetExtraRoll()
        {
            return Rolls[Rolls.Length - 1];
        }

        private int HandleStrike(int nextRollOne, int nextRollTwo)
        {
            var frameScore = Rules.MaxPinNumber + nextRollOne + nextRollTwo;
            return frameScore;
            //TotalScore += frameScore;
        }

        private int HandleSpare(int nextRoll)
        {
            var frameScore = Rules.MaxPinNumber + nextRoll;
            return frameScore;
            //TotalScore += frameScore;
        }

        private int HandleNormalRoll(int firstRoll, int secondRoll)
        {
            var frameScore = firstRoll + secondRoll;
            return frameScore;
            //TotalScore += frameScore;
        }
    }
}
