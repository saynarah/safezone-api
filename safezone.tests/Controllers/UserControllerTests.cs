using Microsoft.AspNetCore.Mvc;
using Moq;
using safezone.application.DTOs.Login;
using safezone.application.DTOs.User;
using safezone.application.Interfaces;
using safezone.Controllers;
using safezone.domain.Entities;

namespace safezone.tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _jwtServiceMock = new Mock<IJwtService>();
            _authServiceMock = new Mock<IAuthService>();
            _controller = new UserController(_userRepositoryMock.Object, _jwtServiceMock.Object, _authServiceMock.Object);
        }

        [Fact]
        public async Task GetById_UserExists_ReturnsUser()
        {
            var user = new User { Id = 1, Email = "test@test.com" };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync(user);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public async Task GetById_UserNotFound_ReturnsNotFound()
        {
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync((User)null);

            var result = await _controller.GetById(1);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ValidUser_ReturnsCreatedAtAction()
        {
            var dto = new UserDTO { Email = "test@test.com", Password = "123" };
            _authServiceMock.Setup(auth => auth.HashPassword(dto.Password)).Returns("hashed");

            var result = await _controller.Create(dto);

            var createdAtAction = Assert.IsType<CreatedAtActionResult>(result);
            var user = Assert.IsType<User>(createdAtAction.Value);
            Assert.Equal(dto.Email, user.Email);
        }

        [Fact]
        public async Task Update_UserExists_ReturnsNoContent()
        {
            var existingUser = new User { Id = 1, Email = "old@test.com", Password = "123" };
            var dto = new UserDTO { Email = "new@test.com", Password = "456" };

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync(existingUser);
            _authServiceMock.Setup(auth => auth.HashPassword(dto.Password)).Returns("hashed");

            var result = await _controller.Update(1, dto);

            Assert.IsType<NoContentResult>(result);
            Assert.Equal("new@test.com", existingUser.Email);
            Assert.Equal("hashed", existingUser.Password);
        }

        [Fact]
        public async Task Update_UserNotFound_ReturnsNotFound()
        {
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync((User)null);
            var dto = new UserDTO();

            var result = await _controller.Update(1, dto);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_UserExists_ReturnsNoContent()
        {
            var user = new User { Id = 1 };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync(user);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_UserNotFound_ReturnsNotFound()
        {
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync((User)null);

            var result = await _controller.Delete(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task ValidateUser_ValidCredentials_ReturnsOkTrue()
        {
            _userRepositoryMock.Setup(repo => repo.ValidateUserAsync("test@test.com", "123")).ReturnsAsync(true);

            var result = await _controller.ValidateUser("test@test.com", "123");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task ValidateUser_MissingEmailOrPassword_ReturnsBadRequest()
        {
            var result = await _controller.ValidateUser("", "");

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            var dto = new LoginDTO { Email = "test@test.com", Password = "123" };
            var user = new User { Email = dto.Email, Password = "hashed" };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(dto.Email)).ReturnsAsync(user);
            _authServiceMock.Setup(auth => auth.VerifyPassword(dto.Password, user.Password)).Returns(true);
            _jwtServiceMock.Setup(jwt => jwt.GenerateToken(user)).Returns("fake-jwt-token");

            var result = await _controller.Login(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value;
            Assert.Equal("fake-jwt-token", response?.GetType().GetProperty("token")?.GetValue(response)?.ToString());
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            var dto = new LoginDTO { Email = "test@test.com", Password = "wrong" };
            var user = new User { Email = dto.Email, Password = "hashed" };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(dto.Email)).ReturnsAsync(user);
            _authServiceMock.Setup(auth => auth.VerifyPassword(dto.Password, user.Password)).Returns(false);

            var result = await _controller.Login(dto);

            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid credentials", unauthorized.Value);
        }
    }
}
