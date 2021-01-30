using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        public int TotalScore { get; set; }
        //public Dictionary<int, int> FrameScores { get; set; }
        //public Dictionary<int, string> FrameThrows { get; set; }
        //public List<int> FrameScoreList { get; set; }
        public int[] Rolls { get; set; } // maybe make private instead of prop?
        //public int[] GroupedThrowsArray { get; set; }

        private const int MaxPinNumber = 10;
        public ScoreCalculator(int[] rolls) // dependency injection needs interface to use for mock testing
        {
            TotalScore = 0;
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

            for (int i = 0, frameNumber = 1; i < Rolls.Length && i + 1 < Rolls.Length && frameNumber <= 10; i += 2, frameNumber++)
            {
                Console.WriteLine($"Frame {frameNumber}");
                //Console.WriteLine($"Current item: [{i}]: {Rolls[i]}");
                //Console.WriteLine($"Next item:[{i + 1}]: {Rolls[i + 1]}");
                var firstRollInFrame = Rolls[i];
                var secondRollInFrame = Rolls[i + 1];
                Console.WriteLine($"[{firstRollInFrame}][{secondRollInFrame}]");

                if (frameNumber == Rules.LastFrame)
                {
                    Console.WriteLine($"Last frame: [{firstRollInFrame}] [{secondRollInFrame}]");
                    HandleLastFrame(firstRollInFrame, secondRollInFrame);
                    Console.WriteLine($"Total: {TotalScore}");
                }
                else
                {
                    if (Rules.isStrike(firstRollInFrame)) // use static isStrike()
                    {
                        Console.WriteLine("STRIKE");
                        var nextRollOne = Rolls[i + 1];
                        var nextRollTwo = Rolls[i + 2]; //could be out of bounds
                        //firstRoll = 10
                        //secondRoll = nextRoll
                        HandleStrike(nextRollOne, nextRollTwo);
                        i--;
                    }
                    else if (Rules.isSpare(firstRollInFrame, secondRollInFrame))
                    {
                        var nextRoll = Rolls[i + 2];
                        Console.WriteLine("SPARE");
                        HandleSpare(nextRoll);
                    }
                    else
                    {
                        Console.WriteLine("Normal situation");
                        HandleNormalRoll(firstRollInFrame, secondRollInFrame);
                    }
                }
            }

            //Console.WriteLine($"SCORE DISPLAY: {stringBuilder}");

            //foreach (var frameScore in FrameScores)
            //{ 
            //    Console.WriteLine($"Frame {frameScore.Key}: {frameScore.Value}");
            //}

            Console.WriteLine($"Total score: {TotalScore}");

            return TotalScore;
        }

        private void HandleLastFrame(int firstRollInFrame, int secondRollInFrame)
        {
            if (Rules.isStrike(firstRollInFrame))
            {
                // 3 throws -> getExtraThrow => return Rolls[last]
                var extraRoll = GetExtraRoll();
                var frameScore = Rules.MaxPinNumber + secondRollInFrame + extraRoll;
                TotalScore += frameScore;

                Console.WriteLine($"Total score last: {TotalScore}");
            }else if (Rules.isSpare(firstRollInFrame, secondRollInFrame))
            {
                // extra roll situation
                var extraRoll = GetExtraRoll();
                var frameScore = Rules.MaxPinNumber + extraRoll;

                TotalScore += frameScore;
            }
            else
            {
                var frameScore = firstRollInFrame + secondRollInFrame;
                TotalScore += frameScore;
            }
        }


        private int GetExtraRoll()
        {
            return Rolls[Rolls.Length - 1];
        }
        private void HandleNormalFrame()
        {

        }

        //private static bool isSpare(int firstRoll, int secondRoll)
        //{
        //    return firstRoll + secondRoll == 10;
        //}

        //private static bool isStrike(int roll)
        //{
        //    return roll == 10;
        //}
        
        private static int GetFrameScore(int throw1, int throw2)
        {
            return throw1 + throw2;
        }
        private static int GetFrameScore(int[] throws)
        {
            return throws[0] + throws[1];
        }

        private int GetNextThrow(int currentIndex)
        {
            // + 2 because we are at pair [i, i+1] and we want to get next throw, which is i+2
            return Rolls[currentIndex + 2];
        }

        private int[] GetNextTwoThrows(int currentIndex)
        {
            int nextThrowOne = Rolls[currentIndex + 2];
            int nextThrowTwo;
            if (Rules.isStrike((nextThrowOne)))
            {
                nextThrowTwo = GetNextThrow(currentIndex + 2); // +2 because I want to get next index for nextThrowOne
            }
            else
            {
                nextThrowTwo = Rolls[currentIndex + 3];
            }

            return new [] { nextThrowOne, nextThrowTwo };
        }

        private void HandleStrike(int nextRollOne, int nextRollTwo)
        {
            var frameScore = Rules.MaxPinNumber + nextRollOne + nextRollTwo;
            TotalScore += frameScore;
            //var next2Throws = GetNextTwoThrows(currentIndex);
            //var nextThrowOne = next2Throws[0];
            //var nextThrowTwo = next2Throws[1];

            //if (isStrike(nextThrowOne)) // TODO: use MaxPinNumber && rename to MaxPinNumber..
            //{
            //    nextThrowTwo = GetNextThrow(currentIndex + 2);
            //}

            //var currentScore = Rules.MaxPinNumber+ nextThrowOne + nextThrowTwo; // current score is calculated differently depending on situation: strike: score(t1 + t2) + next1 + next2, spare: score(t1 + t2) + next1, normal: score(t1 + t2)

            //// next lines can be extracted in method "saveScores... which calculates the frame score, saves the current frame score in FrameScores[currentFrame] and calculates total score in TotalScore"
            //var frameScore = TotalScore + currentScore;
            ////FrameScores.Add(currentFrame, frameScore);
            ////Console.WriteLine($"Frame {currentFrame} score: {frameScore}");
            //TotalScore += currentScore;
            Console.WriteLine($"Total SCORE: {TotalScore}");
        }

        private void HandleSpare(int nextRoll)
        {
            //GroupedThrowsArray
            //var nextThrow = GetNextThrow(currentIndex);
            //var currentScore = MaxPinNumber + nextThrow; // current score is calculated differently depending on situation: strike: score(t1 + t2) + next1 + next2, spare: score(t1 + t2) + next1, normal: score(t1 + t2)

            var frameScore = Rules.MaxPinNumber + nextRoll;
            TotalScore += frameScore;

            // next lines can be extracted in method "saveScores... which calculates the frame score, saves the current frame score in FrameScores[currentFrame] and calculates total score in TotalScore"
            //var frameScore = TotalScore + currentScore;
            //FrameScores.Add(currentFrame, frameScore);
            //Console.WriteLine($"Current score: {currentScore}");
            //TotalScore += currentScore;
            Console.WriteLine($"Total score: {TotalScore}");
        }

        private void HandleNormalRoll(int firstRoll, int secondRoll)
        {
            var frameScore = firstRoll + secondRoll; // current score is calculated differently depending on situation: strike: score(t1 + t2) + next1 + next2, spare: score(t1 + t2) + next1, normal: score(t1 + t2)

            // next lines can be extracted in method "saveScores... which calculates the frame score, saves the current frame score in FrameScores[currentFrame] and calculates total score in TotalScore"
            //FrameScores.Add(currentFrame, frameScore);
            //Console.WriteLine($"Current score: {currentScore}");
            TotalScore += frameScore;
            Console.WriteLine($"Total score: {TotalScore}");
        }
    }
}
