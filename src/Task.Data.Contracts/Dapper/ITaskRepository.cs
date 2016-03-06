using System;
using System.Collections.Generic;
using Task.Business.Entities;

namespace Task.Data.Contracts.Dapper
{
    public interface ITaskRepository
    {
        IEnumerable<TaskEntity> Get();

        IEnumerable<TaskEntity> GetCompleted();

        TaskEntity Get(Guid taskId);

        TaskEntity Create(TaskEntity taskEntity);

        void Update(TaskEntity taskEntity);
    }
}
