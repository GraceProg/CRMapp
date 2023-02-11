using CRM.Models.DBClasses;
using FastReport.Export.PdfSimple;
using FastReport;
using Microsoft.AspNetCore.Mvc;
using FastReport.Web;
using FastReport.Export.Html;
using CRM.Models.Repositories;
using System.Drawing;

namespace CRM.Controllers
{
    //[Authorize(Roles = "Employee")]
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
            using (var callsRepo = new CustomerCallsRepository(Configuration))
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var path = Path.Combine(Directory.GetParent(webRootPath)?.FullName, "calls.frx");
                var webReport = new WebReport();
                webReport.Toolbar.Exports.ExportTypes = Exports.All;
                webReport.Toolbar.Color = Color.Cornsilk;
                webReport.Toolbar.ShowPrint = true;
                webReport.Report.Load(path);
                var calls = callsRepo.GetAll();
                webReport.Report.RegisterData(calls, "CustomerCallRef");
                return View(webReport);
            }
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