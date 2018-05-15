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
        [Display(Name = "Test")]
        public string Name { get; set; }
        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }
        public ICollection<Section> Sections { get; set; }
        public virtual Professor Professor { get; set; }

        public virtual ICollection<Evaluation> Evaluations { get; set; }

    }
}
