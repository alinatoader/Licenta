using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class TestViewModel
    {
        public int NoQuestions { get; set; }
        public int MaxNoQuestions { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
