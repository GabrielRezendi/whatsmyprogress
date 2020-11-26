using CommandDotNet;
using System;
using whatsmyprogress.Commands;

namespace whatsmyprogress
{
    public class Main
    {
        [SubCommand]
        public Projects Projects { get; set; }

        [Command(Description = "Set project as default")]
        public void SetDefaultProject() { 
        }

        [Command(Description = "List tasks from a project")]
        public void Tasks(int projectid) {
            if (projectid == 0)
            {
                Console.WriteLine("Use the command 'setdefaultproject' or inform a specific project id");
            }
        }
    }
}
