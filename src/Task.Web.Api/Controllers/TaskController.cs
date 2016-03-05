using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Task.Client.Entities;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var test1 = new TaskViewModel
            {
                TaskId = Guid.NewGuid(),
                Title = "Clean car",
                Details = "Subaru Wrx",
                DueDate = DateTime.UtcNow.Date,
                CompletedDate = null
            };

            var test2 = new TaskViewModel
            {
                TaskId = Guid.NewGuid(),
                Title = "Finish backend work",
                Details = "For leveling up ASP.NET5",
                DueDate = DateTime.UtcNow.Date,
                CompletedDate = null
            };

            var result = new List<TaskViewModel>() {
                test1,
                test2
            };

            return Ok(result);
        }

        // POST api/task
        [HttpPost]
        public IActionResult Post([FromBody] TaskViewModel value)
        {
            // TODO: Find and send current Uri
            return Created("", new TaskViewModel());
        }

        // PUT api/task/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TaskViewModel value)
        {
            return new NoContentResult();
        }
    }
}
