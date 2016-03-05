using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http.Extensions;
using Microsoft.AspNet.Mvc;
using Task.Business.Entities;
using Task.Client.Entities;
using Task.Data.Contracts;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var tasks = _taskRepository.Get();

            if (tasks == null)
            {
                return Ok();
            }

            var taskViewModels = tasks.Select(x => new TaskViewModel
            {
                TaskId = x.TaskId,
                Title = x.Title,
                Details = x.Details,
                DueDate = x.DueDate,
                CompletedDate = x.CompletedDate
            });

            return Ok(taskViewModels.ToList());
        }

        // POST api/task
        [HttpPost]
        public IActionResult Post([FromBody] TaskViewModel taskViewModel)
        {
            var result = _taskRepository.Create(new TaskEntity
            {
                TaskId = taskViewModel.TaskId,
                Title = taskViewModel.Title,
                Details = taskViewModel.Details,
                DueDate = taskViewModel.DueDate,
                CompletedDate = taskViewModel.CompletedDate
            });

            return Created(Request.GetDisplayUrl(), result);
        }

        // PUT api/task/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TaskViewModel taskViewModel)
        {
            return new NoContentResult();
        }
    }
}
