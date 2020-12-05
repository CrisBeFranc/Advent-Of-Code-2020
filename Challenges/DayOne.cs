using AdventOfCode2020.Models;
using System.Collections.Generic;

namespace AdventOfCode2020.Challenges
{
    public class DayOne
    {
        private readonly int _year;
        private ValidEntries _toReturn;

        public DayOne (int year)
        {
            _year = year;
        }

        public ValidEntries PartOne(List<int> expenseReport)
        {
            var validExpenseEntries = new HashSet<int>();

            // O(n) Duplet in List with Target Sum
            expenseReport.ForEach(n =>
            {
                if (validExpenseEntries.Contains(_year - n))
                {
                    _toReturn = new ValidEntries
                    {
                        ValidEntryOne = n,
                        ValidEntryTwo = _year - n,
                        ValidEntryThree = -99,
                        ValidEntryProduct = n * (_year - n)
                    };                   
                }
                else
                {
                    validExpenseEntries.Add(n);
                }
            });

            return _toReturn;
        }

        public ValidEntries PartTwo(List<int> expenseReport)
        {
            var validExpenseEntries = new HashSet<int>();
            bool foundTriplet = false;

            // O(n) Triplet in List With Target Sum
            foreach (var n in expenseReport)
            {
                var currentSum = _year - n;
                foreach (var j in expenseReport)
                {
                    if (!foundTriplet && validExpenseEntries.Contains(currentSum - j))
                    {
                        var sum = n + j + (currentSum - j);
                        var product = n * j * (currentSum - j);

                        _toReturn = new ValidEntries
                        {
                            ValidEntryOne = n,
                            ValidEntryTwo = j,
                            ValidEntryThree = currentSum - j,
                            ValidEntryProduct = n * j * (currentSum - j)
                        };                       
                        foundTriplet = true;
                        break;
                    }
                    else
                    {
                        validExpenseEntries.Add(j);
                    }
                }
            }

            return _toReturn;
        }

    }
}
