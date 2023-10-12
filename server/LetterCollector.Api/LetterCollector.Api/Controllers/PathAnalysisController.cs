using Microsoft.AspNetCore.Mvc;
using LetterCollector.Api.Services;

namespace LetterCollector.Api.Controllers
{
    [ApiController]
    [Route("/map")]
    public class PathAnalysisController : Controller
    {
        [HttpPost]
        [Route("/analyze")]
        public IActionResult AnalyzePath()
        {
            var analyzer = new PathAnalyzerService();

            return Ok(analyzer.AnalyzePath());
        }
    }
}
