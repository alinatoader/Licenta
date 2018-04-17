using ELearning.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Data
{
    public class ELearningContext : DbContext
    {
        public ELearningContext(DbContextOptions<ELearningContext> options) : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Test> Tests { get; set; }
        //public DbSet<TestQuestion> TestQuestions { get; set; }
    }
}
