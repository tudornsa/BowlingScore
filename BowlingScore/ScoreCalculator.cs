using System;
using System.Collections.Generic;

/*
 * | f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |
   |-, 3|5, -|9, /|2, 5|3, 2|4, 2|3, 3|4, /|X   |X, 2, 5|
   score: 103

   | f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |
   |7, 1|5, /|2, 7|4, /|-, 5|8, /|8, 1|4, 3|2, 4|5, 2   |
   score: 91
   
 */

namespace BowlingScore
{
    public class ScoreCalculator
    {
        private IInputReader _inputReader;
        private string _filePath;
        public int TotalScore { get; set; }
        //public Dictionary<int, int> FrameScores { get; set; }
        //public Dictionary<int, string> FrameThrows { get; set; }
        //public List<int> FrameScoreList { get; set; }
        public string[] ThrowsArray { get; set; } // TODO: REname to roll
        public int[] GroupedThrowsArray { get; set; }

        private const int MaxScore = 10;
        public ScoreCalculator(IInputReader inputReader, string filePath) // dependency injection needs interface to use for mock testing
        {
            TotalScore = 0;
            _filePath = filePath;
            _inputReader = inputReader;
            ThrowsArray = _inputReader.GetThrowsArray(_filePath); // OK? // TODO:RENAMe to rolls
            ExtractThrowValues(); // TODO: MOve this if necessary
            //FrameScores = new Dictionary<int, int>();
        }

        public int CalculateScore()
        {
            Console.Write("New array: [ ");
            foreach (var throwValue in GroupedThrowsArray)
            {
                Console.Write($"{throwValue}, ");
            }

            Console.WriteLine(" ]");

            var stringBuilder = "|";

            for (int i = 0, frameNumber = 1; i < GroupedThrowsArray.Length && i + 1 < GroupedThrowsArray.Length && frameNumber <= 10; i += 2, frameNumber++)
            {
                if (frameNumber == 10)
                {
                    if (i + 2 > GroupedThrowsArray.Length - 1) // 2 throws in 10th
                    {
                        stringBuilder += $" {GroupedThrowsArray[i]}, {GroupedThrowsArray[i + 1]}   |";
                    }
                    else // 3 throws
                    {
                        stringBuilder += $" {GroupedThrowsArray[i]}, {GroupedThrowsArray[i+1]}, {GroupedThrowsArray[i+2]} |";
                    }
                }
                else
                {
                    stringBuilder += $" {GroupedThrowsArray[i]}, {GroupedThrowsArray[i + 1]} |";
                }
                Console.WriteLine($"Frame {frameNumber}");
                Console.WriteLine($"Current item: [{i}]: {GroupedThrowsArray[i]}");
                Console.WriteLine($"Next item:[{i + 1}]: {GroupedThrowsArray[i + 1]}");
                var firstThrow = GroupedThrowsArray[i];
                var secondThrow = GroupedThrowsArray[i + 1];
                Console.WriteLine($"[{firstThrow}][{secondThrow}]");

                if (firstThrow == 10) // use static isStrike()
                {
                    Console.WriteLine("STRIKE");
                    HandleStrike(i, frameNumber);
                }
                else if ((firstThrow + secondThrow) == 10)
                {
                    Console.WriteLine("SPARE");
                    HandleSpare(i, frameNumber);
                }
                else
                {
                    Console.WriteLine("Normal situation");
                    HandleNormalThrow(firstThrow, secondThrow, frameNumber);
                }
            }

            Console.WriteLine($"SCORE DISPLAY: {stringBuilder}");

            //foreach (var frameScore in FrameScores)
            //{ 
            //    Console.WriteLine($"Frame {frameScore.Key}: {frameScore.Value}");
            //}

            Console.WriteLine($"Total score: {TotalScore}");

            return TotalScore;
        }


        private void ExtractThrowValues() // Helps a bit // TODO: rename this!
        {
            var throwsValueList = new List<int>();
            foreach (var throwValue in ThrowsArray)
            {
                throwsValueList.Add(Int32.Parse(throwValue)); // add item to int array
                if (throwValue == "10")
                {
                    throwsValueList.Add(0);
                }
            }
            GroupedThrowsArray = throwsValueList.ToArray();
        }

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
            return GroupedThrowsArray[currentIndex + 2];
        }

        private int[] GetNextTwoThrows(int currentIndex)
        {
            return new int[2] { GroupedThrowsArray[currentIndex + 2], GroupedThrowsArray[currentIndex + 3] }; // same as above
        }

        private void HandleStrike(int currentIndex, int currentFrame)
        {
            var next2Throws = GetNextTwoThrows(currentIndex);
            var nextThrowOne = next2Throws[0];
            var nextThrowTwo = next2Throws[1];

            if (nextThrowOne == 10) // TODO: use MaxScore && rename to MaxPinNumber..
            {
                nextThrowTwo = GetNextThrow(currentIndex + 2);
            }

            var currentScore = MaxScore + nextThrowOne + nextThrowTwo; // current score is calculated differently depending on situation: strike: score(t1 + t2) + next1 + next2, spare: score(t1 + t2) + next1, normal: score(t1 + t2)

            // next lines can be extracted in method "saveScores... which calculates the frame score, saves the current frame score in FrameScores[currentFrame] and calculates total score in TotalScore"
            var frameScore = TotalScore + currentScore;
            //FrameScores.Add(currentFrame, frameScore);
            Console.WriteLine($"Frame {currentFrame} score: {frameScore}");
            TotalScore += currentScore;
            Console.WriteLine($"Total SCORE: {TotalScore}");
        }

        private void HandleSpare(int currentIndex, int currentFrame)
        {
            //GroupedThrowsArray
            var nextThrow = GetNextThrow(currentIndex);
            var currentScore = MaxScore + nextThrow; // current score is calculated differently depending on situation: strike: score(t1 + t2) + next1 + next2, spare: score(t1 + t2) + next1, normal: score(t1 + t2)

            // next lines can be extracted in method "saveScores... which calculates the frame score, saves the current frame score in FrameScores[currentFrame] and calculates total score in TotalScore"
            //var frameScore = TotalScore + currentScore;
            //FrameScores.Add(currentFrame, frameScore);
            Console.WriteLine($"Current score: {currentScore}");
            TotalScore += currentScore;
            Console.WriteLine($"Total score: {TotalScore}");
        }

        private void HandleNormalThrow(int firstThrow, int secondThrow, int currentFrame)
        {
            var currentScore = GetFrameScore(firstThrow, secondThrow); // current score is calculated differently depending on situation: strike: score(t1 + t2) + next1 + next2, spare: score(t1 + t2) + next1, normal: score(t1 + t2)

            // next lines can be extracted in method "saveScores... which calculates the frame score, saves the current frame score in FrameScores[currentFrame] and calculates total score in TotalScore"
            var frameScore = TotalScore + currentScore;
            //FrameScores.Add(currentFrame, frameScore);
            //Console.WriteLine($"Current score: {currentScore}");
            TotalScore = frameScore;
            Console.WriteLine($"Total score: {TotalScore}");
        }
    }
}
