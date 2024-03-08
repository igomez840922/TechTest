namespace Test.Shared.Entities.DTO
{
    public class AuthenticationResponse
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
    }
}
