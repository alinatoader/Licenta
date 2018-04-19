using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class Professor : User
    {
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
