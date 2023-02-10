using CRM.Models;
using CRM.Models.DBClasses;
using CRM.Models.Repositories;

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
            using (var repo = new CustomerCallsRepository(Configuration))
            {
                var customerCalls = repo.GetAll();
                return View(customerCalls);
            }
        }



        [HttpGet]
        public CustomerCall? Get(Guid id)
        {
            using (var repo = new CustomerCallsRepository(Configuration))
            {
                var call = repo.Get(id);
                return call;
            }
        }
        [HttpGet]
        public int delete(Guid id)
        {
            using (var repo = new CustomerCallsRepository(Configuration))
            {
                return repo.Delete(id);
            }
        }

        public void Save(CustomerCall call)
        {
            using (var repo = new CustomerCallsRepository(Configuration))
            {
                if(call.Id == Guid.Empty)
                {
                    call.Id = Guid.NewGuid();
                }
                repo.Save(call);
            }
        }

    }
}