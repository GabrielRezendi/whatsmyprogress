using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using whatsmyprogress.DAL.DAO;
using whatsmyprogress.DAL.Entities;

namespace whatsmyprogress.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        public IDAO<Task> _tasksDao { get; set; }
        public TasksController(IDAO<Task> _tasksDao)
        {
            this._tasksDao = _tasksDao;
        }

        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return _tasksDao.Get();
        }

        [HttpGet("{id}")]
        public Task Get(int id)
        {
            return _tasksDao.Get(id);
        }

        [HttpGet("{id}")]
        public IEnumerable<Task> GetByProjectId(int id)
        {
            return _tasksDao.Get().Where(task => task.Id_Project == id);
        }

        [HttpPost]
        public void Add([FromBody] Task task)
        {
            _tasksDao.Add(task);
        }

        [HttpPost]
        public void Update([FromBody] Task task)
        {
            _tasksDao.Update(task);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _tasksDao.Delete(id);
        }
    }
}
