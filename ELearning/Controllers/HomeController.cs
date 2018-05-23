using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Start()
        {
            var user = HttpContext.Session.GetString("user");
            if (user == "prof")
            {
                return RedirectToAction("IncomingQuestions", "Questions");
            }
            else
            {
                return RedirectToAction("Create", "Questions");
            }
        }
    }
}