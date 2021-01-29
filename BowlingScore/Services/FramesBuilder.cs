using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScore.Services
{
    public class FramesBuilder : IFramesBuilder
    {
        private int[] _rolls;
        private FramesBuilder(int[] rolls)
        {
            _rolls = rolls;
        }
        public string CreateScoreboard()
        {
            return "| f1 | f2 | f3 | f4 | f5 | f6 | f7 | f8 | f9 | f10   |"; // TODO
        }
    }
}
