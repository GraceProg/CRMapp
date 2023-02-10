using CRM.Models;
using CRM.Models.DBClasses;
using CRM.Models.Repositories;

using FastReport;
using FastReport.Export.PdfSimple;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Net;
using System.Web;

namespace CRM.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IConfiguration Configuration { get; }

        public CustomersController(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _webHostEnvironment = env;
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



        [HttpGet]
        public FileResult Generate()
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            var contentRootPath = _webHostEnvironment.ContentRootPath;

            var path = Path.Combine(Directory.GetParent(webRootPath)?.FullName, "customerCalls.frx");


            FastReport.Utils.Config.WebMode= true;
            var rep = new Report();
            rep.Load(path);

            var customers = new List<Customer>
            {
                new Customer { Number = 1, Name = "John", SurName = "Doe", DateOfBirth = DateTime.Now, Country = "Cameroon"},
                new Customer { Number = 1, Name = "John", SurName = "Doe", DateOfBirth = DateTime.Now, Country = "Cameroon"},
                new Customer { Number = 1, Name = "John", SurName = "Doe", DateOfBirth = DateTime.Now, Country = "Cameroon"},
                new Customer { Number = 1, Name = "John", SurName = "Doe", DateOfBirth = DateTime.Now, Country = "Cameroon"},
                new Customer { Number = 1, Name = "John", SurName = "Doe", DateOfBirth = DateTime.Now, Country = "Cameroon"},
                new Customer { Number = 1, Name = "John", SurName = "Doe", DateOfBirth = DateTime.Now, Country = "Cameroon"},
                new Customer { Number = 1, Name = "John", SurName = "Doe", DateOfBirth = DateTime.Now, Country = "Cameroon"},
                new Customer { Number = 1, Name = "John", SurName = "Doe", DateOfBirth = DateTime.Now, Country = "Cameroon"},
                new Customer { Number = 1, Name = "John", SurName = "Doe", DateOfBirth = DateTime.Now, Country = "Cameroon"},
                new Customer { Number = 1, Name = "John", SurName = "Doe", DateOfBirth = DateTime.Now, Country = "Cameroon"},
            };

            rep.SetParameterValue("ReportPeriod", "January 2023");
            rep.RegisterData(customers, "CustomerRef");

            if(rep.Report.Prepare())
            {
                var pdfExport = new PDFSimpleExport();
                pdfExport.ShowProgress = false;
                pdfExport.Subject = "Subject report";
                pdfExport.Title = "Report Title";
                var ms = new MemoryStream();
                rep.Report.Export(pdfExport , ms);
                rep.Dispose();
                ms.Position = 0;

                return File(ms, "application/pdf", "myreport.pdf");
            }
            else
            {
                return null;
            }
        }
    }
}
