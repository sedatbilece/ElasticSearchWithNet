using ElasticSearchWithNet.API.DTOs;
using ElasticSearchWithNet.API.Models;
using ElasticSearchWithNet.API.Repositories;
using Nest;
using System.Collections.Immutable;
using System.Drawing;

namespace ElasticSearchWithNet.API.Services
{
    public class ProductService
    {

        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {

            var response =await  _productRepository.SaveAsync(request.CreateProduct());

            if (response == null)
            {
                return ResponseDto<ProductDto>.Fail(new List<string> 
                { "kayıt esnasında hata oluştu" } ,System.Net.HttpStatusCode.InternalServerError);
            }

            return ResponseDto<ProductDto>.Success(response.CreateDto(),System.Net.HttpStatusCode.Created);

        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var prd = await _productRepository.GetAllAsync();

            var prdDto =  prd.Select(x => new ProductDto(
                x.Id,
                 x.Name,
                x.Price,
                x.Stock,
                new ProductFeatureDto(x.Feature.Width,x.Feature.Height,x.Feature.Color))).ToList();

            return ResponseDto<List<ProductDto>>.Success(prdDto,System.Net.HttpStatusCode.OK);
        }


        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            var hasProduct = await _productRepository.GetByIdAsync(id);

            if (hasProduct == null)
                return ResponseDto<ProductDto>.Fail("ürün bulunamadı", System.Net.HttpStatusCode.NotFound);

            return ResponseDto<ProductDto>.Success(hasProduct.CreateDto(),System.Net.HttpStatusCode.OK);
        }


        public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {


            var isSuccess = await _productRepository.UpdateAsync(productUpdateDto);

            if(!isSuccess)
                return ResponseDto<bool>.Fail("ürün güncellenemedi", System.Net.HttpStatusCode.NotFound);

            return ResponseDto<bool>.Success(true, System.Net.HttpStatusCode.NoContent);
        }



    }
}
