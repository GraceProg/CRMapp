using CRM.Models.DBClasses;
using CRM.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class CustomersController : Controller
    {
        public IConfiguration Configuration { get; }

        public CustomersController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IActionResult Index()
        { 
            var customers = getAll();
            return View(customers);
        }
        public List<Customer> getAll()
        {
            using (var repo = new CustomersRepository(Configuration))
            {
                var customers = repo.GetAll();
                return customers;
            }
        }

        [HttpGet]
        public Customer Get(int customerNumber)
        {
            using (var repo = new CustomersRepository(Configuration))
            {
                var customer = repo.Get(customerNumber);
                return customer;
            }
        }
        [HttpGet]
        public int delete(int customerNumber)
        {
            using (var repo = new CustomersRepository(Configuration))
            {
                return repo.Delete(customerNumber);
            }
        }

        public void Save(Customer customer)
        {
            //var customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(customerString);
            using (var repo = new CustomersRepository(Configuration))
            {
                repo.Save(customer);
            }
        }
    }
}
