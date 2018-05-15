using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class Section
    {
        [Key]
        public int Id { get; set; }
        public int? TestId { get; set; }
        public virtual Test Test { get; set; }
        public int? ConceptId { get; set; }
        public virtual Concept Concept { get; set; }
        public QuestionDifficulty Difficulty { get; set; }
        public int NumberOfQuestions { get; set; }
        public virtual ICollection<SectionQuestion> SectionQuestions { get; set; }
    }
}
