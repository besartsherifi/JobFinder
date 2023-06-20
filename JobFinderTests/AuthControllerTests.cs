using JwtAuthAspNet7WebAPI.Controllers;
using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace JobFinderTests
{
    public class AuthControllerTests
    {
        private readonly AuthController _authController;
        private readonly Mock<IAuthService> _authServiceMock;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task RegisterDemander_ValidRegisterDto_ReturnsOkResult()
        {
            var registerDto = new RegisterDto();

            var expectedResponse = new AuthServiceResponseDto
            {
                IsSucceed = true,
                Message = "User Created Successfully"
            };

            _authServiceMock.Setup(service => service.RegisterDemanderAsync(registerDto))
                .ReturnsAsync(expectedResponse);

            var result = await _authController.RegisterDemander(registerDto);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<AuthServiceResponseDto>(okResult.Value);

            Assert.True(response.IsSucceed);
            Assert.Equal(expectedResponse.Message, response.Message);
        }

        [Fact]
        public async Task Login_ValidLoginDto_ReturnsOkResult()
        {
            var loginDto = new LoginDto();

            var expectedResponse = new AuthServiceResponseDto
            {
                IsSucceed = true,
                Message = "Authentication successful"
            };

            _authServiceMock.Setup(service => service.LoginAsync(loginDto))
                .ReturnsAsync(expectedResponse);

            var result = await _authController.Login(loginDto);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<AuthServiceResponseDto>(okResult.Value);

            Assert.True(response.IsSucceed);
            Assert.Equal(expectedResponse.Message, response.Message);
        }
    }
}
