using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Start()
        {
            //daca e profesor
            return RedirectToAction("IncomingQuestions", "Questions");
            //daca e student
            //return RedirectToAction("Create", "Questions", new { id = 1 });
        }
    }
}