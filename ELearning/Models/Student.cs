using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class Student : User
    {
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
