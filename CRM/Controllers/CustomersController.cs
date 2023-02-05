using CRM.Models;

using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    public class CustomersController : Controller
    {
        public IConfiguration Configuration { get; }

        public CustomersController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            using (var repo = new Repository(Configuration))
            {
                //var startDate = new DateTime(DateTime.Now.Year, 1, 1);
                //var endDate = DateTime.Now;
                var customers = repo.GetCustomers();
                //ViewBag.StartDate = startDate;
                //ViewBag.EndDate = endDate;
                ViewBag.Customers = customers;
                return View();
            }
        }
    }
}