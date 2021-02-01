using BowlingScore.Interfaces;
using BowlingScore.Services;
using Moq;
using Xunit;

namespace BowlingScore.Tests
{
    public class ScorePrinterTests
    {
        private readonly Mock<IFramesBuilder> _framesBuilderMock;
        private readonly Mock<IScoreCalculator> _scoreCalculatorMock;
        private readonly ScorePrinter _scorePrinter;

        public ScorePrinterTests()
        {
            _framesBuilderMock = new Mock<IFramesBuilder>();
            _scoreCalculatorMock = new Mock<IScoreCalculator>();
            _scorePrinter = new ScorePrinter(_scoreCalculatorMock.Object, _framesBuilderMock.Object);
        }

        [Fact]
        public void Print_CallsCalculateScoreAndCreateScoreboardOnce()
        {
            const int totalScoreMock = 0;
            const string expectedMock = "mock";
            _scoreCalculatorMock.Setup(calculator => calculator.CalculateScore()).Returns(totalScoreMock);
            _framesBuilderMock.Setup(builder => builder.CreateScoreboard(totalScoreMock)).Returns(expectedMock);

            _scorePrinter.Print();

            _scoreCalculatorMock.Verify(m => m.CalculateScore(), Times.Once);
            _framesBuilderMock.Verify(m => m.CreateScoreboard(totalScoreMock), Times.Once);
        }
    }
}