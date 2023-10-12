using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
    }
}
