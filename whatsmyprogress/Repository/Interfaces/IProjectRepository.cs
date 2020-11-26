using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using whatsmyprogress.DAL.Entities;

namespace whatsmyprogress.Repository.Interfaces
{
    public interface IProjectRepository
    {
        void Add(Project entity);
        void Delete(int id);
        void Update(Project entity);
        Task<IEnumerable<Project>> Get();
        Task<Project> Get(int id);
    }
}
