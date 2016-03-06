using System;
using Task.Data.Contracts;
using Task.Business.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Task.Data
{
    public class TaskRepository : ITaskRepository
    {
        //private static IDbConnection _db = new SqlConnection("");

        public TaskRepository()
        {
        }

        public IEnumerable<TaskEntity> Get()
        {
            yield return new TaskEntity
            {
                TaskId = Guid.NewGuid(),
                Title = "Clean car",
                Details = "Subaru Wrx",
                DueDate = DateTime.UtcNow.Date,
                CompletedDate = null
            };

            yield return new TaskEntity
            {
                TaskId = Guid.NewGuid(),
                Title = "Finish backend work",
                Details = "For leveling up ASP.NET5",
                DueDate = DateTime.UtcNow.Date,
                CompletedDate = null
            };
        }

        public TaskEntity Get(Guid taskId)
        {
            return new TaskEntity
            {
                TaskId = Guid.NewGuid(),
                Title = "Finish security implementation",
                Details = "For leveling up security skills",
                DueDate = DateTime.UtcNow.Date,
                CompletedDate = null
            };
        }

        public TaskEntity Create(TaskEntity taskEntity)
        {
            return new TaskEntity
            {
                TaskId = Guid.NewGuid(),
                Title = "Finish cloud integration",
                Details = "Aws or Azure",
                DueDate = DateTime.UtcNow.Date,
                CompletedDate = null
            };
        }

        public void Update(TaskEntity taskEntity)
        {
        }
    }
}
