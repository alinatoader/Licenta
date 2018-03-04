using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELearning.Models;
using ELearning.Data;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ELearningContext _context;

        public QuestionsController(ELearningContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddQuestion()
        {
            ViewData["Message"] = "Create";
            return View();
        }

        public async Task<IActionResult> PublicQuestions()
        {
            return View(await _context.Questions.AsNoTracking().ToArrayAsync());

        }

        public async Task<IActionResult> MyQuestions()
        {
            return View(await _context.Questions.AsNoTracking().ToArrayAsync());

        }



    }
}
