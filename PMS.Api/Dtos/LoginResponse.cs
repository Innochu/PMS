namespace PMS.Api.Dtos
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string? Message { get; set; }
    }
}
