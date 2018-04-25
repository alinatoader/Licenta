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
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELearning.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ELearningContext _context;

        public QuestionsController(ELearningContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(int? id)
        {
            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == 1);
            if (id != null)
                ViewData["Assignment"] = new SelectList(_context.Assignments.AsNoTracking().Include(a => a.Concept).Include(a => a.Professor).Where(a => a.GroupId == student.GroupId), "Id", "ComposedName", (int)id);
            else
                ViewData["Assignment"] = new SelectList(_context.Assignments.AsNoTracking().Include(a => a.Concept).Include(a => a.Professor).Where(a => a.GroupId == student.GroupId), "Id", "ComposedName");
            return View(new Question { AssignmentId = id != null ? (int)id : 0, Status = QuestionStatus.Pending, Answers = new List<Answer> { new Answer(), new Answer(), new Answer() } });
        }

        [HttpPost]
        public async Task<string> Create(Question question)
        {
            if (ModelState.IsValid)
            {
                var exists = await _context.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.Text == question.Text);
                if (exists == null)
                {
                    question.Status = QuestionStatus.Pending;
                    await _context.AddAsync(question);
                    await _context.SaveChangesAsync();
                    return "Intrebare adaugata cu succes!";
                }
                else
                {
                    return "Aceasta intrebare exista deja. Mai incearca!";
                }
            }
            return "Completeaza cu date valide!";
        }


        public async Task<IActionResult> IncomingQuestions()
        {
            return View(await _context.Questions.AsNoTracking().Include(q => q.Answers).Where(q => q.Status == QuestionStatus.Pending).ToArrayAsync());
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.AsNoTracking().Include(q => q.Answers).Include(q => q.Student).SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.AsNoTracking().Include(q => q.Answers).SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            var currentAssignment = await _context.Assignments.AsNoTracking().Include(a => a.Concept).Include(a => a.Professor).FirstOrDefaultAsync(a => a.Id == question.AssignmentId);
            ViewData["Assignment"] = new SelectList(_context.Assignments.AsNoTracking().Include(a => a.Concept).Include(a => a.Professor).Where(a => a.GroupId == 4), "Id", "ComposedName", currentAssignment.ComposedName);
            return View(question);
        }

        [HttpPost]
        public async Task<string> Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    question.Status = QuestionStatus.Pending;
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return "";
                    }
                    else
                    {
                        throw;
                    }
                }
                return "Intrebare trimisa cu succes!";
            }
            return "Completeaza cu date valide!";
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, [Bind("Id, Text, Status, StudentId, Answers")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    question.Status = QuestionStatus.Accepted;
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IncomingQuestions));
            }
            return View(question);
        }

        public async Task<IActionResult> RejectQuestion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            try
            {
                question.Status = QuestionStatus.Rejected;
                _context.Update(question);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(question.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(IncomingQuestions));
        }

        public async Task<IActionResult> MyAcceptedQuestions(int id)
        {
            id = 1;
            var student = await _context.Students.AsNoTracking().Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
                return BadRequest();
            var questions = await _context.Questions.AsNoTracking().Include(q => q.Answers).Include(q => q.Assignment).Include(q => q.Assignment.Concept).Include(q => q.Assignment.Professor).Include(q => q.QuestionConcepts).ThenInclude(qc => qc.Concept).Where(q => q.StudentId == student.Id && q.Status == QuestionStatus.Accepted).ToListAsync();
            return View(questions);
        }

        public async Task<IActionResult> MyPendingQuestions(int id)
        {
            id = 1;
            var student = await _context.Students.AsNoTracking().Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
                return BadRequest();
            return View(student.Questions.Where(q => q.Status == QuestionStatus.Pending));
        }

        public async Task<IActionResult> MyRejectedQuestions(int id)
        {
            id = 1;
            var student = await _context.Students.AsNoTracking().Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
                return BadRequest();
            var questions = student.Questions.Where(q => q.Status == QuestionStatus.Rejected);
            foreach (var q in questions)
            {
                var assignment = await _context.Assignments.AsNoTracking().Include(a => a.Professor).FirstOrDefaultAsync(a => a.Id == q.AssignmentId);
                q.ProfessorName = assignment.Professor.FullName;
            }
            return View(questions);
        }

        public async Task<IActionResult> Summary(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.AsNoTracking().Include(q => q.Answers).SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

    }
}
