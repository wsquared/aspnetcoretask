using System;
using System.Collections.Generic;
using Task.Business.Entities;

namespace Task.Data.Contracts
{
    public interface ITaskRepository
    {
        IEnumerable<TaskEntity> Get();

        TaskEntity Get(Guid taskId);

        TaskEntity Create(TaskEntity taskEntity);

        void Update(TaskEntity taskEntity);
    }
}
