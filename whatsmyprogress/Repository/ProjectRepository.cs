using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using whatsmyprogress.DAL.Entities;
using whatsmyprogress.Repository.Interfaces;

namespace whatsmyprogress.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private static readonly HttpClient client = new HttpClient();
        public IConfiguration _configuration { get; set; }
        public string ApiAddress { get; set; }

        public ProjectRepository()
        {
            _configuration = Program.GetConfig();
            ApiAddress = _configuration["ApiAddress"] + "/Projects";
        }

        public async System.Threading.Tasks.Task Add(Project entity)
        {
            var response = await client.PostAsync($"{ApiAddress}/Add", RepositoryHelper.GetByteArrayContent(entity));
            
            TreatHttpResponse(response.StatusCode);
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var response = await client.DeleteAsync($"{ApiAddress}/Delete/{id}");
            
            TreatHttpResponse(response.StatusCode);
        }

        public async System.Threading.Tasks.Task<IEnumerable<Project>> Get()
        {
            var response = await client.GetAsync($"{ApiAddress}/Get/");

            TreatHttpResponse(response.StatusCode);

            return JsonConvert.DeserializeObject<List<Project>>(response.Content.ReadAsStringAsync().Result);
        }

        public async System.Threading.Tasks.Task<Project> Get(int id)
        {
            var response = await client.GetAsync($"{ApiAddress}/Get/{id}");

            TreatHttpResponse(response.StatusCode);

            return JsonConvert.DeserializeObject<Project>(response.Content.ReadAsStringAsync().Result);
        }

        public async System.Threading.Tasks.Task Update(Project entity)
        {
            var response = await client.PostAsync($"{ApiAddress}/Update", RepositoryHelper.GetByteArrayContent(entity));

            TreatHttpResponse(response.StatusCode);
        }

        void TreatHttpResponse(HttpStatusCode statusCode) {
            if (statusCode != System.Net.HttpStatusCode.OK)
                throw new HttpRequestException();
        }
     
    }
}
