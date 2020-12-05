using AdventOfCode2020.Challenges;
using AdventOfCode2020.Models;
using AdventOfCode2020.Results;
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
    public partial class ChallengeDayTwo
    {

        public string _loadedFile;
        private List<Password> _passwords;
        private Regex regexLetter = new Regex(@"([\w]{1}[\:])");
        private int _partOne;
        private int _partTwo;
        private bool invalid = false;
        private bool changedBox = false;

        ResultsDayTwo childOne;
        ResultsDayTwo childTwo;

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
            var byteOfTheFile = await _client.GetStreamAsync("challenges-data/day_two.txt");
            _passwords = new List<Password>();            

            using (StreamReader reader = new StreamReader(byteOfTheFile, Encoding.Unicode))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] digits = regexLetter.Split(line);
                    _passwords.Add(new Password
                    {
                        FindingLetter = digits[1].Split(":")[0],
                        MinValue = int.Parse(digits[0].Split("-")[0]),
                        MaxValue = int.Parse(digits[0].Split("-")[1]),
                        Pass = digits[2].Trim()
                    });
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
                _passwords = new List<Password>();

                using (StreamReader reader = new StreamReader(newReport))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            string[] digits = regexLetter.Split(line);
                            _passwords.Add(new Password
                            {
                                FindingLetter = digits[1].Split(":")[0],
                                MinValue = int.Parse(digits[0].Split("-")[0]),
                                MaxValue = int.Parse(digits[0].Split("-")[1]),
                                Pass = digits[2].Trim()
                            });
                        }   
                    }
                }
            }
        }

        protected async Task ShowResults()
        {

            if (ValidateTextbox())
            {
                invalid = false;
                DayTwo _challengeSolver = new DayTwo();

                _partOne = 0;
                _partTwo = 0;

                await RefreshReport();

                if (_passwords.Count > 0)
                {
                    _partOne = _challengeSolver.PartOne(_passwords);
                    _partTwo = _challengeSolver.PartTwo(_passwords);
                }

                if (_partOne != 0 && _partTwo != 0)
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
