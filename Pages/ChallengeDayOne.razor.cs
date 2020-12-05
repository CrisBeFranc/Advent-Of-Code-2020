using AdventOfCode2020.Challenges;
using AdventOfCode2020.Models;
using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Pages
{
    public partial class ChallengeDayOne
    {

        public string _loadedFile;
        private List<int> _expenseReport;
        private ValidEntries _partOne;
        private ValidEntries _partTwo;
        private int sumNumber = 2020;
        private bool invalid = false;
        private bool changedBox = false;

        DayOneResult childOne;
        DayOneResult childTwo;

        public string ExpenseReport
        {
            get { return _loadedFile; }
            set
            {
                _loadedFile = value;
                changedBox = true;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            var byteOfTheFile = await _client.GetStreamAsync("challenges-data/day_one.txt");
            _expenseReport = new List<int>();

            using (StreamReader reader = new StreamReader(byteOfTheFile, Encoding.Unicode))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    _expenseReport.Add(Convert.ToInt32(line));
                }

                reader.DiscardBufferedData();
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                _loadedFile = await reader.ReadToEndAsync();
            }
        }

        protected bool ValidateTextbox()
        {
            if (changedBox)
            {
                string fix = new string(_loadedFile.Replace("\n", string.Empty));
                return Regex.IsMatch(fix.Trim(), @"^[0-9]+$");
            }
            return true;
        }

        protected async Task RefreshReport()
        {
            if (_loadedFile != null)
            {
                Stream newReport = new MemoryStream(Encoding.UTF8.GetBytes(_loadedFile));
                _expenseReport = new List<int>();

                using (StreamReader reader = new StreamReader(newReport))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                            _expenseReport.Add(Convert.ToInt32(line));

                    }
                }
            }
        }

        protected async Task ShowResults()
        {

            if (ValidateTextbox())
            {
                invalid = false;
                DayOne _challengeSolver = new DayOne(sumNumber);

                _partOne = new ValidEntries();
                _partTwo = new ValidEntries();

                await RefreshReport();

                if (_expenseReport.Count > 0)
                {
                    _partOne = _challengeSolver.PartOne(_expenseReport);
                    _partTwo = _challengeSolver.PartTwo(_expenseReport);
                }

                if (_partOne != null && _partTwo != null)
                {
                    childOne.Show();
                    childTwo.Show();
                }
                else
                {
                    invalid = true;
                }

            }
            else
            {
                invalid = true;
            }

        }
    }
}
