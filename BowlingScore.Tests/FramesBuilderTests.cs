using System.Collections.Generic;
using BowlingScore.Services;
using Xunit;

namespace BowlingScore.Tests
{
    public class FramesBuilderTests
    {
        [Theory]
        [MemberData(nameof(ExampleInput))]
        [MemberData(nameof(OnlySpares))]
        [MemberData(nameof(OnlyStrikes))]
        [MemberData(nameof(SpareLastFrame))]
        [MemberData(nameof(StrikeLastFrame))]
        [MemberData(nameof(OnlyMisses))]
        [MemberData(nameof(NormalRolls))]
        public void CreateScoreboard_WithCorrectInput_ReturnsExpectedScoreBoard(string expectedResult, int[] rolls, int totalScore)
        {
            FramesBuilder framesBuilder = new FramesBuilder(rolls);

            var scoreboard = framesBuilder.CreateScoreboard(totalScore);

            Assert.Equal(expectedResult, scoreboard);
        }

        public static IEnumerable<object[]> ExampleInput =>
            new List<object[]>
            {
                new object[]
                {
                    "| f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |\r\n|2, 3|5, 4|9, /|2, 5|3, 2|4, 2|3, 3|4, /|X   |3, 2   |\r\nscore: 90",
                    new[] { 2, 3, 5, 4, 9, 1, 2, 5, 3, 2, 4, 2, 3, 3, 4, 6, 10, 3, 2 } ,
                    90
                }
            };
        public static IEnumerable<object[]> OnlySpares =>
            new List<object[]>
            {
                new object[]
                {
                    "| f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |\r\n|5, /|5, /|5, /|5, /|5, /|5, /|5, /|5, /|5, /|5, /, 5|\r\nscore: 150",
                    new[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                    150
                }
            };

        public static IEnumerable<object[]> OnlyStrikes =>
            new List<object[]>
            {
                new object[]
                {
                    "| f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |\r\n|X   |X   |X   |X   |X   |X   |X   |X   |X   |X, X, X|\r\nscore: 300",
                    new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
                    300
                }
            };

        public static IEnumerable<object[]> SpareLastFrame =>
            new List<object[]>
            {
                new object[]
                {
                    "| f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |\r\n|X   |X   |X   |X   |X   |X   |X   |X   |X   |5, /, 5|\r\nscore: 270",
                    new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 5, 5, 5 },
                    270
                }
            };

        public static IEnumerable<object[]> StrikeLastFrame =>
            new List<object[]>
            {
                new object[]
                {
                    "| f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |\r\n|2, 3|2, 3|2, 3|2, 3|2, 3|2, 3|2, 3|2, 3|2, 3|X, 2, 3|\r\nscore: 60",
                    new[] { 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 10, 2, 3 },
                    60
                }
            };

        public static IEnumerable<object[]> OnlyMisses =>
            new List<object[]>
            {
                new object[]
                {
                    "| f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |\r\n|-, -|-, -|-, -|-, -|-, -|-, -|-, -|-, -|-, -|-, -   |\r\nscore: 0",
                    new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    0
                }
            };

        public static IEnumerable<object[]> NormalRolls =>
            new List<object[]>
            {
                new object[]
                {
                    "| f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |\r\n|4, 4|4, 4|4, 4|4, 4|4, 4|4, 4|4, 4|4, 4|4, 4|4, 4   |\r\nscore: 80",
                    new[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
                    80
                }
            };
    }
}
