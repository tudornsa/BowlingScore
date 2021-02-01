using System.Collections.Generic;
using BowlingScore.Services;
using Xunit;

namespace BowlingScore.Tests
{
    public class ScoreCalculatorTests
    {
        [Theory]
        [MemberData(nameof(ExampleInput))]
        [MemberData(nameof(OnlySpares))]
        [MemberData(nameof(OnlyStrikes))]
        [MemberData(nameof(SpareLastFrame))]
        [MemberData(nameof(StrikeLastFrame))]
        [MemberData(nameof(OnlyMisses))]
        [MemberData(nameof(NormalRolls))]
        public void CalculateScore_WithCorrectInput_ReturnsExpectedResult(int expectedResult, int[] input)
        {
            ScoreCalculator calculator = new ScoreCalculator(input);

            var total = calculator.CalculateScore();

            Assert.Equal(expectedResult, total);
        }

        public static IEnumerable<object[]> ExampleInput =>
            new List<object[]>
            {
                new object[]
                {
                    90,
                    new[] { 2, 3, 5, 4, 9, 1, 2, 5, 3, 2, 4, 2, 3, 3, 4, 6, 10, 3, 2 }
                }
            };
        public static IEnumerable<object[]> OnlySpares =>
            new List<object[]>
            {
                new object[]
                {
                    150,
                    new[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
                }
            };
        public static IEnumerable<object[]> OnlyStrikes =>
            new List<object[]>
            {
                new object[]
                {
                    300,
                    new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }
                }
            };
        public static IEnumerable<object[]> SpareLastFrame =>
            new List<object[]>
            {
                new object[]
                {
                    270,
                    new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 5, 5, 5 }
                }
            };
        public static IEnumerable<object[]> StrikeLastFrame =>
            new List<object[]>
            {
                new object[]
                {
                    60,
                    new[] { 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 10, 2, 3 },
                }
            };
        public static IEnumerable<object[]> OnlyMisses =>
            new List<object[]>
            {
                new object[]
                {
                    0,
                    new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                }
            };
        public static IEnumerable<object[]> NormalRolls =>
            new List<object[]>
            {
                new object[]
                {
                    80,
                    new[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }
                }
            };
    }
}
