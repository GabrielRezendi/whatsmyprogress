using CommandDotNet;
using CommandDotNet.NameCasing;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace whatsmyprogress
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static int Main(string[] args)
        {
            if (IsConnectionOk().Result)
                return new AppRunner<Main>().UseNameCasing(Case.LowerCase).Run(args);
            else
                return Constants.FAILURE_RETURN_ID;
        }

        private static async Task<bool> IsConnectionOk()
        {
            try
            {
                var CheckConnectionAddress = GetConfig()["ApiAddress"] + "/CheckConnection";
                var response = client.GetAsync($"{CheckConnectionAddress}").Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    
                    Console.WriteLine(Constants.FAILURE_TO_CONNECT_TO_API);
                    Console.WriteLine(responseContent);

                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.FAILURE_TO_CONNECT_TO_API);
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static IConfiguration GetConfig()
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return Configuration;
        }
    }
}
