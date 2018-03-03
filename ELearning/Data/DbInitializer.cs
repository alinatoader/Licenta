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
                new Question{Text = "Ce faci?"},
            };
            foreach (Question q in questions)
            {
                context.Questions.Add(q);
            }
            context.SaveChanges();
        }
    }
}
