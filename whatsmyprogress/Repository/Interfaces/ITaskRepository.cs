using System;
using System.Collections.Generic;
using System.Text;
using whatsmyprogress.DAL.Entities;

namespace whatsmyprogress.Repository.Interfaces
{
    public interface ITaskRepository
    {
        System.Threading.Tasks.Task Add(Task entity);
        System.Threading.Tasks.Task Delete(int id);
        System.Threading.Tasks.Task Update(Task entity);
        System.Threading.Tasks.Task<IEnumerable<Task>> List(int id);
        System.Threading.Tasks.Task<Task> Get(int id);
    }
}
