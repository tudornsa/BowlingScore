using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BowlingScore.Tests
{

    public class ScoreCalculatorTests
    {
        [Fact]
        public void CalculateScore_TotalScore_ReturnSuccess()
        {
            var expectedTotal = 90;
            var inputFilePath = @"./TestInput.txt";
            InputReader reader = new InputReader();
            ScoreCalculator calculator = new ScoreCalculator(reader, inputFilePath);

            //act
            var total = calculator.CalculateScore();

            //assert
            Assert.Equal(expectedTotal, total);
        }

        [Fact]
        public void CalculateScore_EdgeCaseTotalScore_ReturnSuccess()
        {
            var expectedTotal = 199;
            var inputFilePath = @"./TestInputEdgeCase.txt";
            InputReader reader = new InputReader();
            ScoreCalculator calculator = new ScoreCalculator(reader, inputFilePath);

            //act
            var total = calculator.CalculateScore();

            //assert
            Assert.Equal(expectedTotal, total);
        }

        [Fact]
        public void CalculateScore_JustStrikesTotalScore_ReturnSuccess()
        {
            var expectedTotal = 300;
            var inputFilePath = @"./TestInputJustStrikes.txt";
            InputReader reader = new InputReader();
            ScoreCalculator calculator = new ScoreCalculator(reader, inputFilePath);

            //act
            var total = calculator.CalculateScore();

            //assert
            Assert.Equal(expectedTotal, total);
        }

        [Fact]
        public void CalculateScore_SpareExtraThrowLastFrame_ReturnSuccess()
        {
            var expectedTotal = 270;
            var inputFilePath = @"./TestInputSpareLastFrame.txt";
            InputReader reader = new InputReader();
            ScoreCalculator calculator = new ScoreCalculator(reader, inputFilePath);

            //act
            var total = calculator.CalculateScore();

            //assert
            Assert.Equal(expectedTotal, total);
        }
    }
}
