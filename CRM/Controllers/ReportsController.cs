using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    public class ReportsController : Controller
    {
        public IConfiguration Configuration { get; }

        public ReportsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
            //using (var repo = new Repository(Configuration))
            //{
            //    return View();
            //}
        }
    }
}