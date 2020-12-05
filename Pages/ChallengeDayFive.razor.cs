using AdventOfCode2020.Challenges;
using AdventOfCode2020.Results;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Pages
{
    public partial class ChallengeDayFive
    {

        public string _loadedFile;
        private List<string> _boardingPasses;
        private int _partOne = 0;
        private int _partTwo = 0;

        private bool invalid = false;
        private bool changedBox = false;

        ResultsDayFive childOne;
        ResultsDayFive childTwo;
        

        protected override async Task OnInitializedAsync()
        {
            var byteOfTheFile = await _client.GetStreamAsync("challenges-data/day_five.txt");
            _boardingPasses = new List<string>();

            using (StreamReader reader = new StreamReader(byteOfTheFile, Encoding.Unicode))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    _boardingPasses.Add(line);
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
                _boardingPasses = new List<string>();

                using (StreamReader reader = new StreamReader(newReport))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                            _boardingPasses.Add(line);

                    }
                }
            }
        }

        protected async Task ShowResults()
        {

            if (ValidateTextbox())
            {
                invalid = false;
                DayFive _challengeSolver = new DayFive();                

                await RefreshReport();

                if (_boardingPasses.Count > 0)
                {
                    _partOne = _challengeSolver.PartOne(_boardingPasses);
                    _partTwo = _challengeSolver.PartTwo(_boardingPasses);
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
