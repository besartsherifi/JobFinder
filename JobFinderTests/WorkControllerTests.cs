using JwtAuthAspNet7WebAPI.Controllers;
using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;
using JwtAuthAspNet7WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace JwtAuthAspNet7WebAPI.Tests
{
    public class WorkControllerTests
    {
        [Fact]
        public async Task CreateWork_WithValidDto_ReturnsOkResult()
        {
            var workServiceMock = new Mock<IWorkService>();
            var workCreateDto = new WorkCreateDto
            {
                Title = "Test Work",
                Description = "Test Description",
                Address = "Test Address",
                IsActive = true,
                DemanderId = "1"
            };
            var createdWork = new Work
            {
                Id = 1,
                Title = "Test Work",
                Description = "Test Description",
                Address = "Test Address",
                IsActive = true,
                DemanderId = "1"
            };
            workServiceMock.Setup(s => s.CreateWork(workCreateDto)).ReturnsAsync(createdWork);
            var controller = new WorkController(workServiceMock.Object);

            var result = await controller.CreateWork(workCreateDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultWork = Assert.IsType<Work>(okResult.Value);
            Assert.Equal(createdWork.Id, resultWork.Id);
            Assert.Equal(createdWork.Title, resultWork.Title);

            workServiceMock.Verify(s => s.CreateWork(workCreateDto), Times.Once);
        }

        [Fact]
        public async Task GetWorkById_WithValidId_ReturnsWork()
        {
            var workServiceMock = new Mock<IWorkService>();
            var workId = 1;
            var work = new Work
            {
                Id = workId,
                Title = "Test Work",
                Description = "Test Description",
                Address = "Test Address",
                IsActive = true,
                DemanderId = "1"
            };
            workServiceMock.Setup(s => s.GetWorkById(workId)).ReturnsAsync(work);
            var controller = new WorkController(workServiceMock.Object);
            var result = await controller.GetWorkById(workId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultWork = Assert.IsType<Work>(okResult.Value);
            Assert.Equal(work.Id, resultWork.Id);
            Assert.Equal(work.Title, resultWork.Title);

            workServiceMock.Verify(s => s.GetWorkById(workId), Times.Once);
        }

        [Fact]
        public async Task GetWorkById_WithInvalidId_ReturnsNotFoundResult()
        {
            var workServiceMock = new Mock<IWorkService>();
            var invalidWorkId = 999;
            workServiceMock.Setup(s => s.GetWorkById(invalidWorkId)).ReturnsAsync((Work?)null);
            var controller = new WorkController(workServiceMock.Object);

            var result = await controller.GetWorkById(invalidWorkId);

            Assert.IsType<NotFoundResult>(result);

            workServiceMock.Verify(s => s.GetWorkById(invalidWorkId), Times.Once);
        }

        [Fact]
        public async Task GetWorks_ReturnsListOfWorks()
        {
            var workServiceMock = new Mock<IWorkService>();
            var works = new List<Work>
            {
                new Work
                {
                    Id = 1,
                    Title = "Work 1",
                    Description = "Description 1",
                    Address = "Address 1",
                    IsActive = true,
                    DemanderId = "1"
                },
                new Work
                {
                    Id = 2,
                    Title = "Work 2",
                    Description = "Description 2",
                    Address = "Address 2",
                    IsActive = false,
                    DemanderId = "2"
                }
            };
            workServiceMock.Setup(s => s.GetWorks()).ReturnsAsync(works);
            var controller = new WorkController(workServiceMock.Object);

            var result = await controller.GetWorks();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultWorks = Assert.IsType<List<Work>>(okResult.Value);
            Assert.Equal(works.Count, resultWorks.Count);

            workServiceMock.Verify(s => s.GetWorks(), Times.Once);
        }

        [Fact]
        public async Task GetWorksByDemanderId_ReturnsWorksForAuthenticatedUser()
        {
            var workServiceMock = new Mock<IWorkService>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var userId = "1";
            var userClaims = new List<Claim>
            {
                new Claim("Id", userId)
            };
            var works = new List<Work>
            {
                new Work
                {
                    Id = 1,
                    Title = "Work 1",
                    Description = "Description 1",
                    Address = "Address 1",
                    IsActive = true,
                    DemanderId = userId
                },
                new Work
                {
                    Id = 2,
                    Title = "Work 2",
                    Description = "Description 2",
                    Address = "Address 2",
                    IsActive = false,
                    DemanderId = userId
                }
            };
            workServiceMock.Setup(s => s.GetWorksByDemanderId()).ReturnsAsync(works);
            var controller = new WorkController(workServiceMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(userClaims))
                    }
                }
            };

            var result = await controller.GetWorksByDemanderId();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultWorks = Assert.IsType<List<Work>>(okResult.Value);
            Assert.Equal(works.Count, resultWorks.Count);

            workServiceMock.Verify(s => s.GetWorksByDemanderId(), Times.Once);
        }
    }
}
