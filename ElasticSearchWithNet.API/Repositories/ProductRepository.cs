﻿using ElasticSearchWithNet.API.DTOs;
using ElasticSearchWithNet.API.Models;
using Nest;
using System.Collections.Immutable;

namespace ElasticSearchWithNet.API.Repositories
{
    public class ProductRepository
    {

        private readonly ElasticClient _client;
        private const string indexName = "products";
        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }


        public async Task<Product> SaveAsync(Product newProduct)
        {
            newProduct.Created= DateTime.Now;

            var response = await _client.IndexAsync(newProduct, x => x.Index(indexName).Id(Guid.NewGuid().ToString()));
            
            //fast fail
            if (!response.IsValid) return null;


            newProduct.Id=response.Id;
            return newProduct;

        }


        public async Task<ImmutableList<Product>> GetAllAsync()
        {
            var result = await _client.SearchAsync<Product>
                (s => s.Index(indexName).Query(q=>q.MatchAll()) );

            foreach(var item in result.Hits)
            {
                item.Source.Id=item.Id;
            }

            return result.Documents.ToImmutableList();
        }


        public async Task<Product> GetByIdAsync(string id)
        {
            var response = await _client.GetAsync<Product>(id, x => x.Index(indexName));

            if (!response.IsValid)
                return null;

            response.Source.Id= response.Id;

            return response.Source;
        }


        public async Task<bool> UpdateAsync(ProductUpdateDto updateDto)
        {
            var response = await _client.UpdateAsync<Product, ProductUpdateDto>(updateDto.Id, 
                x => x.Index(indexName).Doc(updateDto));

            return response.IsValid;
        }


        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var response = await _client.DeleteAsync<Product>(id,x=>x.Index(indexName)); 

            return response;
        }





    }
}
