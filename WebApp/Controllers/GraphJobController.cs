using Microsoft.AspNetCore.Mvc;
using NetGraphSolver;

namespace WebApp.Controllers
{
    public class GraphJobController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Table")]
        [HttpGet]
        public IActionResult Table([FromQuery] string jobData)
        {
            List<GraphJob> jobs = new List<GraphJob>();
            try
            {
                jobs = GraphJobParser.ParseGraphJobs(jobData);
            }
            catch
            {
                return BadRequest("Ошибка в формате данных.");
            }

            var solver = new GraphSolver(jobs);
            try
            {
                solver.CalculateAll();
            }
            catch
            {
                return BadRequest("Неизвестная ошибка при вычислениях.");
            }

            return View(solver);
        }
    }
}
