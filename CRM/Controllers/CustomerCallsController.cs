using CRM.Models.DBClasses;
using CRM.Models.Repositories;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace CRM.Controllers
{
    //[Authorize(Roles = "Employee")]
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

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}