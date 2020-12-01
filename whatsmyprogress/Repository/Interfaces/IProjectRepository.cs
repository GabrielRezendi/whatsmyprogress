using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using whatsmyprogress.DAL.Entities;

namespace whatsmyprogress.Repository.Interfaces
{
    public interface IProjectRepository
    {
        System.Threading.Tasks.Task Add(Project entity);
        System.Threading.Tasks.Task Delete(int id);
        System.Threading.Tasks.Task Update(Project entity);
        System.Threading.Tasks.Task<IEnumerable<Project>> Get();
        System.Threading.Tasks.Task<Project> Get(int id);
    }
}
