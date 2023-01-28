using BlogWebApi.Controllers;
using BlogWebApi.DataAccess;
using BlogWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace BlogWebApi.Tests
{
    public class PostsControllerTests
    {
        [Fact]
        public async Task Post_ShouldReturnOk_WhenSendValidInput()
        {
            var now = DateTime.Now;
            Random rnd = new Random();
            var i = rnd.Next(100);
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = $"Post {i}",
                Category = $"Category {i}",
                Author = $"Author {i}",
                Content = $"Content {i}",
                CoverImage = "",
                Slug = $"post-{i}",
                CreateDate = now,
                UpdatedDate = now,
            };
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(x => x.CreateAsync(post)).Returns(() => Task.FromResult(true));
            var sut = new PostsController(mockRepository.Object);
            var response = await sut.PostPost(post);
            Assert.IsType<OkObjectResult>(response);
            mockRepository.Verify(x => x.CreateAsync(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenSendInvalidInput()
        {
            Post inputData = null;
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(x => x.CreateAsync(inputData)).Returns(() => Task.FromResult(false));
            var sut = new PostsController(mockRepository.Object);
            var response = await sut.PostPost(inputData);
            Assert.IsType<BadRequestResult>(response);
            mockRepository.Verify(x => x.CreateAsync(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public async Task Get_ShouldReturnOk_WhenCalledWithNoParams()
        {
            IEnumerable<Post> expectedData = new List<Post>();
            for (int k = 0; k < 2; ++k)
            {
                var now = DateTime.Now;
                Random rnd = new Random();
                var i = rnd.Next(100);
                var post = new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = $"Post {i}",
                    Category = $"Category {i}",
                    Author = $"Author {i}",
                    Content = $"Content {i}",
                    CoverImage = "",
                    Slug = $"post-{i}",
                    CreateDate = now,
                    UpdatedDate = now,
                };
                expectedData.Append(post);
            }
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(x => x.GetAllAsync()).Returns(() => Task.FromResult(expectedData));
            var sut = new PostsController(mockRepository.Object);
            var response = await sut.GetPosts();
            Assert.IsType<OkObjectResult>(response.Result);
            OkObjectResult result = response.Result as OkObjectResult;
            Assert.IsType<List<Post>>(result.Value);
            IEnumerable<Post> actualData = result.Value as List<Post>;
            Assert.Equal(expectedData.Count(), actualData.Count());
            Assert.Equal(expectedData, actualData);
            mockRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Get_ShouldReturnOkWithOnePost_WhenGuidExists()
        {
            var now = DateTime.Now;
            Random rnd = new Random();
            var i = rnd.Next(100);
            var expectedData = new Post()
            {
                Id = Guid.NewGuid(),
                Title = $"Post {i}",
                Category = $"Category {i}",
                Author = $"Author {i}",
                Content = $"Content {i}",
                CoverImage = "",
                Slug = $"post-{i}",
                CreateDate = now,
                UpdatedDate = now,
            };
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(x => x.GetAsync(expectedData.Id)).Returns(() => Task.FromResult(expectedData));
            var sut = new PostsController(mockRepository.Object);
            var response = await sut.GetPost(expectedData.Id);
            Assert.IsType<OkObjectResult>(response);
            OkObjectResult result = response as OkObjectResult;
            Assert.IsType<Post>(result.Value);
            Post actualData = result.Value as Post;
            Assert.Equal(expectedData, actualData);
            mockRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }


        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenCalledWithInvalidGuid()
        {
            Post expectedData = null;
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(() => Task.FromResult(expectedData));
            var sut = new PostsController(mockRepository.Object);
            var response = await sut.GetPost(Guid.NewGuid());
            Assert.IsType<NotFoundResult>(response);
            mockRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }


        [Fact]
        public async Task Put_ShouldReturnOk_WhenCalledWithValidData()
        {
            var now = DateTime.Now;
            Random rnd = new Random();
            var i = rnd.Next(100);
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = $"Post {i}",
                Category = $"Category {i}",
                Author = $"Author {i}",
                Content = $"Content {i}",
                CoverImage = "",
                Slug = $"post-{i}",
                CreateDate = now,
                UpdatedDate = now,
            };
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(x => x.UpdateAsync(post)).Returns(() => Task.FromResult(true));
            var sut = new PostsController(mockRepository.Object);
            var response = await sut.PutPost(post);
            Assert.IsType<OkObjectResult>(response);
            mockRepository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public async Task Put_ShouldReturnBadRequest_WhenCalledWithMissingData()
        {
            Post inputData = null;
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(x => x.CreateAsync(inputData)).Returns(() => Task.FromResult(false));
            var sut = new PostsController(mockRepository.Object);
            var response = await sut.PostPost(inputData);
            Assert.IsType<BadRequestResult>(response);
            mockRepository.Verify(x => x.CreateAsync(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenCalledWithValidData()
        {
            Guid inputData = Guid.NewGuid();
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(x => x.DeleteAsync(inputData)).Returns(() => Task.FromResult(true));
            var sut = new PostsController(mockRepository.Object);
            var response = await sut.DeletePost(inputData);
            Assert.IsType<OkResult>(response);
            mockRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenCalledWithInvalidId()
        {
            Guid inputData = Guid.NewGuid();
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(x => x.DeleteAsync(inputData)).Returns(() => Task.FromResult(false));
            var sut = new PostsController(mockRepository.Object);
            var response = await sut.DeletePost(inputData);
            Assert.IsType<NotFoundResult>(response);
            mockRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

    }
}
