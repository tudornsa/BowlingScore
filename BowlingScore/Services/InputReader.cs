using System;
using System.Collections.Generic;
using System.IO;

namespace BowlingScore.Services
{
    public class InputReader : IInputReader
    {
        /*
         Using GetInput + ParseScore helps make the code extensible and reusable.
         Example of scenarios where it's helpful: Maybe i get a new input format. Then I can add ParseScoresNewFormat to handle that situation,
        rather than re-write the code to handle that as well. 
         Also easier to unit test this functionality
        */
        //public string RawInput { get; set; } // do I need to use this?
        //public string[] ThrowsArray { get; set; } // do I need to use this?

        private const int MaxPinNumber = 10;
        private static string ReadRawInput(string filePath) // Get raw input as string. Should this be static?
        {
            return File.ReadAllText(filePath);
        }

        private static string[] ExtractArray(string rawInput)
        {
            return rawInput.Split(", ");  // IS THIS OK NOW????
        }

        //private static string RemoveWhiteSpace(string input) 
        //{
        //    return Regex.Replace(input, @"\s+", "");
        //}

        private string[] GetInput(string filePath) // Do stuff with input
        {
            var rawInput = ReadRawInput(filePath);
            //var rawInputNoSpaces = RemoveWhiteSpace(rawInput); // no need
            //Console.WriteLine(rawInputNoSpaces);
            //RawInput = rawInputNoSpaces;
            //ThrowsArray = rawInputNoSpaces.Split(','); // Just return it?
            var rolls = ExtractArray(rawInput);


            Console.Write("---->");
            foreach (var theThrow in rolls)
            {
                Console.Write($"{theThrow}, ");
            }
            Console.WriteLine();
            return rolls;
        }

        private static int ConvertToInt(string number)
        {
            return Int32.Parse(number);
        }

        private static int[] AddZeroIfStrike(string[] originalRolls)
        {
            var modifiedRolls = new List<int>();
            foreach (var rollValue in originalRolls)
            {
                modifiedRolls.Add(ConvertToInt(rollValue));
                if (ConvertToInt(rollValue) == MaxPinNumber)
                {
                    modifiedRolls.Add(0);
                }
            }
            return modifiedRolls.ToArray();
        }

        public int[] ParseInput(string filePath)
        {
            var originalRolls = GetInput(filePath);
            return AddZeroIfStrike(originalRolls);
        }
    }
}
