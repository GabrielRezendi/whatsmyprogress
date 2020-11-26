using CommandDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whatsmyprogress.DAL.Entities;
using whatsmyprogress.Repository;
using whatsmyprogress.Repository.Interfaces;

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
                Console.WriteLine(ex);
            }
        }

        [Command(Description = "Adds a new project")]
        public void Add(string name) {
            try
            {
                _projectRepository.Add(new Project { Title = name });
                Console.WriteLine($"{name} was added to the list of projects!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, the system failed to add to the list of projects");
                Console.WriteLine(ex);
            }
        }

        [Command(Description = "Deletes a project")]
        public void Delete(int id)
        {
            try
            {
                _projectRepository.Delete(id);
                Console.WriteLine($"The project was successfuly deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, the system failed to delete from the list of projects");
                Console.WriteLine(ex);
            }
        }
    }
}
