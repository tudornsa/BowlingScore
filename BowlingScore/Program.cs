using System;
using BowlingScore.Services;

namespace BowlingScore
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFilePath = args[0];
            var reader = new InputReader();
            var rolls = reader.ParseInput(inputFilePath);

            ScoreCalculator calculator = new ScoreCalculator(rolls);
            FramesBuilder framesBuilder = new FramesBuilder(rolls);

            ScorePrinter printer = new ScorePrinter(calculator, framesBuilder);
            var scoreBoard = printer.Print();

            Console.WriteLine(scoreBoard);
        }
    }
}
