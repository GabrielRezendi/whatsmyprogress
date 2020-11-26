using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public void Add(Project entity)
        {
            var response = client.PostAsync($"{ApiAddress}/Add", RepositoryHelper.GetByteArrayContent(entity)).Result;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new HttpRequestException();
        }

        public void Delete(int id)
        {
            var response = client.DeleteAsync($"{ApiAddress}/Delete/{id}").Result;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new HttpRequestException();
        }

        public async Task<IEnumerable<Project>> Get()
        {
            var response = await client.GetAsync($"{ApiAddress}/Get/");
            
            return JsonConvert.DeserializeObject<List<Project>>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<Project> Get(int id)
        {
            var response = await client.GetAsync($"{ApiAddress}/Get/{id}");
            return JsonConvert.DeserializeObject<Project>(response.Content.ReadAsStringAsync().Result);
        }

        public async void Update(Project entity)
        {
            await client.PostAsync($"{ApiAddress}/Update", RepositoryHelper.GetByteArrayContent(entity));
        }

     
    }
}
