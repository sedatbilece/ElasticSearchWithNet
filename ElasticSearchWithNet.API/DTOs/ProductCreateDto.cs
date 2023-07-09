using ElasticSearchWithNet.API.Models;

namespace ElasticSearchWithNet.API.DTOs
{
    public record ProductCreateDto(string Name,decimal Price,int Stock,ProductFeatureDto Feature)
    {


        //for mapping Dto to Model
        public Product CreateProduct()
        {
            return new Product { 
                Name = Name,
                Price = Price,
                Stock = Stock,
                Feature = new ProductFeature
                {
                    Width = Feature.Width,
                    Height = Feature.Height,
                    Color = Feature.Color
                }
            
            };
        }

    }
}
