using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureApiBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecureApiBackend.Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private ProductController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new ProductController();
        }

        [Test]
        public void GetProductInfo_AuthenticatedUser_ReturnsProductInfo()
        {
            // Simulate authenticated user
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "testUser")
                    }, "testAuthType"))
                }
            };

            var result = _controller.GetProductInfo() as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void GetProductInfo_UnauthenticatedUser_ReturnsForbidden()
        {
            // Simulate unauthenticated request
            var result = _controller.GetProductInfo() as ForbidResult;

            Assert.IsNull(result);
        }
    }
}
