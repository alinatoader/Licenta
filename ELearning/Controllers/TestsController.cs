using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearning.Data;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class TestsController : Controller
    {
        private readonly ELearningContext _context;

        public TestsController(ELearningContext context)
        {
            _context = context;
        }
    }
}