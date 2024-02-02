using Mango.Web.Models;
using Mango.Web.Services.Interface;

namespace Mango.Web.Services
{
    public class ProductServices : BaseService, IProductService
    {
        public readonly IHttpClientFactory httpClient;

        public ProductServices(IHttpClientFactory httpClient) : base(httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.POST,
                Data = productDto,
                Url = SD.ProductAPIBase = "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase = "/api/products/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase = "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase = "/api/products/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.PUT,
                Data = productDto,
                Url = SD.ProductAPIBase = "/api/products",
                AccessToken = ""
            });
        }
    }
}
