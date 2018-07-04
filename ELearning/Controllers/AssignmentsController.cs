using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELearning.Data;
using ELearning.Models;
using Microsoft.AspNetCore.Http;

namespace ELearning.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly ELearningContext _context;

        public AssignmentsController(ELearningContext context)
        {
            _context = context;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            var eLearningContext = _context.Assignments.Include(a => a.Group).Include(a => a.Professor);
            return View(await eLearningContext.ToListAsync());
        }

        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.Group)
                .Include(a => a.Professor)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: Assignments/Create
        public IActionResult Create(string message=null)
        {
            ViewData["Group"] = new SelectList(_context.Groups.AsNoTracking(), "Id", "Name");
            ViewData["Concept"] = new SelectList(_context.Concepts.AsNoTracking(), "Id", "Name");
            ViewData["Message"] = message;
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Deadline,ProfessorId,Concept,Group")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                var group = _context.Groups.AsNoTracking().FirstOrDefault(g => g.Name == assignment.Group.Name);
                if (group == null)
                {
                    group = new Group() { Name = assignment.Group.Name };
                    _context.Add(group);
                    await _context.SaveChangesAsync();
                }
                var concept = _context.Concepts.AsNoTracking().FirstOrDefault(c => c.Name == assignment.Concept.Name);
                if (concept == null)
                {
                    concept = new Concept() { Name = assignment.Concept.Name };
                    _context.Add(concept);
                    await _context.SaveChangesAsync();
                }
                assignment.GroupId = group.Id;
                assignment.ConceptId = concept.Id;
                assignment.Group = null;
                assignment.Concept = null;
                assignment.ProfessorId = (int) HttpContext.Session.GetInt32("ID");
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create),new { message = "Tema trimisa cu succes" });
            }
            ViewData["Group"] = new SelectList(_context.Groups.AsNoTracking(), "Id", "Name", assignment.Group);
            ViewData["Concept"] = new SelectList(_context.Concepts.AsNoTracking(), "Id", "Name", assignment.Concept);
            
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments.SingleOrDefaultAsync(m => m.Id == id);
            if (assignment == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", assignment.GroupId);
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "Id", "Id", assignment.ProfessorId);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Deadline,Domain,GroupId,ProfessorId")] Assignment assignment)
        {
            if (id != assignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", assignment.GroupId);
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "Id", "Id", assignment.ProfessorId);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.Group)
                .Include(a => a.Professor)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignments.SingleOrDefaultAsync(m => m.Id == id);
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> MyAssignments()
        {
            var assignments = await FindMyAssignments();
            var profs = assignments.Select(a => a.Professor).Distinct().ToList();
            profs.Add(new Professor { FirstName = "Toti", LastName = "" });
            ViewData["Professors"] = new SelectList(profs, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MyAssignments(AssignmentViewModel model)
        {
            var assignments = await FindMyAssignments();
            if (model.ProfessorId > 0)
                assignments = assignments.Where(a => a.ProfessorId == model.ProfessorId).ToList();
            if (model.DeadlineSortDir == 0)
                assignments = assignments.OrderBy(a => a.Deadline).ToList();
            else assignments = assignments.OrderByDescending(a => a.Deadline).ToList();
            model.Assignments = assignments;
            var profs = assignments.Select(a => a.Professor).Distinct().ToList();
            profs.Add(new Professor { FirstName = "Toti", LastName = "" });
            ViewData["Professors"] = new SelectList(profs, "Id", "FullName", model.ProfessorId);
            return View(model);

        }

        private async Task<ICollection<Assignment>> FindMyAssignments()
        {
            var id = HttpContext.Session.GetInt32("ID");
            var user = await _context.Students.AsNoTracking().Include(u => u.Group).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }
            var assignments = await _context.Assignments.AsNoTracking().Include(a => a.Professor).Include(a => a.Concept).Where(a => a.GroupId == user.GroupId).ToListAsync();
            return assignments;
        }
    }
}
