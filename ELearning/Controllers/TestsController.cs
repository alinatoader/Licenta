using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearning.Data;
using ELearning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Controllers
{
    public class TestsController : Controller
    {
        private readonly ELearningContext _context;

        public TestsController(ELearningContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateRandom()
        {
            return View(new TestViewModel() { Questions = new List<Question>(), MaxNoQuestions = await _context.Questions.AsNoTracking().Include(q => q.Answers).CountAsync() });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRandom([Bind("NoQuestions")]TestViewModel model)
        {
            if (ModelState.IsValid)
            {
                Random a = new Random();
                List<int> indexes = new List<int>();
                var questions = await _context.Questions.AsNoTracking().Include(q => q.Answers).ToListAsync();
                if (model.NoQuestions > questions.Count)
                    model.NoQuestions = questions.Count;
                if (model.NoQuestions == questions.Count)
                    model.Questions = questions;
                else
                {
                    for (var i = 0; i < model.NoQuestions; i++)
                    {
                        int nr = a.Next(0, questions.Count - 1);
                        if (!indexes.Contains(nr))
                            indexes.Add(nr);
                    }
                    model.Questions = new List<Question>();
                    foreach (var index in indexes)
                    {
                        model.Questions.Add(questions.ElementAt(index));
                    }
                }
            }
            return View(model);
        }
    }
}