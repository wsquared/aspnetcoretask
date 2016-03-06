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
using Task.Common;
using Xunit;

namespace TaskWeb.Api.Tests.Controllers
{
    public class TaskControllerShould
    {
        [Fact]
        public void GetTaskViewModels()
        {
            // Arrange
            var mock = new Mock<ITaskRepository>();
            mock.Setup(x => x.Get()).Returns(new List<TaskEntity>());
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
            mock.Setup(x => x.Create(It.IsAny<TaskEntity>())).Returns(It.IsAny<TaskEntity>());
            var controller = new TaskController(mock.Object);

            //Act
            var response = (CreatedResult)controller.Post(new TaskViewModel());

            // Assert
            Assert.IsType<TaskViewModel>(response?.Value);
        }

        [Fact]
        public void ReturnCreatedStatusCodeCreateNewTask()
        {
            // Arrange
            var mock = new Mock<ITaskRepository>();
            mock.Setup(x => x.Create(It.IsAny<TaskEntity>())).Returns(It.IsAny<TaskEntity>());
            var controller = new TaskController(mock.Object);

            //Act
            var response = (CreatedResult)controller.Post(new TaskViewModel());

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response?.StatusCode);
        }

        [Fact]
        public void ReturnNoContentWhenUpdateTask()
        {
            // Arrange
            var mock = new Mock<ITaskRepository>();
            mock.Setup(x => x.Update(It.IsAny<TaskEntity>()));
            var controller = new TaskController(mock.Object);

            //Act
            var response = (NoContentResult)controller.Put(Guid.NewGuid(), new TaskViewModel());

            // Assert
            Assert.Equal(StatusCodes.Status204NoContent, response?.StatusCode);
        }
    }
}
