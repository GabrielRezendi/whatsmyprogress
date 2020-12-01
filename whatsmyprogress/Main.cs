using CommandDotNet;
using configurationwriter;
using ShellProgressBar;
using System;
using System.Linq;
using whatsmyprogress.Commands;
using whatsmyprogress.Repository;
using whatsmyprogress.Repository.Interfaces;
using Console = EzConsole.EzConsole;

namespace whatsmyprogress
{
    public class Main
    {
        [SubCommand]
        public Projects Projects { get; set; }
        [SubCommand]
        public Tasks Tasks { get; set; }

        [Command(Description = "Set specific project as default")]
        public void SetDefaultProject(int projectid)
        {
            ConfigurationWriter configurationWriter = new ConfigurationWriter();
            configurationWriter.AddOrUpdateAppSetting<int>(Constants.DEFAULT_PROJECTID, projectid);

            Console.WriteLine("Default Project was successfuly set.", ConsoleColor.Green);
        }

        [Command(Description = "Show progress of the default project")]
        public void ShowProgress()
        {
            ITaskRepository _taskRepository = new TaskRepository();

            var taskList = _taskRepository.List(Helper.GetDefaultProjectId()).Result;

            int amountOfTasks = taskList.Count();
            int amountOfConqueredTasks = taskList.Where(x => x.TaskStatus == DAL.Enums.TaskStatus.Conquered).Count();

            var options = new ProgressBarOptions
            {
                ForegroundColor = ConsoleColor.Blue,
                ForegroundColorDone = ConsoleColor.DarkGreen,
                BackgroundColor = ConsoleColor.DarkGray,
                ShowEstimatedDuration = false
            };

            using (var pbar = new ProgressBar(amountOfTasks, "conquered", options))
            {
                pbar.Tick(amountOfConqueredTasks);
            }

        }

        [Command(Description = "Show current default project")]
        public void ShowDefaultProject()
        {
            IProjectRepository _projectRepository = new ProjectRepository();

            var currentDefaultProject = _projectRepository.Get(Helper.GetDefaultProjectId()).Result;

            Console.WriteLine($"{currentDefaultProject.Id} - {currentDefaultProject.Title} is the current default project.", ConsoleColor.Blue);
        }

    }
}
