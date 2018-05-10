using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearning.Data;
using ELearning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ELearning.Controllers
{
    public class TestsController : Controller
    {
        private readonly ELearningContext _context;

        public TestsController(ELearningContext context)
        {
            _context = context;
        }

        public IActionResult SelectSections()
        {
            ViewData["Concepts"] = new SelectList(_context.Concepts.AsNoTracking().ToList(), "Id", "Name");
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> GenerateTest(Test test)
        {
            Random random = new Random();
            foreach(var section in test.Sections)
            {
                var questions = await _context.Questions.AsNoTracking().Where(q => q.QuestionConcepts.Any(qc => qc.ConceptId == section.ConceptId) && q.Difficulty == section.Difficulty).ToListAsync();
                for(int i = 0; i < section.NumberOfQuestions; i++)
                {
                    var index = random.Next(0, questions.Count);
                    test.TestQuestions = new List<TestQuestion>();
                    test.TestQuestions.Add(new TestQuestion() { QuestionId = questions.ElementAt(index).Id });
                    questions.RemoveAt(index);
                    if (questions.Count == 0)
                        i = section.NumberOfQuestions + 1;
                }
            }
            TempData["Test"] = JsonConvert.SerializeObject(test);
            return StatusCode(200);
        }

        public IActionResult Edit()
        {
            var test = JsonConvert.DeserializeObject<Test>(TempData["Test"] as string);
            return View(test);
        }
    }
}