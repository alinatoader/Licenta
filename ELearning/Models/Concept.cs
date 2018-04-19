using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class Concept
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<QuestionConcept> QuestionConcepts { get; set; }
    }
}
