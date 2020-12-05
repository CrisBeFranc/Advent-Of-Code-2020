using AdventOfCode2020.Challenges;
using AdventOfCode2020.Models;
using AdventOfCode2020.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Pages
{
    public partial class ChallengeDayThree
    {

        public string _loadedFile;
        private List<string> _forestMap;
        private List<Slope> _slopeConfigurations = new List<Slope>();
        int _right = 1;
        int _down = 1;
        long _productResult = 0;

        private bool invalid = false;
        private bool changedBox = false;

        ResultsDayThree _child;  

        protected override async Task OnInitializedAsync()
        {
            var byteOfTheFile = await _client.GetStreamAsync("challenges-data/day_three.txt");
            _forestMap = new List<string>();

            using (StreamReader reader = new StreamReader(byteOfTheFile, Encoding.Unicode))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    _forestMap.Add(line);
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
                return Regex.IsMatch(fix.Trim(), @"^[\.]+[\#]+$");
            }
            return true;
        }

        protected async Task RefreshSlopesMap()
        {
            if (_loadedFile != null)
            {
                Stream newReport = new MemoryStream(Encoding.UTF8.GetBytes(_loadedFile));
                _forestMap = new List<string>();

                using (StreamReader reader = new StreamReader(newReport))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                            _forestMap.Add(line);
                    }
                }
            }
        }

        protected void AddSlope()
        {
            if (_right < 1 || _down < 1)
            {
                invalid = true;
            }
            else
            {
                invalid = false;
                _slopeConfigurations.Add(new Slope
                {
                    StartPosition = _right + 1,
                    Move = _right,
                    Jumps = _down
                });
            }
        }

        protected void RemoveSlope(Slope toRemove)
        {
            _slopeConfigurations.Remove(toRemove);
        }


        protected async Task ShowResult()
        {

            if (ValidateTextbox())
            {
                invalid = false;
                DayThree _challengeSolver = new DayThree();

                await RefreshSlopesMap();

                _slopeConfigurations.ForEach(sc =>
                {
                    sc.EncounteredTrees = _challengeSolver.CalculateSlopes(sc.StartPosition, sc.Move, sc.Jumps, _forestMap);
                });

                if(_slopeConfigurations.Select(s => s.EncounteredTrees).Any(r => r > 1))
                {
                    double result = _slopeConfigurations
                        .Select(s => s.EncounteredTrees).Aggregate(1.0, (acc, c) => (acc * c));

                    _productResult = Convert.ToInt64(result);
                    _child.Show();
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
