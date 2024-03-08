namespace Test.Shared.Entities.DTO
{
    public class RegisterResponse
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }

}
