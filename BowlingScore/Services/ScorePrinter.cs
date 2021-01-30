using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScore.Services
{
    public class ScorePrinter : IScorePrinter
    {
        private IScoreCalculator _calculator;
        private IFramesBuilder _framesBuilder;
        public ScorePrinter(IScoreCalculator calculator, IFramesBuilder framesBuilder)
        {
            _calculator = calculator;
            _framesBuilder = framesBuilder;
        }
        public void Print()
        {
            var totalScore = _calculator.CalculateScore();
            var rollsBoard = _framesBuilder.CreateScoreboard(totalScore);
            Console.WriteLine($"FRAME ROLLS:");
            Console.WriteLine(rollsBoard);
        }

    }
}
