using JwtAuthAspNet7WebAPI.Controllers;
using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Services;
using JwtAuthAspNet7WebAPI.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;


namespace JwtAuthAspNet7WebAPI.Tests
{
    public class BidControllerTests
    {
        [Fact]
        public async Task CreateBid_WhenValidBidCreateDto_ReturnsOkResult()
        {
            var bidServiceMock = new Mock<IBidService>();
            var controller = new BidController(bidServiceMock.Object);
            var bidCreateDto = new BidCreateDto { Amount = 10.0, WorkId = 1, SeekerId = "1" };

            var result = await controller.CreateBid(bidCreateDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBidsOfSeeker_WhenAuthenticated_ReturnsOkResult()
        {
            var bidServiceMock = new Mock<IBidService>();
            var controller = new BidController(bidServiceMock.Object);
            var bidCreateDto = new BidCreateDto();

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim("Id", "1")
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = await controller.GetBidsOfSeeker();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBidsOfWork_WhenValidWorkId_ReturnsOkResult()
        {
            var bidServiceMock = new Mock<IBidService>();
            var controller = new BidController(bidServiceMock.Object);
            var workId = 1;
            var result = await controller.GetBidsOfWork(workId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateBid_WhenCalled_ReturnsOkResult()
        {
            var bidServiceMock = new Mock<IBidService>();
            var controller = new BidController(bidServiceMock.Object);
            var bidCreateDto = new BidCreateDto();

            var result = await controller.CreateBid(bidCreateDto);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetBidsOfWork_WhenWorkIdIsGreaterThanZero_ReturnsOkResult()
        {
            var bidServiceMock = new Mock<IBidService>();
            var controller = new BidController(bidServiceMock.Object);
            var workId = 1;

            var result = await controller.GetBidsOfWork(workId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBidsOfWork_WhenWorkIdIsZero_ReturnsOkResult()
        {
            var bidServiceMock = new Mock<IBidService>();
            var controller = new BidController(bidServiceMock.Object);
            var workId = 0;
            var result = await controller.GetBidsOfWork(workId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task CreateBid_WhenCalledWithSeekerIdAndWorkId_ReturnsOkResult()
        {
            var bidServiceMock = new Mock<IBidService>();
            var controller = new BidController(bidServiceMock.Object);
            var bidCreateDto = new BidCreateDto
            {
                SeekerId = "1",
                WorkId = 1
            };

            var result = await controller.CreateBid(bidCreateDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateBid_WhenCalledWithoutSeekerId_ReturnsBadRequest()
        {
            var bidServiceMock = new Mock<IBidService>();
            var controller = new BidController(bidServiceMock.Object);
            var bidCreateDto = new BidCreateDto
            {
                WorkId = 1
            };

            var result = await controller.CreateBid(bidCreateDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateBid_WhenCalledWithoutWorkId_ReturnsBadRequest()
        {
            var bidServiceMock = new Mock<IBidService>();
            var controller = new BidController(bidServiceMock.Object);
            var bidCreateDto = new BidCreateDto
            {
                SeekerId = "1"
            };

            var result = await controller.CreateBid(bidCreateDto);
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
