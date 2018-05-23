using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using ELearning.Data;
using ELearning.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                return RedirectToAction("Start", "Home");
            }
            var prof = await _context.Professors.AsNoTracking().FirstOrDefaultAsync(p => p.Email == model.Email);
            if (prof != null && new PasswordHasher<Professor>().VerifyHashedPassword(prof,prof.Password,model.Password) != PasswordVerificationResult.Failed)
            {
                HttpContext.Session.SetInt32("ID", prof.Id);
                HttpContext.Session.SetString("user", "prof");
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
    }
}