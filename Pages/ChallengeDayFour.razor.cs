using AdventOfCode2020.Challenges;
using AdventOfCode2020.Results;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Pages
{
    public partial class ChallengeDayFour
    {

        public string _loadedFile;
        private List<string> _passports;
        private int partOne = 0;
        private int partTwo = 0;        

        private bool invalid = false;
        private bool changedBox = false;

        ResultsDayFour childOne;
        ResultsDayFour childTwo;


        protected override async Task OnInitializedAsync()
        {
            var byteOfTheFile = await _client.GetStreamAsync("challenges-data/day_four.txt");
            _passports = new List<string>();

            using (StreamReader reader = new StreamReader(byteOfTheFile, Encoding.Unicode))
            {
                string line;
                string userDetails = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line == string.Empty)
                    {
                        _passports.Add(userDetails);
                        userDetails = string.Empty;
                    }
                    else
                    {
                        userDetails = $"{line} {userDetails}";
                    }
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

        protected async Task RefreshPassports()
        {
            if (_loadedFile != null)
            {
                Stream newReport = new MemoryStream(Encoding.UTF8.GetBytes(_loadedFile));
                _passports = new List<string>();

                using (StreamReader reader = new StreamReader(newReport))
                {
                    string line;
                    string userDetails = string.Empty;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        line = line.Trim();
                        if (line == string.Empty)
                        {
                            _passports.Add(userDetails);
                            userDetails = string.Empty;
                        }
                        else
                        {
                            userDetails = $"{line} {userDetails}";
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
                DayFour _challengeSolver = new DayFour();                              

                await RefreshPassports();

                if (_passports.Count > 0)
                {
                    partOne = _challengeSolver.PartOne(_passports);
                    partTwo = _challengeSolver.PartTwo(_passports);
                }

                if (partOne != 0 && partTwo != 0)
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
