using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingScore
{
    public class ScoreCalculator
    {
        private InputReader _inputReader;
        private string _filePath;
        public int TotalScore { get; set; }
        public List<int> FrameScoreList { get; set; }
        public string[] ThrowArray { get; set; }

        const int MaxScore = 10;
        public ScoreCalculator(InputReader inputReader, string filePath)
        {
            _filePath = filePath;
            _inputReader = inputReader;
            ThrowArray = _inputReader.GetThrowArray(_filePath);
        }

        public int CalculateScore()
        {
            var newArr = CreateNewArray(ThrowArray);
            Console.Write("New array: [ ");
            foreach (var throwValue in newArr)
            {
                Console.Write($"{throwValue}, ");
            }

            Console.WriteLine(" ]");
            //foreach (var item in newArr)
            //{
            //    Console.WriteLine($"Item: {item}");
            //}
            //var frameNr = 1;
            for (int i = 0, frameNumber = 1; i < newArr.Length && i + 1 < newArr.Length && frameNumber <= 10; i += 2, frameNumber++)
            {
                Console.WriteLine($"Frame {frameNumber}");
                Console.WriteLine($"Current item: [{i}]: {newArr[i]}");
                Console.WriteLine($"Next item:[{i+1}]: {newArr[i+1]}");
                if (i + 1 == newArr.Length) // extra throw situation >>> does it fall under frame = 11? and if so is this check even needed?
                {
                    break;
                }
                else
                {
                    var firstThrow = Int32.Parse(newArr[i]); // do smth like static GetIntFromString(str)
                    var secondThrow = Int32.Parse(newArr[i + 1]);
                    Console.WriteLine($"[{firstThrow}][{secondThrow}]");

                    if (firstThrow == 10) // TODO: case when next 2 vals are 10
                    {
                        Console.WriteLine("STRIKE");
                        var next2Throws = GetNextTwoThrows(i, newArr);
                        var nextThrowOne = Int32.Parse(next2Throws[0]);
                        var nextThrowTwo = Int32.Parse(next2Throws[1]);
                        if (nextThrowOne == 10)
                        {
                            nextThrowTwo = Int32.Parse(GetNextThrow(i + 2, newArr));
                            Console.WriteLine(nextThrowTwo);
                            var currScore = MaxScore + nextThrowOne + nextThrowTwo;
                            Console.WriteLine($"Current SCORE: {currScore}");
                            TotalScore += currScore;
                            Console.WriteLine($"Total SCORE: {TotalScore}");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine($"EXTRA 2 THROWS: [{next2Throws[0]}][{next2Throws[1]}]");
                            var currentScore = MaxScore + Int32.Parse(next2Throws[0]) + Int32.Parse(next2Throws[1]);
                            Console.WriteLine($"Current score: {currentScore}");
                            TotalScore += currentScore;
                            Console.WriteLine($"Total score: {TotalScore}");
                        }
                    }
                    else if ((firstThrow + secondThrow) == 10)
                    {
                        Console.WriteLine("SPARE");
                        var nextThrow = Int32.Parse(GetNextThrow(i, newArr));
                        var currentScore = MaxScore + nextThrow;
                        Console.WriteLine($"Current score: {currentScore}");
                        TotalScore += currentScore;
                        Console.WriteLine($"Total score: {TotalScore}");
                    }
                    else
                    {
                        var currentScore = firstThrow + secondThrow;
                        Console.WriteLine($"Current score: {currentScore}");
                        TotalScore += currentScore;
                        Console.WriteLine($"Total score: {TotalScore}");
                    }

                    //if ((firstThrow + secondThrow) == 10)
                    //{
                    //    Console.WriteLine("SPARE");
                    //    var nextThrow = Int32.Parse(GetNextThrow(i));
                    //    var currentScore = MaxScore + nextThrow;
                    //    Console.WriteLine($"Current score: {currentScore}");
                    //    Score += currentScore;
                    //    Console.WriteLine($"Total score: {Score}");
                    //}
                    //else
                    //{
                    //    var currentScore = firstThrow + secondThrow;
                    //    Console.WriteLine($"Current score: {currentScore}");
                    //    Score += currentScore;
                    //    Console.WriteLine($"Total score: {Score}");
                    //}

                }
            }

            return TotalScore;

            //for (var i = 0; i < 20; i += 2)
            //{

            //    if (i + 1 >= ThrowArray.Length)
            //    {
            //        if (ThrowArray[i] == "10")
            //        {
            //            Console.WriteLine(ThrowArray[i]);
            //            Console.WriteLine(0);
            //            Console.WriteLine("---");
            //            i--;
            //            continue;
            //        }
            //        Console.WriteLine(ThrowArray[i]);
            //        Console.WriteLine("---");
            //        continue;
            //    }
            //    else
            //    {
            //        if (ThrowArray[i] == "10")
            //        {
            //            Console.WriteLine(ThrowArray[i]);
            //            Console.WriteLine(0);
            //            Console.WriteLine("---");
            //            i--;
            //            continue;
            //        }
            //        Console.WriteLine(ThrowArray[i]);
            //        Console.WriteLine(ThrowArray[i + 1]);
            //        Console.WriteLine("---");
            //    }
            //}
            //var i = 0;
            //while(i < ThrowArray.Length && i + 1 < ThrowArray.Length)
            //var inputArray = _inputReader.GetThrowArray(_filePath);
            //var i = 1;
            //foreach (string score in inputArray)
            //{
            //    Console.WriteLine($"Lovitura {i++}: {score}");
            //}
        }

        private static string[] CreateNewArray(string[] oldArray) // Helps a bit
        {
            var newList = new List<string>();
            foreach (var throwValue in oldArray)
            {
                newList.Add(throwValue);
                if (throwValue == "10")
                {
                    newList.Add("0");
                }
            }

            var newArray = newList.ToArray();
            return newArray;
        }

        private static int GetFrameScore(int throw1, int throw2)
        {
            return throw1 + throw2;
        }
        private static int GetFrameScore(int[] throws)
        {
            return throws[0] + throws[1];
        }

        private string GetNextThrow(int currentIndex, string[] newArray)
        {
            // + 2 because we are at pair [i, i+1] and we want to get next throw, which is i+2
            return newArray[currentIndex + 2];
        }

        private string[] GetNextTwoThrows(int currentIndex, string[] newArray)
        {
            return new string[2] { newArray[currentIndex + 2], newArray[currentIndex + 3]}; // same as above
        }

        //public void test2Throws(int index)
        //{
        //    //ThrowArray = _inputReader.GetThrowArray(_filePath);
        //    var throwsArr = this.GetNextTwoThrows(index);
        //    Console.Write("[ ");
        //    foreach (var throwVal in throwsArr)
        //    {
        //        Console.Write(throwVal);
        //        Console.Write(", ");
        //    }

        //    Console.WriteLine(" ]");
        //}
    }
}
