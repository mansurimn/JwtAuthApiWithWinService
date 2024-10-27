namespace WindowsServiceClient.Tests
{
    [TestFixture]
    public class WindowsServiceTests
    {
        private WindowsService _service;

        [SetUp]
        public void Setup()
        {
            _service = new WindowsService();
        }

        [Test]
        public async Task AuthenticateAndGetTokenAsync_ValidCredentials_ReturnsToken()
        {
            string token = await _service.AuthenticateAndGetTokenAsync("testUser", "testPassword");
            Assert.IsNotNull(token);
            Assert.IsNotEmpty(token);
        }

        [Test]
        public async Task AuthenticateAndGetTokenAsync_InvalidCredentials_ReturnsNullOrEmptyToken()
        {
            string token = await _service.AuthenticateAndGetTokenAsync("wrongUser", "wrongPassword");
            Assert.IsTrue(string.IsNullOrEmpty(token));
        }

        [Test]
        public async Task GetProductInfoAsync_ValidToken_ReturnsProductInfo()
        {
            string token = await _service.AuthenticateAndGetTokenAsync("testUser", "testPassword");
            string productInfo = await _service.GetProductInfoAsync(token);

            Assert.IsNotNull(productInfo);
            Assert.IsTrue(productInfo.Contains("ProductId"));
        }

        [Test]
        public async Task GetProductInfoAsync_InvalidToken_ReturnsForbiddenMessage()
        {
            string invalidToken = "invalidToken";
            string response = await _service.GetProductInfoAsync(invalidToken);

            Assert.AreEqual("403 Forbidden", response);
        }
    }
}