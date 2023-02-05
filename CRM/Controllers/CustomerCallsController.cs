using CRM.Models;

using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    public class CustomerCallsController : Controller
    {
        public IConfiguration Configuration { get; }

        public CustomerCallsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            using (var repo = new Repository(Configuration))
            {
                var customers = repo.GetCustomerCalls();
                ViewBag.Customers = customers;
                return View();
            }
        }
    }
}