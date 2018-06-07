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
                new Professor{FirstName="Camelia", LastName="Serban", Email="camelia.serban@yahoo.com", Password = "AQAAAAEAACcQAAAAEKE6wezecQR/8l2qalbnEEMVN32swwfcgPMAJ+FShyKF3SSQvzYnjtT9pvYi/d0g4w=="}
            };
            context.Professors.AddRange(profs);
            context.SaveChanges();

            var students = new Student[]
            {
                new Student{FirstName="Alina", LastName="Toader", Email="alina@yahoo.com", GroupId = 4, Password = "AQAAAAEAACcQAAAAELtaLh6mZI08S+06F9/VidEPieGmAk/AVqOphDpzAJXfVvrEQxhe42SsCXkcTc5LqQ=="}
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            var concepts = new Concept[]
            {
                new Concept{Name = "OOP"},
                new Concept{Name = "Calcul numeric"},
                new Concept{Name = "Testare software"},
                new Concept{Name = "Inteligenta artificiala"},
                new Concept{Name = "Programare paralela"},
                new Concept{Name = "Sisteme de operare"},
            };
            context.Concepts.AddRange(concepts);
            context.SaveChanges();

            var assignments = new Assignment[]
{
                new Assignment{  Deadline = new DateTime(2018,5,23), GroupId = 4 , ProfessorId = 1, ConceptId = 1},
                new Assignment{  Deadline = new DateTime(2018,7,23), GroupId = 4 , ProfessorId = 1, ConceptId = 2},
                new Assignment{  Deadline = new DateTime(2018,5,25), GroupId = 4 , ProfessorId = 1, ConceptId = 3},
                new Assignment{ Deadline = new DateTime(2018,6,3), GroupId = 4 , ProfessorId = 1, ConceptId = 4},
                new Assignment{ Deadline = new DateTime(2018,6,3), GroupId = 4 , ProfessorId = 1, ConceptId = 5},
                new Assignment{ Deadline = new DateTime(2018,6,3), GroupId = 4 , ProfessorId = 1, ConceptId = 6},

};
            context.Assignments.AddRange(assignments);
            context.SaveChanges();

            var questions = new Question[]
            {
                new Question{Text = "Care sunt principiile OOP?", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Medium, StudentId = 1, AssignmentId = 1},
                new Question{Text = "Ce inseamna OOP?", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Easy, StudentId = 1, AssignmentId = 1},
                new Question{Text = "Ce metoda putem folosi pentru rezolvarea sistemelor neliniare?", Status = QuestionStatus.Pending, Difficulty = QuestionDifficulty.Hard, StudentId = 1, AssignmentId = 2},
                new Question{Text = "Cat de grea este materia \"Calcul numeric\"?", Status = QuestionStatus.Rejected, Difficulty = QuestionDifficulty.Hard, StudentId = 1, AssignmentId = 2, Comment= "Te rog sa te gandesti la intrebari mai serioase!"},
                new Question{Text = "Ce este testarea?", Status = QuestionStatus.Accepted, Difficulty = QuestionDifficulty.Easy, StudentId = 1, AssignmentId = 3},

                new Question{Text = "Ce este incapsularea?", Status = QuestionStatus.Accepted, Difficulty = QuestionDifficulty.Easy, StudentId = 1, AssignmentId = 1},
                new Question{Text = "Ce este polimorfismul?", Status = QuestionStatus.Accepted, Difficulty = QuestionDifficulty.Medium, StudentId = 1, AssignmentId = 1},
                new Question{Text = "Ce este mostenirea?", Status = QuestionStatus.Accepted, Difficulty = QuestionDifficulty.Hard, StudentId = 1, AssignmentId = 1},
            };
            context.Questions.AddRange(questions);
            context.SaveChanges();

            var answers = new Answer[]
            {
                new Answer{Text = "Object Oriented Programming", Correct = true, QuestionId = 2},
                new Answer{Text = "Raspuns Gresit", Correct = false, QuestionId = 2},
                new Answer{Text = "Raspuns Gresit", Correct = false, QuestionId = 2},
                new Answer{Text = "Abstractizare, incapsulare, polimorfism si mostenire", Correct = true, QuestionId = 1},
                new Answer{Text = "Abstractizare si incapsulare", Correct = false, QuestionId = 1},
                new Answer{Text = "Abstractizare, incapsulare si mostenire", Correct = false, QuestionId = 1},
                new Answer{Text = "Newton", Correct = true, QuestionId = 3},
                new Answer{Text = "Hermite", Correct = false, QuestionId = 3},
                new Answer{Text = "Spline", Correct = false, QuestionId = 3},
                new Answer{Text = "Toate", Correct = false, QuestionId = 3},
                new Answer{Text = "Corect", Correct = true, QuestionId = 4 },
                new Answer{Text = "Corect", Correct = true, QuestionId = 4},
                new Answer{Text = "Fals", Correct = false, QuestionId = 4},
                new Answer{Text = "O investigație empirică realizată cu scopul de a oferi părților interesate informații referitoare la calitatea produsului sau serviciului supus testării", Correct=true, QuestionId=5},
                new Answer{Text = "Raspuns gresit", Correct = false, QuestionId = 5},
                new Answer{Text = "Raspuns gresit", Correct = false, QuestionId = 5},
                new Answer{Text = "Un feature care restrictioneaza accesul la membrii unui obiect", Correct = true, QuestionId = 6},
                new Answer{Text = "Reuniunea datelor si a metodelor unui obiect", Correct = true, QuestionId = 6},
                new Answer{Text = "Posibilitatea de a rescrie o functie", Correct = false, QuestionId = 6},
                new Answer{Text = "Capacitatea unui obiect de a lua forme diferite", Correct = true, QuestionId = 7},
                new Answer{Text = "Reuniunea datelor si a metodelor unui obiect", Correct = false, QuestionId = 7},
                new Answer{Text = "Posibilitatea de a implementa o functie", Correct = false, QuestionId = 7},
                new Answer{Text = "Posibilitatea unei clase de a copia o alta clasa fara a rescrie codul", Correct = true, QuestionId = 8},
                new Answer{Text = "Reuniunea datelor si a metodelor unui obiect", Correct = false, QuestionId = 8},
                new Answer{Text = "Posibilitatea de a implementa o functie", Correct = false, QuestionId = 8},

            };
            context.Answers.AddRange(answers);
            context.SaveChanges();

            var questionConcepts = new QuestionConcept[]
            {
                new QuestionConcept(){QuestionId=4, ConceptId=2},
                new QuestionConcept(){QuestionId=5, ConceptId=3},
                new QuestionConcept(){QuestionId=6, ConceptId=1},
                new QuestionConcept(){QuestionId=7, ConceptId=1},
                new QuestionConcept(){QuestionId=8, ConceptId=1},

            };
            context.QuestionConcepts.AddRange(questionConcepts);
            context.SaveChanges();

        }
    }
}
