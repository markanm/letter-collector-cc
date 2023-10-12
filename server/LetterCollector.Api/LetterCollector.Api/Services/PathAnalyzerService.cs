using LetterCollector.Api.Models;
using System.Text.RegularExpressions;

namespace LetterCollector.Api.Services
{
    public class PathAnalyzerService
    {
        private const char StartingChar = '@';
        private const string EndingCharRegex = "[xX]";
        private const char EmptySpaceChar = ' ';

        private static readonly Dictionary<char, (int x, int y)> Directions = new Dictionary<char, (int x, int y)>
        {
            { 'R', (1, 0) }, { 'L', (-1, 0) }, { 'U', (0, -1) }, { 'D', (0, 1) }
        };

        public PathAnalysisResultDto AnalyzePath(string[] map)
        {
            var currentPosition = FindStartingPosition(map);
            char currentChar = map[currentPosition.x][currentPosition.y];

            char currentDirection = FindNextDirection(map, currentPosition);

            //while (!Regex.IsMatch(map[currentPosition.x][currentPosition.y].ToString(), EndingCharRegex))
            //{
            //    //CurrentPosition = FindNextCharacter(map, CurrentPosition);

            //    //if(CurrentPosition ==)
            //}

            return new PathAnalysisResultDto() { Letters = "ABC", PathAsCharacters = "@---A---+|C|+---+|+-B-x" };
        }

        private (int x, int y) FindStartingPosition(string[] map)
        {
            (int x, int y)? result = null;

            for (int j = 0; j < map.Length; j++)
            {
                for (var i = 0; i < map[j].Length; i++)
                {
                    if (map[j][i] == StartingChar)
                    {
                        if (result != null)
                        {
                            throw new ArgumentException("Invalid map: Can't have more than one starting position!");
                        }

                        result = (x: i, y: j);
                    }
                }
            }

            if (!result.HasValue)
            {
                throw new ArgumentException("Invalid map: No starting character found!");
            }

            return result.Value;
        }

        private char FindNextDirection(string[] map, (int x, int y) position)
        {
            char? result = null;
            foreach (var direction in Directions)
            {
                int X = position.x + direction.Value.x;
                int Y = position.y + direction.Value.y;

                if ((Y < 0 || Y > map.Length) || (X < 0 || X > map[X].Length))
                {
                    continue;
                }

                if (map[position.y + direction.Value.y][position.x + direction.Value.x] != EmptySpaceChar)
                {
                    if (result != null)
                    {
                        throw new ArgumentException("Invalid map: Cannot have 2 opposed directions!");
                    }

                    result = direction.Key;
                }
            }

            if (!result.HasValue)
            {
                throw new ArgumentException("Invalid map: Cannot find next direction!");
            }

            return result.Value;
        }
    }
}
