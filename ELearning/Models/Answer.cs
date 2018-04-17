using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Answer")]
        public string Text { get; set; }
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public int QuestionId { get; set; }
        //public virtual Question Question { get; set; }
    }
}
