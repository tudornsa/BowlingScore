using BowlingScore.Interfaces;

namespace BowlingScore.Services
{
    public class ScorePrinter
    {
        private readonly IScoreCalculator _calculator;
        private readonly IFramesBuilder _framesBuilder;
        public ScorePrinter(IScoreCalculator calculator, IFramesBuilder framesBuilder)
        {
            _calculator = calculator;
            _framesBuilder = framesBuilder;
        }
        public string Print()
        {
            var totalScore = _calculator.CalculateScore();
            var scoreBoard = _framesBuilder.CreateScoreboard(totalScore);
            return scoreBoard;
        }
    }
}
