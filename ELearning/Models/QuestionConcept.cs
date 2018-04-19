using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class QuestionConcept
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Question")]
        public int? QuestionId { get; set; }
        public virtual Question Question { get; set; }
        [ForeignKey("Concept")]
        public int ConceptId { get; set; }
        public virtual Concept Concept { get; set; }
    }
}
