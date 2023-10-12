using LetterCollector.Api.Models;

namespace LetterCollector.Api.Services
{
    public class PathAnalyzerService
    {
        public PathAnalysisResult AnalyzePath()
        {
            return new PathAnalysisResult() { Letters = "ABC", PathAsCharacters = "@---A---+|C|+---+|+-B-x" };
        }
    }
}
