using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlickrClone.Models;
using Xunit;

namespace Flickr.Tests
{
    public class PostTest
    {
        [Fact]
        public void GetDescriptionTest()
        {
            //Arrange
            var post = new Post();
            post.Description = "test image";

            //Act
            var result = post.Description;

            //Assert
            Assert.Equal("test image", result);
        }
    }
}
