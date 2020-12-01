using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using whatsmyprogress.DAL.Entities;
using whatsmyprogress.Repository.Interfaces;

namespace whatsmyprogress.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private static readonly HttpClient client = new HttpClient();
        public IConfiguration _configuration { get; set; }
        public string ApiAddress { get; set; }

        public TaskRepository()
        {
            _configuration = Program.GetConfig();
            ApiAddress = _configuration["ApiAddress"] + "/Tasks";
        }

        public async System.Threading.Tasks.Task Add(Task entity)
        {
            var response = await client.PostAsync($"{ApiAddress}/Add", RepositoryHelper.GetByteArrayContent(entity));

            TreatHttpResponse(response.StatusCode);
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var response = await client.DeleteAsync($"{ApiAddress}/Delete/{id}");

            TreatHttpResponse(response.StatusCode);
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> List(int projectId)
        {
            var response = await client.GetAsync($"{ApiAddress}/GetByProjectId/{projectId}");

            TreatHttpResponse(response.StatusCode);

            return JsonConvert.DeserializeObject<List<Task>>(response.Content.ReadAsStringAsync().Result);
        }

        public async System.Threading.Tasks.Task<Task> Get(int id)
        {
            var response = await client.GetAsync($"{ApiAddress}/Get/{id}");

            TreatHttpResponse(response.StatusCode);

            return JsonConvert.DeserializeObject<Task>(response.Content.ReadAsStringAsync().Result);
        }

        public async System.Threading.Tasks.Task Update(Task entity)
        {
            var response = await client.PostAsync($"{ApiAddress}/Update", RepositoryHelper.GetByteArrayContent(entity));

            TreatHttpResponse(response.StatusCode);
        }

        void TreatHttpResponse(HttpStatusCode statusCode)
        {
            if (statusCode != System.Net.HttpStatusCode.OK)
                throw new HttpRequestException();
        }
    }
}
