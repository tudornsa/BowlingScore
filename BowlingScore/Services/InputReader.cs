using System;
using System.IO;
using Array = System.Array;

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

        private static string ReadRawInput(string filePath) // Should this be static? Should any method that does not need a prop be static?
        {
            return File.ReadAllText(filePath);
        }

        private static string[] ExtractArray(string rawInput)
        {
            return rawInput.Split(", ");
        }

        private static int[] GetInput(string filePath)
        {
            var rawInput = ReadRawInput(filePath);
            var rolls = ExtractArray(rawInput);
            return ConvertToInt(rolls);
        }

        private static int[] ConvertToInt(string[] rolls)
        {
            return Array.ConvertAll(rolls, roll => Int32.Parse(roll));
        }

        //private static int[] GetValues(string[] originalRolls)
        //{
        //    //var modifiedRolls = new List<int>();
        //    //foreach (var rollValue in originalRolls)
        //    //{
        //    //    modifiedRolls.Add(ConvertToInt(rollValue));
        //    //}
        //    //return modifiedRolls.ToArray();
        //    return Array.ConvertAll(originalRolls, input => Int32.Parse(input));
        //}

        public int[] ParseInput(string filePath)
        {
            var rolls = GetInput(filePath);
            return rolls;
        }
    }
}
