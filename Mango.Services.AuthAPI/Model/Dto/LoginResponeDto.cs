namespace Mango.Services.AuthAPI.Model.Dto
{
    public class LoginResponeDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
