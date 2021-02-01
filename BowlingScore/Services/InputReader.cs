using System;
using System.IO;
using BowlingScore.Interfaces;
using Array = System.Array;

namespace BowlingScore.Services
{
    public class InputReader
    {
        public int[] ParseInput(string filePath)
        {
            var rolls = GetInput(filePath);
            return rolls;
        }

        private static string ReadRawInput(string filePath)
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
            return Array.ConvertAll(rolls, roll => int.Parse(roll));
        }

    }
}
