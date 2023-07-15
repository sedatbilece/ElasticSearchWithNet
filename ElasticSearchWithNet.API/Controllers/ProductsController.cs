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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id) { 
        
            return CreateActionResult(await _productService.GetByIdAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ProductUpdateDto updateDto)
        {

            return CreateActionResult(await _productService.UpdateAsync(updateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {

            return CreateActionResult(await _productService.DeleteAsync(id));
        }


    }
}
