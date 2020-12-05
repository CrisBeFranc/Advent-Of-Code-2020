using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges
{
    public class DayFive
    {

        public int PartOne(List<string> boardingPassess)
        {
            var nums = boardingPassess.Select(boardingPass => boardingPass.Aggregate(0, (acc, c) =>
                       (acc << 1) + (c == 'B' || c == 'R' ? 1 : 0)))
                       .OrderBy(x => x)
                       .ToList();

            return nums.Max();
        }

        public int PartTwo(List<string> boardingPassess)
        {
            var nums = boardingPassess.Select(boardingPass => boardingPass.Aggregate(0, (acc, c) =>
                           (acc << 1) + (c == 'B' || c == 'R' ? 1 : 0)))
                           .OrderBy(x => x)
                           .ToList();

            return nums.Zip(nums.Skip(1), (a, b) => (a, b))
                .First(p => p.Item1 + 2 == p.Item2)
                .Item1 + 1;
        }

    }
}
