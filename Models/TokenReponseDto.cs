namespace DisciplineBackend_WebApi.Models
{
    public class TokenReponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}