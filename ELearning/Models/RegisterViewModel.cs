using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public enum UserType
    {
        Student,
        Profesor
    }
    public class RegisterViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email nu poate fi vid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parola nu poate fi vida")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Prenumele nu poate fi vid")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Numele nu poate fi vid")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Rolul nu poate fi vid")]
        public UserType Rol { get; set; }
        public string Group { get; set; }
    }
}
