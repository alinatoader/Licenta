using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Models
{
    public enum QuestionStatus
    {
        Accepted,
        Pending,
        Rejected
    }

    public enum QuestionDifficulty
    {
        Easy, 
        Medium,
        Hard
    }
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Question")]
        public string Text { get; set; }
        [Display(Name = "Status")]
        public QuestionStatus Status { get; set; }
        public QuestionDifficulty Difficulty { get; set; }
        [ForeignKey("Student")]
        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }
        [ForeignKey("Assignment")]
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
        public virtual ICollection<QuestionConcept> QuestionConcepts { get; set; }

    }
}