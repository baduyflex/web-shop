using Mango.Web.Models;
using Mango.Web.Services.Interface;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Mango.Web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory;
            this.responseModel = new ResponseDto();
        }


        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("Mango");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri("https://localhost:7008" + apiRequest.Url);
                client.DefaultRequestHeaders.Clear();

                if (apiRequest.Data is not null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                HttpResponseMessage apiResponse = null;

                switch (apiRequest.apiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponeDto = JsonConvert.DeserializeObject<T>(apiContent);

                return apiResponeDto;
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    Errors = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };

                var res = JsonConvert.SerializeObject(dto);
                var apiResponeDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponeDto;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
