namespace LetterCollector.Api.Models
{
    public interface IPathAnalysisResult
    {
        /// <summary>
        /// Gets the letters found in path.
        /// </summary>
        public string? Letters { get; }

        /// <summary>
        /// Gets the final path as a string.
        /// </summary>
        public string? PathAsCharacters { get; }
    }
}
