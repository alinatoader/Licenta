using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Data limita")]
        public DateTime Deadline { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        [Display(Name = "Grupa")]
        public virtual Group Group { get; set; }
        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }
        [ForeignKey("Concept")]
        public int ConceptId { get; set; }
        [Display(Name = "Concept")]
        public virtual Concept Concept { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
