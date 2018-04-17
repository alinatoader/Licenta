using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public string Domain { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }
        //public virtual ICollection<Question> Questions { get; set; }
    }
}
