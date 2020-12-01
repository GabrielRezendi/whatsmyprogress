using CommandDotNet;
using System;
using System.Linq;
using whatsmyprogress.DAL.Entities;
using whatsmyprogress.Repository;
using whatsmyprogress.Repository.Interfaces;
using Console = EzConsole.EzConsole;

namespace whatsmyprogress.Commands
{
    public class Projects
    {
        IProjectRepository _projectRepository;
        public Projects()
        {
            this._projectRepository = new ProjectRepository();
        }

        [DefaultMethod]
        [Command(Description = "List all the existing projects")]
        public void projectsDefaultCommand()
        {
            try
            {
                var listOfProjects = _projectRepository.Get().Result.ToList();
                if (listOfProjects.Count > 0)
                {
                    listOfProjects.ForEach(project => Console.WriteLine($"{project.Id} - {project.Title}"));
                }
                else
                {
                    Console.WriteLine("There are no projects yet!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, the system failed to recover the list of projects");
                Console.WriteLine(ex.Message);
            }
        }

        [Command(Description = "Adds a new project")]
        public void Add(string title) {
            try
            {
                _projectRepository.Add(new Project { Title = title }).Wait();
                Console.WriteLine($"{title} was added to the list of projects!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, the system failed to add to the list of projects");
                Console.WriteLine(ex.Message);
            }
        }

        [Command(Description = "Renames a specific project")]
        public void Rename(int id, string newTitle)
        {
            try
            {
                var specificProject = _projectRepository.Get(id).Result;
                var oldTitle = specificProject.Title;

                specificProject.Title = newTitle;

                _projectRepository.Update(specificProject).Wait();

                Console.WriteLine($"{oldTitle} is now kwon as {newTitle}!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, the system failed to delete from the list of projects");
                Console.WriteLine(ex.Message);
            }
        }

        [Command(Description = "Deletes a specific project")]
        public void Delete(int id)
        {
            try
            {
                _projectRepository.Delete(id).Wait();
                Console.WriteLine($"The project was successfuly deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, the system failed to delete from the list of projects");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
