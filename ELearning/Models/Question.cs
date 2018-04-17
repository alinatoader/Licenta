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
        [Display(Name = "Question")]
        public string Text { get; set; }
        [Display(Name = "Status")]
        public QuestionStatus Status { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int AssignmentId { get; set; }
        //public virtual Assignment Assignment { get; set; }
        public virtual IList<Answer> Answers { get; set; }
        //public virtual ICollection<TestQuestion> TestQuestions { get; set; }

    }
}