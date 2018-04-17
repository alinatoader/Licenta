using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
    }
}
