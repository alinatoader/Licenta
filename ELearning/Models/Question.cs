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
        [Display(Name = "Usor")]
        Easy,
        [Display(Name = "Mediu")]
        Medium,
        [Display(Name = "Greu")]
        Hard
    }
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Question")]
        public string Text { get; set; }
        public string Comment { get; set; }
        [Display(Name = "Status")]
        public QuestionStatus? Status { get; set; }
        [Display(Name = "Dificultate")]
        public QuestionDifficulty? Difficulty { get; set; }
        public string ProfessorName { get; set; }
        [ForeignKey("Student")]
        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }
        [ForeignKey("Assignment")]
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }
        public virtual IList<Answer> Answers { get; set; }
        public virtual ICollection<SectionQuestion> TestQuestions { get; set; }
        [Display(Name = "Concepte")]
        public virtual ICollection<QuestionConcept> QuestionConcepts { get; set; }

    }
}