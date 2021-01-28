using System;

namespace BowlingScore
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFilePath = @"./input.txt";
            InputReader reader = new InputReader();
            //var scoreArray = reader.GetScoreArray(inputFilePath);
            ScoreCalculator calculator = new ScoreCalculator(reader, inputFilePath);
            calculator.CalculateScore();
            //var nextThrows = calculator.GetNextTwoThrows(2);
            //calculator.test2Throws(0);
        }
    }
}
