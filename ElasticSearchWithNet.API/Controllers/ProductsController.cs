using ElasticSearchWithNet.API.DTOs;
using ElasticSearchWithNet.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchWithNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto productCreateDto)
        {
            return Ok(await _productService.SaveAsync(productCreateDto));
        }
    }
}
