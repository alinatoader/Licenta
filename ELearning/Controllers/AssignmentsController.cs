using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELearning.Data;
using ELearning.Models;

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
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id","Name");
            // ViewData["Domain"] = new SelectList(_context.Assignments.AsNoTracking().Select(a => a.Domain));
            ViewData["Domain"] = new SelectList(new[] { "Baze de date", "Sisteme de operare", "Metrici soft" });
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Deadline,Domain,GroupId,ProfessorId")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                var group = _context.Groups.AsNoTracking().FirstOrDefault(g => g.Name == assignment.GroupId.ToString());
                if(group == null)
                {
                    group = new Group() { Name = assignment.GroupId.ToString() };
                    _context.Add(group);
                    await _context.SaveChangesAsync();
                }
                assignment.GroupId = group.Id;
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            // ViewData["Domain"] = new SelectList(_context.Assignments.AsNoTracking().Select(a => a.Domain));
            ViewData["Domain"] = new SelectList(new[] { "Baze de date", "Sisteme de operare", "Metrici soft" });
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
    }
}
