using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using whatsmyprogress.DAL.DAO;
using whatsmyprogress.DAL.Entities;

namespace whatsmyprogress.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        public IDAO<Project> _projectDao { get; set; }
        public ProjectsController(IDAO<Project> _projectDao)
        {
            this._projectDao = _projectDao;
        }

        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return _projectDao.Get();
        }

        [HttpGet("{id}")]
        public Project Get(int id)
        {
            return _projectDao.Get(id);
        }

        [HttpPost]
        public void Add([FromBody] Project project)
        {
            _projectDao.Add(project);
        }

        [HttpPost]
        public void Update([FromBody] Project project)
        {
            _projectDao.Update(project);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _projectDao.Delete(id);
        }
    }
}
