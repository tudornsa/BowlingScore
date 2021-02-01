using System;
using BowlingScore.Services;

namespace BowlingScore
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine(
                    "[Error] Please specify an input file! Correct usage: BowlingScore.exe --input pathToFile");
                return;
            }

            if (args[0] != "--input")
            {
                Console.WriteLine(
                    "[Error] Please specify an input file! Correct usage: BowlingScore.exe --input pathToFile");
                return;
            }

            var inputFilePath = args[1];
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
