using ElasticSearchWithNet.API.DTOs;
using ElasticSearchWithNet.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchWithNet.API.Controllers
{
  
    public class ProductsController : BaseController
    {

        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto productCreateDto)
        {
            return CreateActionResult(await _productService.SaveAsync(productCreateDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _productService.GetAllAsync());
        }
    }
}
