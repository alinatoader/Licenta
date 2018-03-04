using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Models
{
    public enum QuestionStatus
    {
        Accepted,
        Pending,
        Rejected
    }
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionStatus Status { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}