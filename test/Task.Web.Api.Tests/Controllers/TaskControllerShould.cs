using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Moq;
using Task.Controllers;
using Task.Client.Entities;
using Task.Business.Entities;
using Task.Data.Contracts;
using Xunit;

namespace TaskWeb.Api.Tests.Controllers
{
    public class TaskControllerShould
    {
        private readonly TaskEntity _taskEntity = new TaskEntity
        {
            TaskId = Guid.NewGuid(),
            Title = "Clean car",
            Details = "Subaru Wrx",
            DueDate = DateTime.UtcNow.Date,
            CompletedDate = null
        };

        [Fact]
        public void GetTaskViewModels()
        {
            // Arrange
            var mock = new Mock<ITaskRepository>();
            mock.Setup(x => x.Get()).Returns(new List<TaskEntity>{_taskEntity});
            var controller = new TaskController(mock.Object);

            //Act
            var response = (HttpOkObjectResult)controller.Get();

            // Assert
            Assert.IsAssignableFrom(typeof(IEnumerable<TaskViewModel>), response.Value);
        }

        [Fact]
        public void ReturnTaskViewModelWhenCreateNewTask()
        {
            // Arrange
            var mock = new Mock<ITaskRepository>();
            mock.Setup(x => x.Create(It.IsAny<TaskEntity>())).Returns(_taskEntity);
            var controller = new TaskController(mock.Object);

            //Act
            var response = (HttpOkObjectResult)controller.Post(new TaskViewModel());

            // Assert
            Assert.IsType<TaskViewModel>(response.Value);
        }

        [Fact]
        public void ReturnCreatedStatusCodeCreateNewTask()
        {
            // Arrange
            var mock = new Mock<ITaskRepository>();
            mock.Setup(x => x.Create(It.IsAny<TaskEntity>())).Returns(_taskEntity);
            var controller = new TaskController(mock.Object);

            //Act
            var response = (HttpOkObjectResult)controller.Post(new TaskViewModel());

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        }

        [Fact]
        public void ReturnNoContentWhenUpdateTask()
        {
            // Arrange
            var mock = new Mock<ITaskRepository>();
            mock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(_taskEntity);
            var controller = new TaskController(mock.Object);

            //Act
            var response = (HttpStatusCodeResult)controller.Put(Guid.NewGuid(), new TaskViewModel());

            // Assert
            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
        }
    }
}
