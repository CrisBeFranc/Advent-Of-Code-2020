using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace AdventOfCode2020.Challenges
{
    public class DayFour
    {

        public int PartOne(List<string> passports)
        {
            var values = new[] { "ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt" };
            int counter = 0;

            passports.ForEach(d =>
            {
                if (values.All(d.Contains))
                {
                    counter++;
                }

            });

            return counter;
        }

        public int PartTwo(List<string> passports)
        {
            List<string> data = new List<string>() { @"byr:(19[2-9][0-9]|200[0-2])",
                                             @"iyr:(201[0-9]|2020)",
                                             @"eyr:(202[0-9]|2030)",
                                             @"hgt:((1[5-8][0-9]|19[0-3])cm)|hgt:(59|6[0-9]|7[0-6])in",
                                             @"hcl:(#[0-9a-f]{6})",
                                             @"ecl:(amb|blu|brn|gry|grn|hzl|oth)",
                                             @"pid:(\d{9}\b)" };

            int counter = 0;
            bool valid = true;
            passports.ForEach(d =>
            {
                foreach (string regex in data)
                {
                    MatchCollection matches = Regex.Matches(d, regex);
                    if (matches.Count == 0)
                    {
                        valid = false;
                    }
                }

                if (valid)
                {
                    counter++;
                }

                valid = true;
            });

            return counter;
        }

    }
}
