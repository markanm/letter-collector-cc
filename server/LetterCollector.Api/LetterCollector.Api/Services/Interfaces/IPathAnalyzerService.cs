using LetterCollector.Api.Models;

namespace LetterCollector.Api.Services.Interfaces
{
    public interface IPathAnalyzerService
    {
        PathAnalysisResultDto AnalyzePath(string[] map);
    }
}
