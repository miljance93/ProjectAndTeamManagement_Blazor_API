namespace Shared.DTOs
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
