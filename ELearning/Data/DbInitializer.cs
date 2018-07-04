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
                new Professor{FirstName="Ana", LastName="Popescu", Email="ana@yahoo.com", Password = "AQAAAAEAACcQAAAAEKE6wezecQR/8l2qalbnEEMVN32swwfcgPMAJ+FShyKF3SSQvzYnjtT9pvYi/d0g4w=="}
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
                new Question{Text = "Care sunt principiile OOP?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 1, Answers = new List<Answer>(){
                    new Answer{Text = "Abstractizare, incapsulare, polimorfism si mostenire", Correct = true},
                    new Answer{Text = "Abstractizare si incapsulare", Correct = false},
                    new Answer{Text = "Abstractizare, incapsulare si mostenire", Correct = false}
                    }
                },
                new Question{Text = "Ce inseamna OOP?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 1, Answers = new List<Answer>(){
                    new Answer{Text = "Object Oriented Programming", Correct = true},
                    new Answer{Text = "Raspuns Gresit", Correct = false},
                    new Answer{Text = "Raspuns Gresit", Correct = false},
                    }
                },
                new Question{Text = "Ce metoda putem folosi pentru rezolvarea sistemelor neliniare?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 2, Answers = new List<Answer>(){
                    new Answer{Text = "Newton", Correct = true},
                    new Answer{Text = "Hermite", Correct = false},
                    new Answer{Text = "Spline", Correct = false},
                    new Answer{Text = "Toate", Correct = false},
                    }
                },
                new Question{Text = "Cat de grea este materia \"Calcul numeric\"?", Status = QuestionStatus.Rejected, Difficulty = QuestionDifficulty.Hard, StudentId = 1, AssignmentId = 2, Comment= "Te rog sa te gandesti la intrebari mai serioase!", Answers = new List<Answer>(){
                    new Answer{Text = "Corect", Correct = true },
                    new Answer{Text = "Corect", Correct = true},
                    new Answer{Text = "Fals", Correct = false},
                    }
                },
                new Question{Text = "Ce este testarea?", Status = QuestionStatus.Accepted, Difficulty = QuestionDifficulty.Easy, StudentId = 1, AssignmentId = 3, Answers = new List<Answer>(){
                    new Answer{Text = "O investigație empirică realizată cu scopul de a oferi părților interesate informații referitoare la calitatea produsului sau serviciului supus testării", Correct=true},
                    new Answer{Text = "Raspuns gresit", Correct = false},
                    new Answer{Text = "Raspuns gresit", Correct = false},
                    },
                    QuestionConcepts = new List<QuestionConcept>()
                    {
                        new QuestionConcept(){ ConceptId = 3}
                    }
                },

                new Question{Text = "Ce este incapsularea?", Status = QuestionStatus.Accepted, Difficulty = QuestionDifficulty.Medium, StudentId = 1, AssignmentId = 1,
                    Answers = new List<Answer>()
                    {
                        new Answer{Text = "Un feature care restrictioneaza accesul la membrii unui obiect", Correct = true},
                        new Answer{Text = "Reuniunea datelor si a metodelor unui obiect", Correct = true},
                        new Answer{Text = "Posibilitatea de a rescrie o functie", Correct = false},
                    },
                    QuestionConcepts = new List<QuestionConcept>()
                    {
                        new QuestionConcept(){ ConceptId = 1}
                    }
                },
                new Question{Text = "Ce este polimorfismul?", Status = QuestionStatus.Accepted, Difficulty = QuestionDifficulty.Medium, StudentId = 1, AssignmentId = 1, Answers = new List<Answer>(){
                    new Answer{Text = "Capacitatea unui obiect de a lua forme diferite", Correct = true},
                    new Answer{Text = "Reuniunea datelor si a metodelor unui obiect", Correct = false},
                    new Answer{Text = "Posibilitatea de a implementa o functie", Correct = false},
                    },
                    QuestionConcepts = new List<QuestionConcept>()
                    {
                        new QuestionConcept(){ ConceptId = 1}
                    }
                },
                new Question{Text = "Ce este mostenirea?", Status = QuestionStatus.Accepted, Difficulty = QuestionDifficulty.Medium, StudentId = 1, AssignmentId = 1, Answers = new List<Answer>(){
                    new Answer{Text = "Posibilitatea unei clase de a copia o alta clasa fara a rescrie codul", Correct = true},
                    new Answer{Text = "Reuniunea datelor si a metodelor unui obiect", Correct = false},
                    new Answer{Text = "Posibilitatea de a implementa o functie", Correct = false},
                    },
                    QuestionConcepts = new List<QuestionConcept>()
                    {
                        new QuestionConcept(){ ConceptId = 1}
                    }
                },

                new Question{Text = "Ce inseamna white-box testing?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 3, Answers = new List<Answer>(){
                    new Answer{Text = "Testarea specificatiilor", Correct = false},
                    new Answer{Text = "Testarea codului", Correct = false },
                    new Answer{Text = "Tehnica de creare a cazurilor de test in functie de codul scris", Correct = true},
                    new Answer{Text = "Tehnica de testare software care se bazeaza in totalitate pe cerintele si specificatiile problemei", Correct = false},
                    }
                },
                new Question{Text = "Ce inseamna black-box testing?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 3, Answers = new List<Answer>(){
                    new Answer{Text = "Tehnica de testare software care se bazeaza in totalitate pe cerintele si specificatiile problemei", Correct = true},
                    new Answer{Text = "Tehnica de creare a cazurilor de test in functie de codul scris", Correct = false},
                    }
                },
                new Question{Text = "De ce este nevoie de testare?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 3, Answers = new List<Answer>(){
                    new Answer{Text = "Satisfacerea cerintelor clientului este cheia reusitei", Correct = true},
                    new Answer{Text = "Obtinerea increderii clientului prin oferirea unui soft bun, de calitate", Correct = true},
                    new Answer{Text = "Calitatea softului este indicata prin testarea acestuia", Correct = true},
                    }
                },

                new Question{Text = "Care este cea mai importanta caracteristica a unui agent inteligent?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 4, Answers = new List<Answer>(){
                    new Answer{Text = "Flexibilitatea", Correct = false},
                    new Answer{Text = "Autonomia", Correct = true},
                    new Answer{Text = "Reactivitatea", Correct = false},
                    }
                },
                new Question{Text = "Cum se defineste flexibilitatea unui agent inteligent?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 4, Answers = new List<Answer>(){
                     new Answer{Text = "Reactivitate", Correct = true},
                    new Answer{Text = "Proactivitate", Correct = true},
                    new Answer{Text = "Abilitate sociala", Correct = true},
                    }
                },
                new Question{Text = "Ce este un agent inteligent?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 4, Answers = new List<Answer>(){
                    new Answer{Text = "O entitate autonoma, care percepe mediul inconjurator si actioneaza asupra lui", Correct = true},
                    new Answer{Text = "O entitate care este capabila sa perceapa mediul inconjurator", Correct = false},
                    }
                },

                new Question{Text = "Ce este un sistem de operare?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 6, Answers = new List<Answer>(){
                    new Answer{Text = "un ansamblu de programe care are rolul de a gestiona și de a facilita utilizatorului accesul la resursele sistemului de calcul", Correct = true},
                    new Answer{Text = "Windows", Correct = false},
                    new Answer{Text = "Linux", Correct = false},
                    }
                },
                new Question{Text = "Ce face comanda fork?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 6, Answers = new List<Answer>(){
                    new Answer{Text = "Creeaza procese copil", Correct = true},
                    new Answer{Text = "Creeaza un director", Correct = false},
                    new Answer{Text = "Creeaza un fisier", Correct = false},
                    }
                },
                new Question{Text = "Cand folosim comanda mkdir?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 6, Answers = new List<Answer>(){
                    new Answer{Text = "Creeaza un director", Correct = true},
                    new Answer{Text = "Creeaza un fisier", Correct = false},
                    new Answer{Text = "Creeaza procese copil", Correct = false},
                    }
                },

                new Question{Text = "Ce intelegem prin programare paralela?", Status = QuestionStatus.Pending, StudentId = 1, AssignmentId = 5, Answers = new List<Answer>(){
                    new Answer{Text = "Arta de a programa o colecție de calculatoare pentru a executa eficient o singură aplicație", Correct = true},
                    new Answer{Text = "Programare intr-un grup de persoane", Correct = false},
                    }
                },

            };
            context.Questions.AddRange(questions);
            context.SaveChanges();
            

        }
    }
}
