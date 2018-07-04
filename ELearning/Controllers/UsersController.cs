using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearning.Data;
using ELearning.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Email == model.Email);
            if (student != null && new PasswordHasher<Student>().VerifyHashedPassword(student, student.Password, model.Password) != PasswordVerificationResult.Failed)
            {
                HttpContext.Session.SetInt32("ID", student.Id);
                HttpContext.Session.SetString("user", "student");
                HttpContext.Session.SetString("name", student.FullName + " - Student");
                return RedirectToAction("Start", "Home");
            }
            var prof = await _context.Professors.AsNoTracking().FirstOrDefaultAsync(p => p.Email == model.Email);
            if (prof != null && new PasswordHasher<Professor>().VerifyHashedPassword(prof,prof.Password,model.Password) != PasswordVerificationResult.Failed)
            {
                HttpContext.Session.SetInt32("ID", prof.Id);
                HttpContext.Session.SetString("user", "prof");
                HttpContext.Session.SetString("name", prof.FullName + " - Profesor");
                return RedirectToAction("Start", "Home");
            }
            ViewData["Message"] = "Credentiale invalide. Mai incearca!";
            return View();
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            ViewData["Group"] = new SelectList(_context.Groups.AsNoTracking(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                if(user.Rol == UserType.Student)
                {
                    var student = new Student { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
                    var group = await _context.Groups.AsNoTracking().FirstOrDefaultAsync(g => g.Name == user.Group);
                    if (group == null)
                    {
                        student.Group = new Group { Name = user.Group };
                    }
                    else { student.GroupId = group.Id; }
                    student.Password = new PasswordHasher<Student>().HashPassword(student, user.Password);
                    _context.Add(student);
                    _context.SaveChanges();
                }
                else
                {
                    var prof = new Professor { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
                    prof.Password = new PasswordHasher<Professor>().HashPassword(prof, user.Password);
                    _context.Add(prof);
                    _context.SaveChanges();
                }
                return RedirectToAction("Login");
            }
            return View(user);
        }
    }
}