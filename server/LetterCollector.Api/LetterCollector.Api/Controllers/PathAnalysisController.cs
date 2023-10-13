using Microsoft.AspNetCore.Mvc;
using LetterCollector.Api.Services;
using LetterCollector.Api.Services.Interfaces;

namespace LetterCollector.Api.Controllers
{
    [ApiController]
    [Route("/map")]
    public class PathAnalysisController : Controller
    {
        private readonly IPathAnalyzerService _pathAnalyzerService;

        public PathAnalysisController(IPathAnalyzerService pathAnalyzerService)
        {
            _pathAnalyzerService = pathAnalyzerService;
        }

        [HttpPost]
        [Route("/analyze")]
        public IActionResult AnalyzePath([FromBody] string[] map)
        {
            try
            {
                var analyzer = _pathAnalyzerService;

                return Ok(analyzer.AnalyzePath(map));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
