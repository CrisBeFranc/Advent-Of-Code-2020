using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges
{
    public class DayTwo
    {


        public int PartOne(List<Password> passwords)
        {
            int totalRepetition = 0;
            passwords.ForEach(p =>
            {
                int totalCount = p.Pass.Count(l => l.Equals(p.FindingLetter[0]));
                if (totalCount >= p.MinValue && totalCount <= p.MaxValue)
                {
                    totalRepetition++;
                }
            });

            return totalRepetition;
        }


        public int PartTwo(List<Password> passwords)
        {
            int validPasswordsCount = 0;
            passwords.ForEach(p =>
            {
                if (p.Pass.Contains(p.FindingLetter[0]))
                {
                    if (p.Pass[p.MinValue - 1] == p.FindingLetter[0])
                    {
                        if (p.Pass[p.MaxValue - 1] != p.FindingLetter[0])
                        {
                            validPasswordsCount++;
                        }
                    }
                    else
                    {
                        if (p.Pass[p.MaxValue - 1] == p.FindingLetter[0])
                        {
                            validPasswordsCount++;
                        }
                    }
                }
            });

            return validPasswordsCount;
        }

    }
}
