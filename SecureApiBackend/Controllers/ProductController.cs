using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SecureApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("info")]
        [Authorize]
        public IActionResult GetProductInfo()
        {
            var productInfo = new
            {
                ProductId = 1,
                Name = "Sample Product",
                Price = 29.99
            };

            return Ok(productInfo);
        }
    }
}
