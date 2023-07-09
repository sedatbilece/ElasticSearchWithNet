﻿using ElasticSearchWithNet.API.DTOs;
using ElasticSearchWithNet.API.Repositories;

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
    }
}
