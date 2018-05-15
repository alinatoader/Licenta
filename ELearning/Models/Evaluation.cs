using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class Evaluation
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Test")]
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int Percentage { get; set; }
    }
}
