using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearning.Data;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class UsersController : Controller
    {
        private readonly ELearningContext _context;

        public UsersController(ELearningContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}