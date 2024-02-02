using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Mango.Services.ProductAPI.Models.DtoS
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage { get; set; }
        public List<string> Errors { get; set; }
    }
}
