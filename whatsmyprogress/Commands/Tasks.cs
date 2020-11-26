using CommandDotNet;
using System;
using System.Linq;
using whatsmyprogress.DAL.Entities;
using whatsmyprogress.Repository;
using whatsmyprogress.Repository.Interfaces;

namespace whatsmyprogress.Commands
{
    public class Tasks
    {
        ITaskRepository  _taskRepository;
        public Tasks()
        {
            this._taskRepository = new TaskRepository();
        }

        [DefaultMethod]
        [Command(Description = "List all tasks from a specific project (no need to inform id of project if already used 'setdefaultproject')")]
        public void tasksDefaultCommand([Option]int projectid)
        {
            if (projectid == 0 && Helper.GetDefaultProjectId() == 0)
            {
                Console.WriteLine("Use the command 'setdefaultproject' or inform a specific project id");
                return;
            }
            else
            {

            }
        }
       
    }
}
