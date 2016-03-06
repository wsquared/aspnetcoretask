using System;
using System.Collections.Generic;
using Task.Business.Entities;
using Moq;
using Task.Data.Contracts.Dapper;
using Task.Data.Contracts.Factory;
using Task.Data.Dapper;
using Xunit;

namespace Task.Data.Tests
{
    public class TaskRepositoryShould
    {
        [Fact]
        public void ReturnTaskEntitiesWhenGet()
        {
            // Arrange
            var repository = CreateRepository();

            //Act
            var result = repository.Get();

            // Assert
            Assert.IsAssignableFrom(typeof(IEnumerable<TaskEntity>), result);
        }

        [Fact]
        public void ReturnTaskEntityWhenGetByTaskId()
        {
            // Arrange
            var repository = CreateRepository();

            //Act
            var result = repository.Get(It.IsAny<Guid>());

            // Assert
            Assert.IsType<TaskEntity>(result);
        }

        [Fact]
        public void ReturnNewTaskEntityWhenCreateNewTask()
        {
            // Arrange
            var repository = CreateRepository();

            //Act
            var result = repository.Create(new TaskEntity());

            // Assert
            Assert.IsType<TaskEntity>(result);
        }

        [Fact]
        public void NotThrowExceptionWhenUpdate()
        {
            // Arrange
            var repository = CreateRepository();

            try
            {
                //Act
                repository.Update(new TaskEntity());
            }
            catch (Exception ex)
            {
                // Assert
                Assert.True(false, ex.Message);
            }
        }

        private static ITaskRepository CreateRepository()
        {
            var mock = new Mock<ISqlServerConnectionFactory>();
            // Setup database

            return new TaskRepository(mock.Object);
        }
    }
}
