using System;
using System.Collections.Generic;
using Task.Data;
using Task.Business.Entities;
using Moq;
using Xunit;

namespace Task.Data.Tests
{
    public class TaskRepositoryShould
    {
        [Fact]
        public void ReturnTaskEntitiesWhenGet()
        {
            // Arrange
            var repository = new TaskRepository();

            //Act
            var result = repository.Get();

            // Assert
            Assert.IsAssignableFrom(typeof(IEnumerable<TaskEntity>), result);
        }

        [Fact]
        public void ReturnTaskEntityWhenGetByTaskId()
        {
            // Arrange
            var repository = new TaskRepository();

            //Act
            var result = repository.Get(It.IsAny<Guid>());

            // Assert
            Assert.IsType<TaskEntity>(result);
        }

        [Fact]
        public void ReturnNewTaskEntityWhenCreateNewTask()
        {
            // Arrange
            var repository = new TaskRepository();

            //Act
            var result = repository.Create(new TaskEntity());

            // Assert
            Assert.IsType<TaskEntity>(result);
        }

        [Fact]
        public void NotThrowExceptionWhenUpdate()
        {
            // Arrange
            var repository = new TaskRepository();

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

    }
}
