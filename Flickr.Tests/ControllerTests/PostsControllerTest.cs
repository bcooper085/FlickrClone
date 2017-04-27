using FlickrClone.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FlickrClone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Flickr.Tests.ControllerTests
{
    public class PostsControllerTest
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        [Fact]
        public void Get_ViewResult_Index_Test()
        {
            var contextOptions = new DbContextOptionsBuilder()
        
            .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FlickrClone;integrated security=True;")
            .Options;
        
            var _db = new FlickrCloneDbContext(contextOptions);
        //Arrange
        PostsController controller = new PostsController(userManager, _db);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Get_ModelList_Index_Test()
        {
            var contextOptions = new DbContextOptionsBuilder()

            .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FlickrClone;integrated security=True;")
            .Options;

            var _db = new FlickrCloneDbContext(contextOptions);
            //Arrange
            PostsController controller = new PostsController(userManager, _db);
            IActionResult actionResult = controller.Index();
            ViewResult indexView = controller.Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsType<List<Post>>(result);
        }
        [Fact]
        public void Post_MethodCreatesPost_Test()
        {
            var contextOptions = new DbContextOptionsBuilder()

            .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FlickrClone;integrated security=True;")
            .Options;

            var _db = new FlickrCloneDbContext(contextOptions);
            //Arrange
            AccountController controller = new AccountController(userManager, signInManager, _db);
            Post testPost = new Post();
            testPost.Title = "test post Title";
            testPost.Description = "test post";
            testPost.Image = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcQSY52npATDDuQkFYsCa2rVJ4TP-rAXi6GTJ_X43P-kqugEdLNy6g";

            //Act
            controller.Create(testPost);
            ViewResult indexView = new AccountController(userManager, signInManager, _db).Index() as ViewResult;
            var collection = indexView.ViewData.Model as IEnumerable<Post>;

            //Assert
            Assert.Contains<Post>(testPost, collection);
        }
    }
}
