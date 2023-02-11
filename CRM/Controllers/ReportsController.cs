using CRM.Models.DBClasses;
using FastReport.Export.PdfSimple;
using FastReport;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using FastReport.Web;
using FastReport.Export.Html;
using FastReport.Utils;
using Microsoft.Extensions.Hosting;
using CRM.Models.Repositories;

namespace CRM.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IConfiguration Configuration { get; }

        public ReportsController(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _webHostEnvironment = env;
        }

        public IActionResult Index()
        {
            using(var customersRepo = new CustomersRepository(Configuration))
            {
                using (var callsRepo = new CustomerCallsRepository(Configuration))
                {
                    var webRootPath = _webHostEnvironment.WebRootPath;
                    var path = Path.Combine(Directory.GetParent(webRootPath)?.FullName, "customerCalls.frx");
                    var webReport = new WebReport();
                    webReport.Toolbar.Exports.ExportTypes = Exports.All;
                    webReport.Report.Load(path);
                    var customers = customersRepo.GetAll();
                    var calls = callsRepo.GetAll();
                    webReport.Report.RegisterData(customers, "CustomerRef");
                    webReport.Report.RegisterData(calls, "CustomerCallRef");
                    return View(webReport);
                }
            }
        }

        public IActionResult Report()
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            var path = Path.Combine(Directory.GetParent(webRootPath)?.FullName, "customerCalls.frx");
            var webReport = new WebReport();
            webReport.Report.Load(path);

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
            webReport.Report.SetParameterValue("ReportPeriod", "January 2023");
            webReport.Report.RegisterData(customers, "CustomerRef");
            return View(webReport);
        }

        [HttpGet]
        public FileResult Generate()
        {
            var webRootPath = _webHostEnvironment.WebRootPath;

            var path = Path.Combine(Directory.GetParent(webRootPath)?.FullName, "customerCalls.frx");


            FastReport.Utils.Config.WebMode = true;
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

            if (rep.Report.Prepare())
            {
                var htmlExport = new HTMLExport();
                var pdfExport = new PDFSimpleExport();
                pdfExport.ShowProgress = false;
                pdfExport.Subject = "Subject report";
                pdfExport.Title = "Report Title";
                var ms = new MemoryStream();
                rep.Report.Export(htmlExport, ms);
                rep.Dispose();
                ms.Position = 0;

                return File(ms, "text/html", "myreport.html");
            }
            else
            {
                return null;
            }
        }
    }
}