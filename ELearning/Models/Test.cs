using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class Test
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        //public virtual ICollection<TestQuestion> TestQuestions { get; set; }
    }
}
