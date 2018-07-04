using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearning.Data;
using ELearning.Models;
using Microsoft.AspNetCore.Http;
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
            var allQuestions = await _context.Questions.AsNoTracking().Include(q => q.QuestionConcepts).ToListAsync();
            foreach (var section in test.Sections)
            {
                section.SectionQuestions = new List<SectionQuestion>();
                var questions = allQuestions.Where(q => q.QuestionConcepts.Any(qc => qc.ConceptId == section.ConceptId) && q.Difficulty == section.Difficulty).ToList();
                if (questions.Count > 0)
                {
                    for (int i = 0; i < section.NumberOfQuestions; i++)
                    {
                        var index = random.Next(0, questions.Count);
                        section.SectionQuestions.Add(new SectionQuestion() { QuestionId = questions.ElementAt(index).Id });
                        allQuestions.Remove(questions.ElementAt(index));
                        questions.RemoveAt(index);
                        if (questions.Count == 0)
                            i = section.NumberOfQuestions + 1;
                    }
                }
            }
            TempData["Test"] = JsonConvert.SerializeObject(test);
            return StatusCode(200, HttpContext.Session.GetString("user"));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var test = new Test();
            test = JsonConvert.DeserializeObject<Test>(TempData["Test"] as string);
            TempData.Keep();

            foreach (var section in test.Sections)
            {
                section.Concept = await _context.Concepts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == section.ConceptId);
                foreach (var question in section.SectionQuestions)
                    question.Question = await _context.Questions.AsNoTracking().Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == question.QuestionId);
            }
            return View(test);
        }

        public async Task<IActionResult> Details(int id)
        {
            var test = new Test();

            test = await _context.Tests.AsNoTracking().Include(t => t.Sections).FirstOrDefaultAsync(t => t.Id == id);
            foreach (var section in test.Sections)
            {
                var fullSection = await _context.Sections.AsNoTracking().Include(s => s.SectionQuestions).FirstOrDefaultAsync(ss => ss.Id == section.Id);
                section.SectionQuestions = fullSection.SectionQuestions;
            }

            foreach (var section in test.Sections)
            {
                section.Concept = await _context.Concepts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == section.ConceptId);
                foreach (var question in section.SectionQuestions)
                    question.Question = await _context.Questions.AsNoTracking().Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == question.QuestionId);
            }
            return View(test);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTest(Test model)
        {
            var test = JsonConvert.DeserializeObject<Test>(TempData["Test"] as string);
            test.Name = model.Name;
            test.ProfessorId = HttpContext.Session.GetInt32("ID");
            try
            {
                await _context.Tests.AddAsync(test);
                await _context.SaveChangesAsync();
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Index()
        {
            var tests = await _context.Tests.Include(t => t.Professor).AsNoTracking().Where(t => t.StudentId == null).ToListAsync();
            return View(tests);
        }

        public async Task<IActionResult> StudentIndex()
        {
            var tests = await _context.Tests.Include(t => t.Professor).AsNoTracking().Where(t => t.StudentId == null || t.StudentId == 1).ToListAsync();
            return View(tests);
        }

        public async Task<IActionResult> TakeTest(int? id)
        {
            var test = new Test();
            if (id == null)
            {
                test = JsonConvert.DeserializeObject<Test>(TempData["Test"] as string);
                TempData.Keep();
                foreach (var section in test.Sections)
                {
                    section.Concept = await _context.Concepts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == section.ConceptId);
                    foreach (var question in section.SectionQuestions)
                        question.Question = await _context.Questions.AsNoTracking().Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == question.QuestionId);
                }
            }
            else
            {
                test = await _context.Tests.AsNoTracking().Include(t => t.Sections).FirstOrDefaultAsync(t => t.Id == id);
                //TempData["Test"] = JsonConvert.SerializeObject(new Test() { Id = test.Id, StudentId = test.StudentId, Name = test.Name });
                foreach (var section in test.Sections)
                {
                    var fullSection = await _context.Sections.AsNoTracking().Include(s => s.SectionQuestions).FirstOrDefaultAsync(ss => ss.Id == section.Id);
                    section.SectionQuestions = fullSection.SectionQuestions;
                }

                foreach (var section in test.Sections)
                {
                    section.Concept = await _context.Concepts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == section.ConceptId);
                    foreach (var question in section.SectionQuestions)
                        question.Question = await _context.Questions.AsNoTracking().Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == question.QuestionId);
                }
            }
            return View(test);
        }

        [HttpPost]
        public async Task<IActionResult> TakeTest(TestViewModel test)
        {
            var noCorrectAnswers = 0;
            Test model;
            if (TempData.Keys.Contains("Test"))
            {
                model = JsonConvert.DeserializeObject<Test>(TempData["Test"] as string);
                if (model.Id == 0)
                {
                    model.StudentId = HttpContext.Session.GetInt32("ID");
                    model.Name = test.Name;
                    await _context.Tests.AddAsync(model);
                    await _context.SaveChangesAsync();
                }
            }
            else { model = await _context.Tests.AsNoTracking().FirstOrDefaultAsync(t => t.Name == test.Name); }
            foreach (var question in test.Questions)
            {
                var isCorrect = true;
                var fullAnswers = await _context.Answers.AsNoTracking().Where(a => a.QuestionId == question.QuestionId && a.Correct == true).ToListAsync();
                foreach (var answer in fullAnswers)
                    if (!question.AnswerIds.Contains(answer.Id))
                    {
                        isCorrect = false;
                        break;
                    }
                if (isCorrect)
                    noCorrectAnswers++;
            }

            var percentage = noCorrectAnswers / (double)test.Questions.Count * 100;
            var eval = new Evaluation() { TestId = model.Id, StudentId = (int)HttpContext.Session.GetInt32("ID"), Percentage = percentage };
            await _context.Evaluations.AddAsync(eval);
            await _context.SaveChangesAsync();
            return StatusCode(200, percentage);
        }
    }
}