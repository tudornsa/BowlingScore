using System;
using Xunit;

namespace BowlingScore.Tests
{
    public class InputReaderTests
    {
        [Fact]
        public void GetInput_ReturnsInput_Success()
        {
            var reader = new InputReader();
            var expected = "2, 3, 5, 4, 9, 1, 2, 5, 3, 2, 4, 2, 3, 3, 4, 6, 10, 3, 2";

            //act
            var input = reader.GetThrowArray(@".\TestInput.txt");

            //assert
            //Assert.Equal(expected, input);
        }
    }
}
