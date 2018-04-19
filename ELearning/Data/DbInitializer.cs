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

            var concepts = new Concept[]
            {
                new Concept{Name = "OOP"},
                new Concept{Name = "Calcul numeric"},
                new Concept{Name = "Testare software"},
                new Concept{Name = "Inteligenta artificiala"},
            };
            context.Concepts.AddRange(concepts);
            context.SaveChanges();

            var assignments = new Assignment[]
{
                new Assignment{  Deadline = new DateTime(2018,5,23), GroupId = 4 , ProfessorId = 1, ConceptId = 1},
                new Assignment{  Deadline = new DateTime(2018,7,23), GroupId = 4 , ProfessorId = 1, ConceptId = 2},
                new Assignment{  Deadline = new DateTime(2018,5,25), GroupId = 4 , ProfessorId = 1, ConceptId = 3},
                new Assignment{ Deadline = new DateTime(2018,6,3), GroupId = 4 , ProfessorId = 1, ConceptId = 4},
};
            context.Assignments.AddRange(assignments);
            context.SaveChanges();

            var questions = new Question[]
            {
                new Question{Text = "Aceasta este o intrebare de lungime medie?", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Hard, StudentId = 1, AssignmentId = 1},
                new Question{Text = "Ce inseamna OOP?", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Easy, StudentId = 1, AssignmentId = 1},
                new Question{Text = "Care sunt principiile OOP?", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Easy, StudentId = 1, AssignmentId = 2},
                new Question{Text = "Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Hard, StudentId = 1, AssignmentId = 3},
                new Question{Text = "Aceasta este o intrebare de lungime medie?", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Hard, StudentId = 1, AssignmentId = 4},
                new Question{Text = "Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Easy, StudentId = 1, AssignmentId = 1},
                new Question{Text = "Ce inseamna OOP?", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Easy, StudentId = 1, AssignmentId = 2},
                new Question{Text = "Care sunt principiile OOP?", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Easy, StudentId = 1, AssignmentId = 3},
                new Question{Text = "Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Medium, StudentId = 1, AssignmentId = 4},
                new Question{Text = "Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga. Aceasta este o intrebare foarte lunga  Aceasta este o intrebare foarte lunga", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Medium, StudentId = 1, AssignmentId = 1},
            };
            context.Questions.AddRange(questions);
            context.SaveChanges();

            var answers = new Answer[]
            {
                new Answer{Text = "rasp1", Correct = true, QuestionId = 1},
                new Answer{Text = "rasp2", Correct = true, QuestionId = 1},
                new Answer{Text = "rasp3", Correct = false, QuestionId = 1},
                new Answer{Text = "rasp1", Correct = true, QuestionId = 2},
                new Answer{Text = "rasp2", Correct = false, QuestionId = 2},
                new Answer{Text = "rasp3", Correct = false, QuestionId = 2},
                new Answer{Text = "rasp1", Correct = true, QuestionId = 3},
                new Answer{Text = "rasp2", Correct = true, QuestionId = 3},
                new Answer{Text = "rasp3", Correct = false, QuestionId = 3},
                new Answer{Text = "rasp1", Correct = true, QuestionId = 4},
                new Answer{Text = "rasp2", Correct = false, QuestionId = 4},
                new Answer{Text = "rasp3", Correct = false, QuestionId = 4},
                new Answer{Text = "rasp1", Correct = false, QuestionId = 5},
                new Answer{Text = "rasp2", Correct = true, QuestionId = 5},
                new Answer{Text = "rasp3", Correct = false, QuestionId = 5},
                new Answer{Text = "rasp1", Correct = true, QuestionId = 6},
                new Answer{Text = "rasp2", Correct = false, QuestionId = 6},
                new Answer{Text = "rasp3", Correct = false, QuestionId = 6},
                new Answer{Text = "rasp1", Correct = true, QuestionId = 7},
                new Answer{Text = "rasp2", Correct = false, QuestionId = 7},
                new Answer{Text = "rasp3", Correct = false, QuestionId = 7},
                new Answer{Text = "rasp1", Correct = true, QuestionId = 8},
                new Answer{Text = "rasp2", Correct = false, QuestionId = 8},
                new Answer{Text = "rasp3", Correct = false, QuestionId = 8},
                new Answer{Text = "rasp1", Correct = true, QuestionId = 9},
                new Answer{Text = "rasp2", Correct = false, QuestionId = 9},
                new Answer{Text = "rasp3", Correct = false, QuestionId = 9},
                new Answer{Text = "rasp1", Correct = true, QuestionId = 10},
                new Answer{Text = "rasp2", Correct = false, QuestionId = 10},
                new Answer{Text = "rasp3", Correct = false, QuestionId = 10},
            };
            context.Answers.AddRange(answers);
            context.SaveChanges();


        }
    }
}
