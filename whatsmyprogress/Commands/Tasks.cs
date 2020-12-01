using CommandDotNet;
using System;
using System.Linq;
using whatsmyprogress.DAL.Entities;
using whatsmyprogress.Repository;
using whatsmyprogress.Repository.Interfaces;
using Console = EzConsole.EzConsole;

namespace whatsmyprogress.Commands
{
    public class Tasks
    {
        ITaskRepository _taskRepository;
        public Tasks()
        {
            this._taskRepository = new TaskRepository();
        }

        [DefaultMethod]
        [Command(Description = "List all tasks from the current default project")]
        public void tasksDefaultCommand()
        {
            var requestResult = _taskRepository.List(Helper.GetDefaultProjectId()).Result;

            if (requestResult != null)
            {
                foreach (var task in requestResult.ToList())
                {
                    Console.Write($"{task.Id} - {task.Description} | ");
                    switch (task.TaskStatus)
                    {
                        case DAL.Enums.TaskStatus.Planned:
                            Console.WriteLine($"{task.TaskStatus}", ConsoleColor.Blue);
                            break;
                        case DAL.Enums.TaskStatus.Fighting:
                            Console.WriteLine($"{task.TaskStatus}", ConsoleColor.Yellow);
                            break;
                        case DAL.Enums.TaskStatus.Conquered:
                            Console.WriteLine($"{task.TaskStatus}", ConsoleColor.Green);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
                Console.WriteLine("There are no tasks yet. Use 'add' and start working!");
        }


        [Command(Description = "Adds a new task to the current default project")]
        public void Add(string description)
        {
            try
            {
                _taskRepository.Add(new Task { Description = description, Id_Project = Helper.GetDefaultProjectId(), TaskStatus = DAL.Enums.TaskStatus.Planned }).Wait();
                Console.WriteLine($"{description} was added to the list of tasks!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, the system failed to add to the list of tasks", ConsoleColor.Red);
                Console.WriteLine(ex.Message);
            }
        }

        [Command(Description = "Rename an specific task")]
        public void Rename(int id, string newDescription)
        {
            try
            {
                var specificTask = _taskRepository.Get(id).Result;
                var oldDescription = specificTask.Description;

                specificTask.Description = newDescription;

                _taskRepository.Update(specificTask).Wait();

                Console.WriteLine($"{oldDescription} is now kwon as {newDescription}!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, the system failed to add to the list of tasks", ConsoleColor.Red);
                Console.WriteLine(ex.Message);
            }
        }

        [Command(Description = "Conquers a specific task!")]
        public void Conquer(int id)
        {
            try
            {
                var specificTask = _taskRepository.Get(id).Result;
                specificTask.TaskStatus = DAL.Enums.TaskStatus.Conquered;
                _taskRepository.Update(specificTask).Wait();
                Console.WriteLine($"{specificTask.Description} is now conquered!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, the system failed to add to the list of projects", ConsoleColor.Red);
                Console.WriteLine(ex.Message);
            }
        }

        [Command(Description = "Start fighting to conquer a specific task!")]
        public void Fight(int id)
        {
            try
            {
                var specificTask = _taskRepository.Get(id).Result;
                specificTask.TaskStatus = DAL.Enums.TaskStatus.Fighting;
                _taskRepository.Update(specificTask).Wait();
                Console.WriteLine($"{specificTask.Description} is now being fought!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, the system failed to update the task", ConsoleColor.Red);
                Console.WriteLine(ex.Message);
            }
        }

        [Command(Description = "Deletes a task")]
        public void Delete(int id)
        {
            try
            {
                _taskRepository.Delete(id).Wait();
                Console.WriteLine($"The task was successfuly deleted", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, the system failed to delete from the list of projects", ConsoleColor.Green);
                Console.WriteLine(ex.Message);
            }
        }


    }
}
