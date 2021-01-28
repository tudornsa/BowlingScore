using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private InputReader _inputReader;
        private string _filePath;
        public int TotalScore { get; set; }
        public Dictionary<int, int> FrameScores { get; set; }
        public Dictionary<int, string> FrameThrows { get; set; }
        public List<int> FrameScoreList { get; set; }
        public string[] ThrowsArray { get; set; }
        public int[] GroupedThrowsArray { get; set; }

        const int MaxScore = 10;
        public ScoreCalculator(InputReader inputReader, string filePath) // dependency injection needs interface to use for mock testing
        {
            TotalScore = 0;
            _filePath = filePath;
            _inputReader = inputReader;
            ThrowsArray = _inputReader.GetThrowsArray(_filePath);
            FrameScores = new Dictionary<int, int>();
        }

        public int CalculateScore()
        {
            ExtractThrowValues(); // am I doing this correctly? should/can I call this in constructor?
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

                if (firstThrow == 10) // TODO: case when next 2 vals are 10
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
                    HandleNormalThrows(firstThrow, secondThrow, frameNumber);
                }
            }

            Console.WriteLine($"SCORE DISPLAY: {stringBuilder}");

            foreach (var frameScore in FrameScores)
            { 
                Console.WriteLine($"Frame {frameScore.Key}: {frameScore.Value}");
            }

            Console.WriteLine($"Total score: {TotalScore}");

            return TotalScore;
        }


        private void ExtractThrowValues() // Helps a bit
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

            if (nextThrowOne == 10)
            {
                nextThrowTwo = GetNextThrow(currentIndex + 2);
            }

            var currentScore = MaxScore + nextThrowOne + nextThrowTwo; // current score is calculated differently depending on situation: strike: score(t1 + t2) + next1 + next2, spare: score(t1 + t2) + next1, normal: score(t1 + t2)

            // next lines can be extracted in method "saveScores... which calculates the frame score, saves the current frame score in FrameScores[currentFrame] and calculates total score in TotalScore"
            var frameScore = TotalScore + currentScore;
            FrameScores.Add(currentFrame, frameScore);
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
            var frameScore = TotalScore + currentScore;
            FrameScores.Add(currentFrame, frameScore);
            Console.WriteLine($"Current score: {currentScore}");
            TotalScore += currentScore;
            Console.WriteLine($"Total score: {TotalScore}");
        }

        private void HandleNormalThrows(int firstThrow, int secondThrow, int currentFrame)
        {
            var currentScore = GetFrameScore(firstThrow, secondThrow); // current score is calculated differently depending on situation: strike: score(t1 + t2) + next1 + next2, spare: score(t1 + t2) + next1, normal: score(t1 + t2)

            // next lines can be extracted in method "saveScores... which calculates the frame score, saves the current frame score in FrameScores[currentFrame] and calculates total score in TotalScore"
            var frameScore = TotalScore + currentScore;
            FrameScores.Add(currentFrame, frameScore);
            Console.WriteLine($"Current score: {currentScore}");
            TotalScore = frameScore;
            Console.WriteLine($"Total score: {TotalScore}");
        }

        //private void RecordThrows(int firstThrow, int secondThrow, int currentFrame)
        //{
        //    Console.Write("|");
        //    for (var i = 1; i < ThrowsArray; i++)
        //    {
        //        Console.Write($" {firstThrow}, {secondThrow} |");
        //    }

        //    Console.WriteLine($" {}");
        //    if (currentFrame == 10)
        //    {
        //        //handle this bish
        //    }
        //    else
        //    {
        //        if(firstThrow == 10)
        //        FrameThrows.Add(currentFrame, $"{firstThrow}, {secondThrow}");
        //    }
        //}
    }
}
