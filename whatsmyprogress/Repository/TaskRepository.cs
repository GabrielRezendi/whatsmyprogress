using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
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
            ApiAddress = _configuration["ApiAddress"] + "/Task";
        }

        public async void Add(Task entity)
        {
            await client.PostAsync($"{ApiAddress}/Add", RepositoryHelper.GetByteArrayContent(entity));
        }

        public async void Delete(int id)
        {
            await client.DeleteAsync($"{ApiAddress}/Delete/{id}");
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> Get()
        {
            var response = await client.GetAsync($"{ApiAddress}/Get/");
            return JsonConvert.DeserializeObject<List<Task>>(response.Content.ReadAsStringAsync().Result);
        }

        public async System.Threading.Tasks.Task<Task> Get(int id)
        {
            var response = await client.GetAsync($"{ApiAddress}/Get/{id}");
            return JsonConvert.DeserializeObject<Task>(response.Content.ReadAsStringAsync().Result);
        }

        public async void Update(Task entity)
        {
            await client.PostAsync($"{ApiAddress}/Update", RepositoryHelper.GetByteArrayContent(entity));
        }
    }
}
