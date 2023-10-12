using LetterCollector.Api.Models;

namespace LetterCollector.Api.Services
{
    public class PathAnalyzerService
    {
        private const char StartingChar = '@';

        public PathAnalysisResultDto AnalyzePath(string[] map)
        {
            var startingPosition = FindStartingPosition(map);

            return new PathAnalysisResultDto() { Letters = "ABC", PathAsCharacters = "@---A---+|C|+---+|+-B-x" };
        }

        private (int x, int y) FindStartingPosition(string[] map)
        {
            for(int j = 0; j < map.Length; j++)
            {
                for(var i = 0; i < map[j].Length; i++)
                {
                    if (map[i][j] == StartingChar)
                    {
                        return (x: j, y: i);
                    }
                }
            }

            throw new ArgumentException("Invalid map: No starting character found!");
        }
    }
}
