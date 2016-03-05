using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Task.Controllers;
using Task.Models;
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
        public void CreateNewTask()
        {
            // Arrange
            var controller = new TaskController();
            
            //Act
            var response = (HttpOkObjectResult) controller.Post(new TaskViewModel());

            // Assert
            Assert.IsType<TaskViewModel>(response.Value);
        }

        [Fact]
        public void UpdateTask()
        {
            // Arrange
            var controller = new TaskController();
            
            //Act
            var response = (HttpOkObjectResult) controller.Put(Guid.NewGuid(), new TaskViewModel());

            // Assert
            Assert.IsType<TaskViewModel>(response.Value);
        }

    }
}
