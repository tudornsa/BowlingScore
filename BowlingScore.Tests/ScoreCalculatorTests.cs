using Xunit;

namespace BowlingScore.Tests
{
    //public class InputReaderMock : IInputReader
    //{
    //    public string[] GetRolls(string filePath)
    //    {
    //        return new string[19] { "2", "3", "5", "4", "9", "1", "2", "5", "3", "2", "4", "2", "3", "3", "4", "6", "10", "3", "2" };
    //    }
    //}

    public class ScoreCalculatorTests // TODO: USE THEORY! (https://stackoverflow.com/questions/36419534/pass-array-of-string-to-xunit-test-method/61497205#61497205)
    {
        //public class InputReaderMock : IInputReader
        //{
        //    private readonly int[] _returnArr;
        //    public InputReaderMock(int[] returnArr)
        //    {
        //        _returnArr = returnArr;
        //    }
            
        //    //public string[] GetRolls(string filePath)
        //    //{
        //    //    return _returnArr;
        //    //}

        //    public int[] ParseInput(string filePath)
        //    {
        //        return _returnArr;
        //    }
        //}
        
        //private const string InputFilePathMock = @"mockFilePath";

        //[Fact]
        //public void CalculateScore_InputExampleTotalScore_Equals90()
        //{
        //    var expected = 90;
        //    var rolls = new [] { 2, 3, 5, 4, 9, 1, 2, 5, 3, 2, 4, 2, 3, 3, 4, 6, 10, 0, 3, 2 };
        //    //IInputReader readerMock = new InputReaderMock(inputArray); // new InputReaderMock(new[] { "2", "3", "5", "4", "9", "1", "2", "5", "3", "2", "4", "2", "3", "3", "4", "6", "10", "3", "2"});
        //    ScoreCalculator calculator = new ScoreCalculator(rolls);

        //    var total = calculator.CalculateScore();

        //    Assert.Equal(expected, total);
        //}
        //[Fact]
        //public void CalculateScore_OnlySparesInput_TotalScoreEquals150() // change like this
        //{
        //    var expected = 150;
        //    var rollsMock = new [] {5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5};
        //    //IInputReader readerMock = new InputReaderMock(inputArray); // new InputReaderMock(new[] {"5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5"});
        //    ScoreCalculator calculator = new ScoreCalculator(rollsMock);

        //    var total = calculator.CalculateScore();

        //    Assert.Equal(expected, total);
        //}


        //[Fact]
        //public void CalculateScore_JustStrikesInputTotalScore_Equals300()
        //{
        //    var expected = 300;
        //    var inputArray = new string[12] { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
        //    IInputReader readerMock = new InputReaderMock(inputArray); // new InputReaderMock(new[] { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" });
        //    ScoreCalculator calculator = new ScoreCalculator(readerMock, InputFilePathMock);

        //    //act
        //    var total = calculator.CalculateScore();

        //    //assert
        //    Assert.Equal(expected, total);
        //}

        //[Fact]
        //public void CalculateScore_EdgeCaseTotalScore_ReturnSuccess() //TODO: RENAME THIS TEST CASE
        //{
        //    var expectedTotal = 199;
        //    var inputArray = new string[18] { "3", "4", "2", "5", "5", "5", "5", "5", "5", "5", "5", "5", "10", "10", "10", "10", "10", "10" };
        //    IInputReader readerMock = new InputReaderMock(inputArray); // new InputReaderMock(new[] { "3", "4", "2", "5", "5", "5", "5", "5", "5", "5", "5", "5", "10", "10", "10", "10", "10", "10" });
        //    ScoreCalculator calculator = new ScoreCalculator(readerMock, InputFilePathMock);

        //    //act
        //    var total = calculator.CalculateScore();

        //    //assert
        //    Assert.Equal(expectedTotal, total);
        //}

        //[Fact]
        //public void CalculateScore_SpareExtraThrowLastFrameInputTotalScore_Equals270()
        //{
        //    var expected = 270;
        //    var inputArray = new string[12] { "10", "10", "10", "10", "10", "10", "10", "10", "10", "5", "5", "5" };
        //    IInputReader readerMock = new InputReaderMock(inputArray); // new InputReaderMock(new[] { "10", "10", "10", "10", "10", "10", "10", "10", "10", "5", "5", "5" });
        //    ScoreCalculator calculator = new ScoreCalculator(readerMock, InputFilePathMock);

        //    //act
        //    var total = calculator.CalculateScore();

        //    //assert
        //    Assert.Equal(expected, total);
        //}

        //[Fact]
        //public void CalculateScore_MissEveryThrowInputTotalScore_Equals0()
        //{
        //    var expected = 0;
        //    var inputArray = new string[20] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        //    IInputReader readerMock = new InputReaderMock(inputArray);
        //    ScoreCalculator calculator = new ScoreCalculator(readerMock, InputFilePathMock);

        //    //act
        //    var total = calculator.CalculateScore();

        //    //assert
        //    Assert.Equal(expected, total);
        //}

        //[Fact]
        //public void CalculateScore_StrikeExtraThrowLastFrameInputTotalScore_Equals60()
        //{
        //    var expected = 60;
        //    var inputArray = new string[21] { "2", "3", "2", "3", "2", "3", "2", "3", "2", "3", "2", "3", "2", "3", "2", "3", "2", "3", "10", "2", "3" };
        //    IInputReader readerMock = new InputReaderMock(inputArray);
        //    ScoreCalculator calculator = new ScoreCalculator(readerMock, InputFilePathMock);

        //    //act
        //    var total = calculator.CalculateScore();

        //    //assert
        //    Assert.Equal(expected, total);
        //}
    }
}
