namespace PMS.Api.Dtos
{
    public class LoginResponse
    {
        public string UserId { get; set; }
        public string Username { get; set; } = null!;
        public string? Message { get; set; }
    }
}
