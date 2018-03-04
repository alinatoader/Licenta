using ELearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ELearningContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            var questions = new Question[]
            {
                new Question{Text = "What is your name?", Status = QuestionStatus.Pending},
            };
            context.Questions.AddRange(questions);
            context.SaveChanges();


            var answers = new Answer[]
            {
                new Answer{Text = "Alina", Comment="This is true", Question = context.Questions.First()},
                new Answer{Text = "Elena", Comment="This is also true", Question = context.Questions.First()},
                new Answer{Text = "Ioana", Comment="This is false", Question = context.Questions.First()},
            };
            context.Answers.AddRange(answers);
            context.SaveChanges();
        }
    }
}
