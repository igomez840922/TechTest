namespace Test.Shared.DTOS
{
    public class ProductDTO
    {
        public long UserID { get; set; }
        public long ID { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string ImageData { get; set; }
        public string MimeType { get; set; }
    }
}
