# ElasticSearch - .Net implementasyonu (NEST Library)

Tarih: July 15, 2023 6:30 PM
Tip: KonuNotu

ElasticSearch isteklerinde **“Nest”** kütüphanesi veya kendi **“Elastic.Clients.ElasticSearch”** kütüphanesini .net tarafında kullanabiliriz.

Elastic search 7 verisyonu dahil bu sürüme kadar **“Nest”** librarye destek veriyor fakat 8 ve sonraki sürümlerde **“Elastic.Clients.ElasticSearch”**  kullanmak gereklidir. Aşağıdaki kodlar elastichsearch’ün yeni bir versiyonu ile işe yaramayabilir .

### Bağlantı işlemi

```csharp
namespace ElasticSearchWithNet.API.Extensions
{
    public  static class ElasticSearch
    {

        public static void AddElastic(this IServiceCollection services,IConfiguration configuration)
        {
            var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]));
            var settings = new ConnectionSettings(pool);
            var client = new ElasticClient(settings);
            services.AddSingleton(client);

        }
    }
}
-------------------------------------
using Elasticsearch.Net;
using Nest;
using ElasticSearchWithNet.API.Extensions;
using ElasticSearchWithNet.API.Repositories;
using ElasticSearchWithNet.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddElastic(builder.Configuration);

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

-------------------------------------
public class ProductRepository
    {

        private readonly ElasticClient _client;
        private const string indexName = "products";
        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }
```

# Nest ile endpoint implemantasyonu

<aside>
💡 SAVE - [HttpPost]

</aside>

```csharp
public async Task<Product> SaveAsync(Product newProduct)
        {
            newProduct.Created= DateTime.Now;

            var response = await _client.IndexAsync(newProduct, x => x.Index(indexName).Id(Guid.NewGuid().ToString()));
            
            //fast fail
            if (!response.IsValid) return null;

            newProduct.Id=response.Id;
            return newProduct;

        }
```

<aside>
💡 GET ALL - [HttpGet]

</aside>

```csharp
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
```

<aside>
💡 GET ONE - [HttpGet("{id}")]

</aside>

```csharp
public async Task<Product> GetByIdAsync(string id)
        {
            var response = await _client.GetAsync<Product>(id, x => x.Index(indexName));

            if (!response.IsValid)
                return null;

            response.Source.Id= response.Id;

            return response.Source;
        }
```

<aside>
💡 UPDATE - [HttpPut]

</aside>

```csharp
public async Task<bool> UpdateAsync(ProductUpdateDto updateDto)
        {
            var response = await _client.UpdateAsync<Product, ProductUpdateDto>(updateDto.Id, 
                x => x.Index(indexName).Doc(updateDto));

            return response.IsValid;
        }
```

<aside>
💡 DELETE - [HttpDelete("{id}")]

</aside>

```csharp
public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var response = await _client.DeleteAsync<Product>(id,x=>x.Index(indexName)); 

            return response;
        }
```