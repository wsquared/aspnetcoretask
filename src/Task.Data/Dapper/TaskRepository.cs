using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Task.Business.Entities;
using Task.Data.Contracts.Dapper;
using Task.Data.Contracts.Factory;

namespace Task.Data.Dapper
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ISqlServerConnectionFactory _sqlServerConnectionFactory;

        // TODO: Temporary storage - look at setting up configuration in the .net core 'way'
        private const string ConnectionString =
            @"Data Source=(localdb)\v11.0;Initial Catalog=Task;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public TaskRepository(ISqlServerConnectionFactory sqlServerConnectionFactory)
        {
            _sqlServerConnectionFactory = sqlServerConnectionFactory;
        }

        public IEnumerable<TaskEntity> Get()
        {
            using (var connection = _sqlServerConnectionFactory.Create(ConnectionString))
            {
                connection.Open();

                var result = connection.Query<TaskEntity>
                (@"
	                SELECT *
	                FROM [Task].[dbo].[Task]
	                WHERE CompletedDate is null
	                ORDER BY DueDate ASC
                ");

                return result;
            }
        }

        public IEnumerable<TaskEntity> GetCompleted()
        {
            using (var connection = _sqlServerConnectionFactory.Create(ConnectionString))
            {
                connection.Open();

                var result = connection.Query<TaskEntity>
                (@"
	                SELECT *
	                FROM [Task].[dbo].[Task]
	                WHERE CompletedDate is not null
	                ORDER BY CompletedDate DESC
                ");

                return result;
            }
        }

        public TaskEntity Get(Guid taskId)
        {
            using (var connection = _sqlServerConnectionFactory.Create(ConnectionString))
            {
                connection.Open();

                var result = connection.Query<TaskEntity>(
                @"
                    SELECT *
                    FROM [Task].[dbo].[Task]
                    WHERE TaskId = @TaskId
                ", 
                new { TaskId = taskId })
                .SingleOrDefault();

                return result;
            }
        }

        public TaskEntity Create(TaskEntity taskEntity)
        {
            using (var connection = _sqlServerConnectionFactory.Create(ConnectionString))
            {
                connection.Open();

                var result = connection.Query<TaskEntity>(
                @"
                    SET @TaskId = NEWID()

                    INSERT INTO [Task].[dbo].[Task]
                    (TaskId, Title, Details, DueDate, CompletedDate)
                    VALUES
                    (@TaskId, @Title, @Details, @DueDate, @CompletedDate)
                    SELECT *
                    FROM [Task].[dbo].[Task]
                    WHERE TaskId = @TaskId
                ", 
                new
                {
                    Title = taskEntity.Title,
                    Details = taskEntity.Details,
                    DueDate = taskEntity.DueDate,
                    CompletedDate = taskEntity.CompletedDate
                }).Single();

                return result;
            }
        }

        public void Update(TaskEntity taskEntity)
        {
            using (var connection = _sqlServerConnectionFactory.Create(ConnectionString))
            {
                connection.Open();

                connection.Query<TaskEntity>
                (@"
                    UPDATE [Task].[dbo].[Task]
                    SET Title = @Title, Details = @Details, DueDate = @DueDate, CompletedDate = @CompletedDate
                    WHERE TaskId = @TaskId

                ", taskEntity);
            }
        }
    }
}
