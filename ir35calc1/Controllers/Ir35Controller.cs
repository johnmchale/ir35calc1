using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ir35calc1.Models;
using Microsoft.AspNetCore.Http;

namespace ir35calc1.Controllers
{
    public class Ir35Controller : Controller
    {
        private readonly ILogger<Ir35Controller> _logger;

        public Ir35Controller(ILogger<Ir35Controller> logger)
        {
            _logger = logger;
        }

        // GET: /Ir35/
        public IActionResult Index()
        {
            DayRate dayRate = new DayRate();
            //dayRate.Amount = 300; 
            
            Contractor contractorYearly  = new Contractor();
            contractorYearly.GrossPay = 0; 
            
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            Contractor contractorMonthly = new Contractor();
            contractorMonthly.GrossPay = 0; 
            
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            Contractor contractorWeekly  = new Contractor();
            contractorWeekly.GrossPay = 0; 
            
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            ContractorGroup contractorGroup = new ContractorGroup();

            contractorGroup.DayRateAmount = dayRate; 
            contractorGroup.Yearly = contractorYearly;
            contractorGroup.Monthly = contractorMonthly;
            contractorGroup.Weekly = contractorWeekly; 

            /*
            {
                Yearly = contractorYearly,
                Monthly = contractorMonthly,
                Weekly = contractorWeekly
            };
            */
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

                DayRate dayRate = new DayRate();
                Contractor contractorYearly = new Contractor();
                Contractor contractorMonthly = new Contractor();
                Contractor contractorWeekly = new Contractor();


                ContractorGroup contractorGroup2 = new ContractorGroup();

                contractorGroup2.DayRateAmount = dayRate;                 
                contractorGroup2.Yearly = contractorYearly;
                contractorGroup2.Monthly = contractorMonthly;
                contractorGroup2.Weekly = contractorWeekly;

                contractorGroup2.DayRateAmount.Amount = contractorGroup.DayRateAmount.Amount; 

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                contractorGroup2.Yearly.GrossPay = Math.Round(contractorGroup.DayRateAmount.Amount.Value * 5 * 52);
                contractorGroup2.Yearly.EmployersNi = Math.Round(contractorGroup2.Yearly.GrossPay * (decimal) 0.138);
                contractorGroup2.Yearly.EmployeesNiBandOne = 0;
                contractorGroup2.Yearly.EmployeesNiBandTwo = Math.Round((decimal) ((50000 - 8632) * 0.12));
                contractorGroup2.Yearly.EmployeesNiBandThree = Math.Round((contractorGroup2.Yearly.GrossPay - 50000) * (decimal) 0.02);
                contractorGroup2.Yearly.EmployeesNi = contractorGroup2.Yearly.EmployeesNiBandOne +
                                                      contractorGroup2.Yearly.EmployeesNiBandTwo +
                                                      contractorGroup2.Yearly.EmployeesNiBandThree;
                contractorGroup2.Yearly.GeneralExpenses = Math.Round(contractorGroup2.Yearly.GrossPay * (decimal) 0.05);
                contractorGroup2.Yearly.DeemedAmount =
                    contractorGroup2.Yearly.GrossPay - contractorGroup2.Yearly.GeneralExpenses;
                contractorGroup2.Yearly.PayeBandOne = 0;
                contractorGroup2.Yearly.PayeBandTwo = Math.Round((decimal) ((50000 - 12500) * 0.2));
                contractorGroup2.Yearly.PayeBandThree = Math.Round((contractorGroup2.Yearly.DeemedAmount - 50000) * (decimal) 0.4);
                contractorGroup2.Yearly.Paye = contractorGroup2.Yearly.PayeBandOne +
                                               contractorGroup2.Yearly.PayeBandTwo +
                                               contractorGroup2.Yearly.PayeBandThree;
                contractorGroup2.Yearly.TakeHomePay = contractorGroup2.Yearly.GrossPay -
                                                      contractorGroup2.Yearly.EmployersNi -
                                                      contractorGroup2.Yearly.EmployeesNi -
                                                      contractorGroup2.Yearly.Paye;
                contractorGroup2.Yearly.TakeHomePayPerc =
                    Math.Round((contractorGroup2.Yearly.TakeHomePay / contractorGroup2.Yearly.GrossPay) * 100);
                contractorGroup2.Yearly.TotalTaxPaid = contractorGroup2.Yearly.EmployersNi +
                                                       contractorGroup2.Yearly.EmployeesNi +
                                                       contractorGroup2.Yearly.Paye;
                contractorGroup2.Yearly.TotalTaxPaidPerc = 100 - contractorGroup2.Yearly.TakeHomePayPerc;

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                contractorGroup2.Monthly.GrossPay = Math.Round((contractorGroup.DayRateAmount.Amount.Value * 5 * 52) / 12);
                contractorGroup2.Monthly.EmployersNi = Math.Round(contractorGroup2.Monthly.GrossPay * (decimal) 0.138);
                contractorGroup2.Monthly.EmployeesNiBandOne = 0;
                contractorGroup2.Monthly.EmployeesNiBandTwo = Math.Round(contractorGroup2.Yearly.EmployeesNiBandTwo / 12);
                contractorGroup2.Monthly.EmployeesNiBandThree = Math.Round(contractorGroup2.Yearly.EmployeesNiBandThree / 12);
                contractorGroup2.Monthly.EmployeesNi = contractorGroup2.Monthly.EmployeesNiBandOne +
                                                       contractorGroup2.Monthly.EmployeesNiBandTwo +
                                                       contractorGroup2.Monthly.EmployeesNiBandThree;
                contractorGroup2.Monthly.GeneralExpenses = Math.Round(contractorGroup2.Monthly.GrossPay * (decimal) 0.05);
                contractorGroup2.Monthly.DeemedAmount =
                    contractorGroup2.Monthly.GrossPay - contractorGroup2.Monthly.GeneralExpenses;
                contractorGroup2.Monthly.PayeBandOne = 0;
                contractorGroup2.Monthly.PayeBandTwo = Math.Round(contractorGroup2.Yearly.PayeBandTwo / 12);
                contractorGroup2.Monthly.PayeBandThree = Math.Round(contractorGroup2.Yearly.PayeBandThree / 12);
                contractorGroup2.Monthly.Paye = contractorGroup2.Monthly.PayeBandOne +
                                               contractorGroup2.Monthly.PayeBandTwo +
                                               contractorGroup2.Monthly.PayeBandThree;
                contractorGroup2.Monthly.TakeHomePay = contractorGroup2.Monthly.GrossPay -
                                                      contractorGroup2.Monthly.EmployersNi -
                                                      contractorGroup2.Monthly.EmployeesNi -
                                                      contractorGroup2.Monthly.Paye;
                contractorGroup2.Monthly.TakeHomePayPerc =
                    Math.Round((contractorGroup2.Monthly.TakeHomePay / contractorGroup2.Monthly.GrossPay) * 100);
                contractorGroup2.Monthly.TotalTaxPaid = contractorGroup2.Monthly.EmployersNi +
                                                        contractorGroup2.Monthly.EmployeesNi +
                                                        contractorGroup2.Monthly.Paye;
                contractorGroup2.Monthly.TotalTaxPaidPerc = 100 - contractorGroup2.Monthly.TakeHomePayPerc;

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                contractorGroup2.Weekly.GrossPay = Math.Round((contractorGroup.DayRateAmount.Amount.Value * 5 * 52) / 52);
                contractorGroup2.Weekly.EmployersNi = Math.Round(contractorGroup2.Weekly.GrossPay * (decimal) 0.138);
                contractorGroup2.Weekly.EmployeesNiBandOne = 0;
                contractorGroup2.Weekly.EmployeesNiBandTwo = Math.Round(contractorGroup2.Yearly.EmployeesNiBandTwo / 52);
                contractorGroup2.Weekly.EmployeesNiBandThree = Math.Round(contractorGroup2.Yearly.EmployeesNiBandThree / 52);
                contractorGroup2.Weekly.EmployeesNi = contractorGroup2.Weekly.EmployeesNiBandOne +
                                                      contractorGroup2.Weekly.EmployeesNiBandTwo +
                                                      contractorGroup2.Weekly.EmployeesNiBandThree;
                contractorGroup2.Weekly.GeneralExpenses = Math.Round(contractorGroup2.Weekly.GrossPay * (decimal) 0.05);
                contractorGroup2.Weekly.DeemedAmount =
                    contractorGroup2.Weekly.GrossPay - contractorGroup2.Weekly.GeneralExpenses;
                contractorGroup2.Weekly.PayeBandOne = 0;
                contractorGroup2.Weekly.PayeBandTwo = Math.Round(contractorGroup2.Yearly.PayeBandTwo / 52);
                contractorGroup2.Weekly.PayeBandThree = Math.Round(contractorGroup2.Yearly.PayeBandThree / 52);
                contractorGroup2.Weekly.Paye = contractorGroup2.Weekly.PayeBandOne +
                                               contractorGroup2.Weekly.PayeBandTwo +
                                               contractorGroup2.Weekly.PayeBandThree;
                contractorGroup2.Weekly.TakeHomePay = contractorGroup2.Weekly.GrossPay -
                                                      contractorGroup2.Weekly.EmployersNi -
                                                      contractorGroup2.Weekly.EmployeesNi -
                                                      contractorGroup2.Weekly.Paye;
                contractorGroup2.Weekly.TakeHomePayPerc =
                    Math.Round((contractorGroup2.Weekly.TakeHomePay / contractorGroup2.Weekly.GrossPay) * 100);
                contractorGroup2.Weekly.TotalTaxPaid = contractorGroup2.Weekly.EmployersNi +
                                                        contractorGroup2.Weekly.EmployeesNi +
                                                        contractorGroup2.Weekly.Paye;
                contractorGroup2.Weekly.TotalTaxPaidPerc = 100 - contractorGroup2.Weekly.TakeHomePayPerc;

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
