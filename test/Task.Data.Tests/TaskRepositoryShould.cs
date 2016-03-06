using System.Collections.Generic;
using Task.Data;
using Task.Business.Entities;
using Xunit;

namespace Task.Data.Tests
{
    public class TaskRepositoryShould
    {
        [Fact]
        public void GetTaskEntities()
        {
            // Arrange
            var repository = new TaskRepository();

            //Act
            var result = repository.Get();

            // Assert
            Assert.IsAssignableFrom(typeof(IEnumerable<TaskEntity>), result);
        }

        [Fact]
        public void CreateNewTaskEntity()
        {
            // Arrange
            var repository = new TaskRepository();

            //Act
            var result = repository.Create(new TaskEntity());

            // Assert
            Assert.IsType<TaskEntity>(result);
        }
    }
}
