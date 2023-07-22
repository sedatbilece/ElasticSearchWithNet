

using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace ElasticSearchWithNet.API.Extensions
{
    public  static class ElasticSearch
    {

        public static void AddElastic(this IServiceCollection services,IConfiguration configuration)
        {


            var userName = configuration.GetSection("Elastic")["username"];
            var password = configuration.GetSection("Elastic")["password"];

            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]))
                .Authentication(new BasicAuthentication(userName, password));

            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);

        }
    }
}
