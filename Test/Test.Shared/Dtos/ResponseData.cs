namespace Test.Shared.Dtos
{
    public class ResponseData
    {
        public int Code { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
    }
}
