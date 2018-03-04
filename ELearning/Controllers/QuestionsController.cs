using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELearning.Models;
using ELearning.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        public IActionResult Create()
        {
            return View(new Question { Status = QuestionStatus.Pending, Answers = new List<Answer> { new Answer(), new Answer(), new Answer() } });
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Text,Status,Answers")]Question question)
        {
            if (ModelState.IsValid)
            {
                var exists = await _context.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.Text == question.Text);
                if (exists == null)
                {
                    await _context.AddAsync(question);
                    await _context.SaveChangesAsync();
                    ViewData["Message"] = "Question added successfully";
                }
                else
                {
                    ViewData["Message"] = "This question already exists";
                }
            }
            return View(question);
        }

        public async Task<IActionResult> PublicQuestions()
        {
            return View(await _context.Questions.AsNoTracking().Include(q => q.Answers).Where(q => q.Status == QuestionStatus.Accepted).ToArrayAsync());

        }

        public async Task<IActionResult> MyQuestions()
        {
            return View(await _context.Questions.AsNoTracking().ToArrayAsync());
        }

        public async Task<IActionResult> IncomingQuestions()
        {
            var x= await _context.Questions.AsNoTracking().Include(q => q.Answers).Where(q => q.Status == QuestionStatus.Pending).ToArrayAsync();
            return View(x) ;
        }

       public async Task<IActionResult> AcceptQuestion(int id)
        {
            var question = await _context.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
                return BadRequest();
            question.Status = QuestionStatus.Accepted;
            _context.Update(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IncomingQuestions));
        }

        public async Task<IActionResult> RejectQuestion(int id)
        {
            var question = await _context.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
                return BadRequest();
            question.Status = QuestionStatus.Rejected;
            _context.Update(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IncomingQuestions));
        }

    }
}
