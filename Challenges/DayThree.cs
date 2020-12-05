using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Challenges
{
    public class DayThree
    {

        public long CalculateSlopes(int startingPosition, int rightMovement,
            int jumps, List<string> forestMap)
        {
            int rowlLength = forestMap[0].Length;
            int totalRows = forestMap.Count();
            long counter = 0;

            for (int i = jumps; i < totalRows; i += jumps)
            {
                startingPosition = startingPosition > rowlLength ? startingPosition - rowlLength : startingPosition;

                char[] row = forestMap[i].ToCharArray();

                if (row[startingPosition - 1] == '#')
                {
                    row[startingPosition - 1] = 'X';
                    counter++;
                }

                if (row[startingPosition - 1] == '.')
                {
                    row[startingPosition - 1] = 'O';
                }

                startingPosition = startingPosition + rightMovement;
            }

            return counter;
        }


    }
}
