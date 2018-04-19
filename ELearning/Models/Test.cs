using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }

    }
}
