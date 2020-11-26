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
            var buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(entity));
            var byteContent = new ByteArrayContent(buffer);

            await client.PostAsync($"{ApiAddress}/Add", byteContent);
        }

        public async void Delete(int id)
        {
            await client.DeleteAsync($"{ApiAddress}/Delete/{id}");
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> Get()
        {
            var response = await client.GetAsync($"{ApiAddress}/Get/");
            return JsonConvert.DeserializeObject(response.ToString()) as List<Task>;
        }

        public async System.Threading.Tasks.Task<Task> Get(int id)
        {
            var response = await client.GetAsync($"{ApiAddress}/Get/{id}");
            return JsonConvert.DeserializeObject(response.ToString()) as Task;
        }

        public async void Update(Task entity)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(entity));
            var byteContent = new ByteArrayContent(buffer);

            await client.PostAsync($"{ApiAddress}/Update", byteContent);
        }
    }
}
