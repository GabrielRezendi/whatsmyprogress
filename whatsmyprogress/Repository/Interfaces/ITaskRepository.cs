using System;
using System.Collections.Generic;
using System.Text;
using whatsmyprogress.DAL.Entities;

namespace whatsmyprogress.Repository.Interfaces
{
    public interface ITaskRepository
    {
        void Add(Task entity);
        void Delete(int id);
        void Update(Task entity);
        System.Threading.Tasks.Task<IEnumerable<Task>> Get();
        System.Threading.Tasks.Task<Task> Get(int id);
    }
}
