using System;
using System.Collections.Generic;
using System.Text;

namespace whatsmyprogress.DAL.DAO
{
    public interface IDAO<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Update(T entity);
        IEnumerable<T> Get();
        T Get(int id);
    }
}
