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
        public IActionResult AnalyzePath([FromBody] string[] map)
        {
            Console.WriteLine(map);

            var analyzer = new PathAnalyzerService();

            return Ok(analyzer.AnalyzePath(map));
        }
    }
}
