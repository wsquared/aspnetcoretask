using System;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Extensions;
using Microsoft.AspNet.Mvc;
using Task.Business.Entities;
using Task.Client.Entities;
using Task.Data.Contracts;
using Task.Core;
using Task.Common;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
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
            return GetHttpResponse(() =>
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
            });
        }

        // POST api/task
        [HttpPost]
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        public IActionResult Post([FromBody] TaskViewModel taskViewModel)
        {
            return GetHttpResponse(() =>
            {
                var result = _taskRepository.Create(new TaskEntity
                {
                    TaskId = taskViewModel.TaskId,
                    Title = taskViewModel.Title,
                    Details = taskViewModel.Details,
                    DueDate = taskViewModel.DueDate,
                    CompletedDate = taskViewModel.CompletedDate
                });

                var newTaskViewModel = new TaskViewModel
                {
                    TaskId = result.TaskId,
                    Title = result.Title,
                    Details = result.Details,
                    DueDate = result.DueDate,
                    CompletedDate = result.CompletedDate
                };

                return Ok(newTaskViewModel as object);
            });
        }

        // PUT api/task/5
        [HttpPut("{id}")]
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        public IActionResult Put(Guid id, [FromBody] TaskViewModel taskViewModel)
        {
            return GetHttpResponse(() =>
            {
                var task = _taskRepository.Get(id);

                if (task == null)
                {
                    throw new TaskNotFoundException(id.ToString());
                }

                var taskEntity = new TaskEntity
                {
                    TaskId = task.TaskId,
                    Title = taskViewModel.Title,
                    Details = taskViewModel.Details,
                    DueDate = taskViewModel.DueDate,
                    CompletedDate = taskViewModel.CompletedDate
                };

                _taskRepository.Update(taskEntity);

                return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
            });
        }
    }
}
