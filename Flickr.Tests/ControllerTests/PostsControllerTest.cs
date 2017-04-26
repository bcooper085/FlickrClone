using FlickrClone.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FlickrClone.Models;
using Microsoft.AspNetCore.Identity;

namespace Flickr.Tests.ControllerTests
{
    public class PostsControllerTest
    {
        private readonly FlickrCloneDbContext _db;
        private readonly UserManager<User> _userManager;

        [Fact]
        public void Get_ViewResult_Index_Test()
        {
            //Arrange
            PostsController controller = new PostsController(_userManager, _db);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Get_ModelList_Index_Test()
        {
            //Arrange
            PostsController controller = new PostsController(_userManager, _db);
            IActionResult actionResult = controller.Index();
            ViewResult indexView = controller.Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsType<List<Post>>(result);
        }
    }
}
