using System;

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
            var scoreBoard = _framesBuilder.CreateScoreboard(totalScore);
            Console.WriteLine($"--------");
            Console.WriteLine(scoreBoard);
            Console.WriteLine($"Total: {totalScore}");
        }

    }
}
