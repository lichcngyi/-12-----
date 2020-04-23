using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UsingOptionsSample.Config;
using UsingOptionsSample.Models;

namespace UsingOptionsSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyOptions options;

        private readonly SubOptions subOptions;

        private readonly SubOptions subOption1;

        private readonly SubOptions subOption2;

        public HomeController(IOptions<MyOptions> optionAccessor, IOptions<SubOptions> subOptionAccessor,IOptionsSnapshot<SubOptions> nameOptionsAccess)
        {
            options = optionAccessor.Value;
            subOptions = subOptionAccessor.Value;

            subOption1= nameOptionsAccess.Get("sub1");
            subOption2 = nameOptionsAccess.Get("sub2");
        }

        public IActionResult Index()
        {
            ViewBag.Option1 = options.Option1;
            ViewBag.Option2 = options.Option2;
            return View();
        }

        public IActionResult About()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("customConfig.json", true, true)
                .AddXmlFile("faf.xml")
                .AddInMemoryCollection(new Dictionary<string, string>())
                .Build();
                
           

            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
