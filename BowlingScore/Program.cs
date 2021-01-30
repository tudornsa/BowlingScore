using System;
using BowlingScore.Services;
using System.Collections.Generic;
using System.IO;

namespace BowlingScore
{
    class Program
    {
        // TODO: Group in folders
        static void Main(string[] args)
        {
            var inputFilePath = @"./input.txt";
            //InputReader reader = new InputReader();
            var reader = new InputReader();
            var rolls = reader.ParseInput(inputFilePath);

            //var input = File.ReadAllText(inputFilePath);
            //var rollsStr = input.Split(", ");
            //List<int> rollsList = new List<int>();
            //foreach (var roll in rollsStr)
            //{
            //    rollsList.Add(Int32.Parse(roll));
            //}

            //var rolls = rollsList.ToArray();
            //var scoreArray = reader.GetScoreArray(inputFilePath);

            ScoreCalculator calculator = new ScoreCalculator(rolls);
            FramesBuilder framesBuilder = new FramesBuilder(rolls);
            //calculator.CalculateScore();

            ScorePrinter printer = new ScorePrinter(calculator, framesBuilder);
            printer.Print();
        }
    }
}
