using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class AssignmentViewModel
    {
        public int DeadlineSortDir { get; set; }
        public int ProfessorId { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}
