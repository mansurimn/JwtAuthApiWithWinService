using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SecureApiBackend.Controllers;

namespace SecureApiBackend.Tests
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        private AuthenticationController _controller;
        private Mock<IConfiguration> _configurationMock;

        [SetUp]
        public void Setup()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.SetupGet(c => c["Jwt:Key"]).Returns("YourVeryLongSecureSuperSecretKey12345");
            _configurationMock.SetupGet(c => c["Jwt:Issuer"]).Returns("YourIssuer");
            _configurationMock.SetupGet(c => c["Jwt:Audience"]).Returns("YourAudience");

            _controller = new AuthenticationController(_configurationMock.Object);
        }

        [Test]
        public void GenerateToken_ValidCredentials_ReturnsToken()
        {
            var loginRequest = new LoginRequest { Username = "testUser", Password = "testPassword" };
            var result = _controller.GenerateToken(loginRequest) as OkObjectResult;

            Assert.NotNull(result);
            Assert.IsNotEmpty(result.Value.ToString());
            StringAssert.Contains("token", result.Value.ToString());
        }

        [Test]
        public void GenerateToken_InvalidCredentials_ReturnsUnauthorized()
        {
            var loginRequest = new LoginRequest { Username = "wrongUser", Password = "wrongPassword" };
            var result = _controller.GenerateToken(loginRequest) as UnauthorizedObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(401, result.StatusCode);
        }
    }
}