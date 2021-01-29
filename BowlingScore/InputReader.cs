using System;
using System.IO;
using System.Text.RegularExpressions;

namespace BowlingScore
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
        private static string ReadRawInput(string filePath) // Get raw input as string. Should this be static?
        {
            return File.ReadAllText(filePath);
        }

        private static string RemoveWhiteSpace(string input) 
        {
            return Regex.Replace(input, @"\s+", "");
        }

        public string[] GetThrowsArray(string filePath) // Do stuff with input
        {
            var rawInput = ReadRawInput(filePath);
            var rawInputNoSpaces = RemoveWhiteSpace(rawInput); // no need
            //Console.WriteLine(rawInputNoSpaces);
            //RawInput = rawInputNoSpaces;
            //ThrowsArray = rawInputNoSpaces.Split(','); // Just return it?
            var throwsArray = rawInputNoSpaces.Split(", ");


            Console.Write("---->");
            foreach (var theThrow in throwsArray)
            {
                Console.Write($"{theThrow}, ");
            }
            Console.WriteLine();
            return throwsArray;
        }
    }
}
