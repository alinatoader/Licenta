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

            var groups = new Group[]
            {
                new Group{Name = "231"},
                new Group{Name = "232"},
                new Group{Name = "233"},
                new Group{Name = "234"},
                new Group{Name = "235"},
                new Group{Name = "236"},
                new Group{Name = "237"},
            };
            context.Groups.AddRange(groups);
            context.SaveChanges();

            var profs = new Professor[]
            {
                new Professor{FirstName="Camelia", LastName="Serban", Email="camelia.serban@yahoo.com"}
            };
            context.Professors.AddRange(profs);
            context.SaveChanges();

            var students = new Student[]
            {
                new Student{FirstName="Alina", LastName="Toader", Email="alina@yahoo.com", GroupId = 4}
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            var questions = new Question[]
            {
                new Question{Text = "Aceasta este o intrebare de lungime medie?", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Ce inseamna OOP?", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Care sunt principiile OOP?", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Aceasta este o intrebare de lungime medie?", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Ce inseamna OOP?", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Care sunt principiile OOP?", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Aceasta este o intrebare de lungime medie?", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Ce inseamna OOP?", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Care sunt principiile OOP?", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Aceasta este o intrebare de lungime medie?", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Ce inseamna OOP?", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Aceasta este o intrebare de lungime medie?", Status = QuestionStatus.Pending, StudentId = 1},
                new Question{Text = "Care sunt principiile OOP?", Status = QuestionStatus.Pending, StudentId = 1},
            };
            context.Questions.AddRange(questions);
            context.SaveChanges();

            var answers = new Answer[]
            {
                new Answer{Text = "corect", Comment="This is true", QuestionId = 1},
                new Answer{Text = "corect", Comment="This is also true", QuestionId = 1},
                new Answer{Text = "gresit", Comment="This is false", QuestionId = 1},
                new Answer{Text = "corect", Comment="This is true", QuestionId = 2},
                new Answer{Text = "gresit", Comment="This is also true", QuestionId = 2},
                new Answer{Text = "gresit", Comment="This is false", QuestionId = 2},
                new Answer{Text = "gresit", Comment="This is true", QuestionId = 3},
                new Answer{Text = "corect", Comment="This is also true", QuestionId = 3},
                new Answer{Text = "gresit", Comment="This is false", QuestionId = 3},
                new Answer{Text = "corect", Comment="This is true", QuestionId = 4},
                new Answer{Text = "corect", Comment="This is also true", QuestionId = 4},
                new Answer{Text = "gresit", Comment="This is false", QuestionId = 4},
            };
            context.Answers.AddRange(answers);
            context.SaveChanges();
        }
    }
}
