namespace Test.Shared.DTOS
{
    public record UserDTO
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public long Type { get; set; }
        public string Token { get; set; }
    }
}
