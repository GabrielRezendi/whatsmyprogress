using CommandDotNet;
using configurationwriter;
using System;
using whatsmyprogress.Commands;
using Console = EzConsole.EzConsole;

namespace whatsmyprogress
{
    public class Main
    {
        [SubCommand]
        public Projects Projects { get; set; }
        [SubCommand]
        public Tasks Tasks { get; set; }

        [Command(Description = "Set project as default")]
        public void SetDefaultProject(int projectid)
        {
            ConfigurationWriter configurationWriter = new ConfigurationWriter();
            configurationWriter.AddOrUpdateAppSetting<int>(Constants.DEFAULT_PROJECTID, projectid);

            Console.WriteLine("Default Project was successfuly set.", ConsoleColor.Green);
        }

    }
}
}
