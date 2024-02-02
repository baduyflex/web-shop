using Mango.Web.Models;

namespace Mango.Web.Services.Interface
{
    public interface IBaseService : IDisposable
    {
        ResponseDto responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
