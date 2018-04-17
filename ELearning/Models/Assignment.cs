using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        [Display(Name = "Data limita")]
        public DateTime Deadline { get; set; }
        [Display(Name = "Domeniu")]
        public string Domain { get; set; }
        [Display(Name = "Grupa")]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }
        //public virtual ICollection<Question> Questions { get; set; }
    }
}
