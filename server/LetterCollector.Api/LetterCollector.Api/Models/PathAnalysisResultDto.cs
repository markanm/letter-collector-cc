namespace LetterCollector.Api.Models
{
    public class PathAnalysisResultDto : IPathAnalysisResultDto
    {
        /// <summary>
        /// Gets the letters found in path.
        /// </summary>
        public string? Letters { get; set; }

        /// <summary>
        /// Gets the final path as a string.
        /// </summary>
        public string? PathAsCharacters { get; set; }
    }
}
