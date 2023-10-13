using LetterCollector.Api.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace LetterCollector.Api.Services
{
    public class PathAnalyzerService
    {
        private const char StartingChar = '@';
        private const char EndingChar = 'x';
        private const char EmptySpaceChar = ' ';
        private const string ValidLetterRegex = @"[A-Z]";
        private const string ValidTurnRegex = @"[A-Z+]";
        private const string ValidHorizontalCharsRegex = @"[A-Zx+-]";
        private const string ValidVerticalCharsRegex = @"[A-Zx+|]";

        private static readonly Dictionary<char, (int x, int y)> Directions = new Dictionary<char, (int x, int y)>
        {
            { 'R', (1, 0) }, { 'D', (0, 1) }, { 'L', (-1, 0) }, { 'U', (0, -1) }
        };

        public PathAnalysisResultDto AnalyzePath(string[] map)
        {
            var letters = new StringBuilder();
            var path = new StringBuilder();
            var currentPosition = FindStartingPosition(map);
            List<(int x, int y)> collectedLetterPositions = new List<(int x, int y)>();

            path.Append(map[currentPosition.y][currentPosition.x]);

            char currentDirection = FindNextDirection(map, currentPosition, null);

            while (map[currentPosition.y][currentPosition.x] != EndingChar)
            {
                currentPosition = Move(map, currentPosition, currentDirection);
                var currentChar = map[currentPosition.y][currentPosition.x];
                path.Append(currentChar);

                if (!collectedLetterPositions.Contains(currentPosition))
                {

                    if (Regex.IsMatch(currentChar.ToString(), ValidLetterRegex))
                    {
                        letters.Append(currentChar);
                        collectedLetterPositions.Add(currentPosition);
                    }
                }

                if (Regex.IsMatch(currentChar.ToString(), ValidTurnRegex))
                {
                    currentDirection = FindNextDirection(map, currentPosition, currentDirection);
                }
            }

            return new PathAnalysisResultDto() { Letters = letters.ToString(), PathAsCharacters = path.ToString() };
        }

        private static (int x, int y) FindStartingPosition(string[] map)
        {
            (int x, int y)? result = null;

            for (int j = 0; j < map.Length; j++)
            {
                for (var i = 0; i < map[j].Length; i++)
                {
                    if (map[j][i] == StartingChar)
                    {
                        // Already found starting char, throw error
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

        private static char FindNextDirection(string[] map, (int x, int y) position, char? currentDirection)
        {
            // Try to continue forward if it's a letter, throw error if fake turn is detected
            if (currentDirection != null)
            {
                var nextPosition = Move(map, position, currentDirection.Value);

                if (IsValidDirection(map, nextPosition.x, nextPosition.y, currentDirection.Value))
                {
                    if (Regex.IsMatch(map[position.y][position.x].ToString(), ValidLetterRegex))
                    {
                        return currentDirection.Value;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid map: Fake turn detected!");
                    }
                }
            }

            char? result = null;

            // Find next turn
            foreach (var direction in Directions)
            {
                int x = position.x + direction.Value.x;
                int y = position.y + direction.Value.y;

                if (!IsValidPosition(map, x, y))
                {
                    continue;
                }

                if (map[y][x] != EmptySpaceChar && direction.Key != currentDirection)
                {
                    if (currentDirection == null || (currentDirection != null && direction.Key != GetOppositeDirection(currentDirection.Value)))
                    {
                        result = direction.Key;
                        break;
                    }
                }
            }

            if (!result.HasValue)
            {
                throw new ArgumentException("Invalid map: Cannot find next direction!");
            }

            var oppositePosition = Move(map, position, GetOppositeDirection(result.Value));

            if (IsValidPosition(map, oppositePosition.x, oppositePosition.y) && map[oppositePosition.y][oppositePosition.x] != EmptySpaceChar)
            {
                throw new ArgumentException("Invalid map: Cannot have 2 opposed directions!");
            }

            return result.Value;
        }

        private static char GetOppositeDirection(char direction)
        {
            return direction switch
            {
                'R' => 'L',
                'L' => 'R',
                'U' => 'D',
                'D' => 'U',
                _ => throw new ArgumentException($"Invalid direction: {direction}"),
            };
        }

        private static (int x, int y) Move(string[] map, (int x, int y) position, char direction)
        {
            if (Directions.TryGetValue(direction, out var result))
            {
                return (x: position.x + result.x, y: position.y + result.y);
            }

            throw new ArgumentException("Invalid direction!");
        }

        private static bool IsValidPosition(string[] map, int x, int y)
        {
            return (y >= 0 && y < map.Length) && (x >= 0 && x < map[y].Length);
        }

        private static bool IsValidDirection(string[] map, int x, int y, char currentDirection)
        {
            return IsValidPosition(map, x, y) && CheckValidDirectionCharacter(map[y][x], currentDirection);
        }

        private static bool CheckValidDirectionCharacter(char currentCharacter, char currentDirection)
        {
            return currentDirection switch
            {
                'R' or 'L' => Regex.IsMatch(currentCharacter.ToString(), ValidHorizontalCharsRegex),
                'D' or 'U' => Regex.IsMatch(currentCharacter.ToString(), ValidVerticalCharsRegex),
                _ => throw new ArgumentException("Invalid direction!")
            };
        }
    }
}
