﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Models
{
    public class TestViewModel
    {
        public string Name { get; set; }
        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}
