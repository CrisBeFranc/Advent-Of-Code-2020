using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Models
{
    public class Password
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public string FindingLetter { get; set; }
        public string Pass { get; set; }
    }
}
