using System;
using System.Collections.Generic;
using System.Text;
using whatsmyprogress.DAL.Enums;

namespace whatsmyprogress.DAL.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public int Id_Project { get; set; }
    }
}
