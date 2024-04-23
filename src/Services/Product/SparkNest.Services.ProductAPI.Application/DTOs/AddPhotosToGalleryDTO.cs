namespace SparkNest.Services.ProductAPI.DTOs
{
    public class AddPhotosToGalleryDTO
    {
        public string? ProductId { get; set; }
        public List<string>? PhotoUrl { get; set; }
    }
}
