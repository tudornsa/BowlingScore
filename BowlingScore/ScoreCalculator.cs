using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingScore
{
    public class ScoreCalculator
    {
        private InputReader _inputReader;
        private string _filePath;
        public int TotalScore { get; set; }
        public List<int> FrameScoreList { get; set; }
        public string[] ThrowArray { get; set; }
        public int[] GroupedThrowsArray { get; set; }

        const int MaxScore = 10;
        public ScoreCalculator(InputReader inputReader, string filePath) // dependency injection needs interface to use for mock testing
        {
            _filePath = filePath;
            _inputReader = inputReader;
            ThrowArray = _inputReader.GetThrowArray(_filePath);
        }

        public int CalculateScore()
        {
            GroupedThrowsArray = ExtractScores(ThrowArray); // should I use this or use var GroupedThrowsArrayay = ExtractScores(,,)
            Console.Write("New array: [ ");
            foreach (var throwValue in GroupedThrowsArray)
            {
                Console.Write($"{throwValue}, ");
            }

            Console.WriteLine(" ]");
            //foreach (var item in GroupedThrowsArray)
            //{
            //    Console.WriteLine($"Item: {item}");
            //}
            //var frameNr = 1;
            for (int i = 0, frameNumber = 1; i < GroupedThrowsArray.Length && i + 1 < GroupedThrowsArray.Length && frameNumber <= 10; i += 2, frameNumber++)
            {
                Console.WriteLine($"Frame {frameNumber}");
                Console.WriteLine($"Current item: [{i}]: {GroupedThrowsArray[i]}");
                Console.WriteLine($"Next item:[{i+1}]: {GroupedThrowsArray[i+1]}");
                if (i + 1 == GroupedThrowsArray.Length) // extra throw situation >>> does it fall under frame = 11? and if so is this check even needed?
                {
                    break;
                }
                else
                {
                    var firstThrow = GroupedThrowsArray[i]; // do smth like static GetIntFromString(str)
                    var secondThrow = GroupedThrowsArray[i + 1];
                    Console.WriteLine($"[{firstThrow}][{secondThrow}]");

                    if (firstThrow == 10) // TODO: case when next 2 vals are 10
                    {
                        Console.WriteLine("STRIKE");
                        HandleStrike(GroupedThrowsArray, i);
                    }
                    else if ((firstThrow + secondThrow) == 10)
                    {
                        Console.WriteLine("SPARE");
                        var nextThrow = GetNextThrow(i, GroupedThrowsArray);
                        var currentScore = MaxScore + nextThrow;
                        Console.WriteLine($"Current score: {currentScore}");
                        TotalScore += currentScore;
                        Console.WriteLine($"Total score: {TotalScore}");
                    }
                    else
                    {
                        var currentScore = firstThrow + secondThrow;
                        Console.WriteLine($"Current score: {currentScore}");
                        TotalScore += currentScore;
                        Console.WriteLine($"Total score: {TotalScore}");
                    }
                }
            }
            return TotalScore;
        }

        private static int[] ExtractScores(string[] throwsArray) // Helps a bit
        {
            var throwsValueList = new List<int>();
            foreach (var throwValue in throwsArray)
            {
                throwsValueList.Add(Int32.Parse(throwValue)); // add item to int array
                if (throwValue == "10")
                {
                    throwsValueList.Add(0);
                }
            }

            return throwsValueList.ToArray();

        }

        private static int GetFrameScore(int throw1, int throw2)
        {
            return throw1 + throw2;
        }
        private static int GetFrameScore(int[] throws)
        {
            return throws[0] + throws[1];
        }

        private int GetNextThrow(int currentIndex, int[] GroupedThrowsArrayay)
        {
            // + 2 because we are at pair [i, i+1] and we want to get next throw, which is i+2
            return GroupedThrowsArrayay[currentIndex + 2];
        }

        private int[] GetNextTwoThrows(int currentIndex, int[] GroupedThrowsArrayay)
        {
            return new int[2] { GroupedThrowsArrayay[currentIndex + 2], GroupedThrowsArrayay[currentIndex + 3]}; // same as above
        }

        private void HandleStrike(int[] throwsArray, int currentIndex)
        {
            var next2Throws = GetNextTwoThrows(currentIndex, throwsArray);
            var nextThrowOne = next2Throws[0];
            var nextThrowTwo = next2Throws[1];
            if (nextThrowOne == 10)
            {
                nextThrowTwo = GetNextThrow(currentIndex + 2, GroupedThrowsArray);
                Console.WriteLine(nextThrowTwo);
                var currScore = MaxScore + nextThrowOne + nextThrowTwo;
                Console.WriteLine($"Current SCORE: {currScore}");
                TotalScore += currScore;
                Console.WriteLine($"Total SCORE: {TotalScore}");
            }
            else
            {
                Console.WriteLine($"EXTRA 2 THROWS: [{next2Throws[0]}][{next2Throws[1]}]");
                var currentScore = MaxScore + next2Throws[0] + next2Throws[1];
                Console.WriteLine($"Current score: {currentScore}");
                TotalScore += currentScore;
                Console.WriteLine($"Total score: {TotalScore}");
            }
        }
    }
}
