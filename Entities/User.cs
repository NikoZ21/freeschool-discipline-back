namespace DisciplineBackend_WebApi.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PsswordHash { get; set; } = string.Empty;
    }
}
