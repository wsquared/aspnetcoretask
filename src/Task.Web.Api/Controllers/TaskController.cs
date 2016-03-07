using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Task.Business.Entities;
using Task.Client.Entities;
using Task.Core;
using Task.Common;
using Task.Data.Contracts.Dapper;

namespace Task.Controllers
{
    [Route("api/v1/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: api/v1/task
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

                var taskViewModels = CreateTaskViewModels(tasks);

                return Ok(taskViewModels.ToList());
            });
        }

        // GET: api/v1/task/completed
        [HttpGet("completed")]
        public IActionResult GetCompleted()
        {
            return GetHttpResponse(() =>
            {
                var tasks = _taskRepository.GetCompleted();

                if (tasks == null)
                {
                    return Ok();
                }

                var taskViewModels = CreateTaskViewModels(tasks);

                return Ok(taskViewModels.ToList());
            });
        }

        // POST api/v1/task
        [HttpPost]
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        public IActionResult Post([FromBody] TaskViewModel taskViewModel)
        {
            return GetHttpResponse(() =>
            {
                var result = _taskRepository.Create(CreateTaskEntity(taskViewModel));

                var newTaskViewModel = CreateTaskViewModel(result);

                return Ok(newTaskViewModel);
            });
        }

        // PUT api/v1/task/5
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

                // Bit dirty, but just ensure taskId and completedDate cannot be set through
                // this call for now
                taskViewModel.Id = task.TaskId;
                taskViewModel.CompletedDate = task.CompletedDate;

                var taskEntity = CreateTaskEntity(taskViewModel);

                _taskRepository.Update(taskEntity);

                return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
            });
        }

        // PUT api/v1/task/5/complete
        [HttpPut("{id}/complete")]
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        public IActionResult UpdateToCompleted(Guid id, [FromBody] string value)
        {
            return GetHttpResponse(() =>
            {
                var task = _taskRepository.Get(id);

                if (task == null)
                {
                    throw new TaskNotFoundException(id.ToString());
                }

                if (task.CompletedDate.HasValue)
                {
                    throw new TaskCompletedException(id.ToString());
                }

                _taskRepository.UpdateToCompleted(id);

                return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
            });
        }

        private static IEnumerable<TaskViewModel> CreateTaskViewModels(IEnumerable<TaskEntity> taskEntities)
        {
            return taskEntities.Select(CreateTaskViewModel);
        }

        private static TaskEntity CreateTaskEntity(TaskViewModel taskViewModel)
        {
            return new TaskEntity
            {
                TaskId = taskViewModel.Id,
                Title = taskViewModel.Title,
                Details = taskViewModel.Details,
                DueDate = taskViewModel.DueDate,
                CompletedDate = taskViewModel.CompletedDate
            };
        }

        private static TaskViewModel CreateTaskViewModel(TaskEntity taskEntity)
        {
            return new TaskViewModel
            {
                Id = taskEntity.TaskId,
                Title = taskEntity.Title,
                Details = taskEntity.Details,
                DueDate = taskEntity.DueDate,
                CompletedDate = taskEntity.CompletedDate
            };
        }
    }
}
