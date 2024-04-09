namespace SparkNest.UI.MVC.Models.Gallery
{
    public class AddPhotosRequest
    {
        public string ProductId { get; set; }
        public  List<IFormFile> Photos{ get; set; }
    }
}
