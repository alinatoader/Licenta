using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public ICollection<int> AnswerIds { get; set; }
    }
    
}
