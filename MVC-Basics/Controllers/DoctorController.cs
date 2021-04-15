using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Basics.Controllers
{
    public class DoctorController : Controller
    {
        [HttpGet]
        public IActionResult FeverCheck()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FeverCheck(float temp, string type)
        {
            string message = "wrong";


            message = CheckFeverUtility.CheckFever(temp, type);

            /* below is the variant if temp is coming in as a string*/
            /* if (string.IsNullOrWhiteSpace(temp))
           {
               message = "Enter a temperature with numbers.";
           }
           else
           {
               message = CheckFeverUtility.CheckFever(temp, type);
           }*/

            ViewBag.message = message;
            ViewBag.temperature = temp;
            ViewBag.temptype = type;
            return View("FeverCheckResult");
        }
    }
}
