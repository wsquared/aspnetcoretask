using System;
using System.Collections.Generic;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Task.Controllers;
using Task.Client.Entities;
using Xunit;

namespace TaskWeb.Api.Tests.Controllers
{
    public class TaskControllerShould
    {
        [Fact]
        public void GetTaskViewModels()
        {
            // Arrange
            var controller = new TaskController();
            
            //Act
            var response = (HttpOkObjectResult) controller.Get();

            // Assert
            Assert.IsAssignableFrom(typeof (IEnumerable<TaskViewModel>), response.Value);
        }

        [Fact]
        public void ReturnTaskViewModelWhenCreateNewTask()
        {
            // Arrange
            var controller = new TaskController();
            
            //Act
            var response = (CreatedResult) controller.Post(new TaskViewModel());

            // Assert
            Assert.IsType<TaskViewModel>(response?.Value);
        }

        [Fact]
        public void ReturnCreatedStatusCodeCreateNewTask()
        {
            // Arrange
            var controller = new TaskController();

            //Act
            var response = (CreatedResult)controller.Post(new TaskViewModel());

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response?.StatusCode);
        }

        [Fact]
        public void ReturnNoContentWhenUpdateTask()
        {
            // Arrange
            var controller = new TaskController();
            
            //Act
            var response = (NoContentResult) controller.Put(Guid.NewGuid(), new TaskViewModel());

            // Assert
            Assert.Equal(StatusCodes.Status204NoContent, response?.StatusCode);
        }

    }
}
