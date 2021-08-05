using ir35calc1.Models;
using ir35calc1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace ir35calc1.Controllers
{
    public class Ir35Controller : Controller
    {
        //private readonly ILogger<Ir35Controller> _logger;

        //public Ir35Controller(ILogger<Ir35Controller> logger)
        //{
        //    _logger = logger;
        //}

        // GET: /Ir35/
        public IActionResult Index()
        {
            ContractorGroup contractorGroup = IR35Service.InitialiseValues(); 
            return View(contractorGroup);
        }

        // POST: /Ir35/
        [HttpPost]
        public IActionResult Index(ContractorGroup contractorGroup)
        {
            if (!ModelState.IsValid)
            {
                // 1st way 
                //ModelState.AddModelError(string.Empty, "");

                // 2nd way 
                //ModelState.AddModelError("error_summary", "Please correct the following problems:");
                //User the above with HtmlHelper.ValidationMessageCssClassName in the view

                // invalid - redisplay form with errors
                return View(contractorGroup);
            }
            else
            {
                ModelState.Clear(); // required since writing back to same view 

                var contractorGroup2 = IR35Service.CalculateValues(contractorGroup);

                return View(contractorGroup2);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
