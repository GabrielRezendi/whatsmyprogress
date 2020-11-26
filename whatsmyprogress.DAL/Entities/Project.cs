using System;
using System.Collections.Generic;
using System.Text;
using whatsmyprogress.DAL.Enums;

namespace whatsmyprogress.DAL.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
