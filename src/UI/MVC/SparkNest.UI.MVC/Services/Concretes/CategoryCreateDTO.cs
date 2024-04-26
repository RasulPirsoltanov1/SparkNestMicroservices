namespace SparkNest.UI.MVC.Services.Concretes
{
    public class CategoryCreateDTO
    {
        public string? Id { get; set; }
        public string Name { get; }
        public string? Description { get; }
        public string? SubCategoryId { get; }
        public string? PhotoUrl { get; }

        public CategoryCreateDTO(string? id,string name, string? description, string? subCategoryId, string? photoUrl)
        {
            Id= id;
            Name = name;
            Description = description;
            SubCategoryId = subCategoryId;
            PhotoUrl = photoUrl;
        }

        public override bool Equals(object? obj)
        {
            return obj is CategoryCreateDTO other &&
                   Name == other.Name &&
                   Description == other.Description &&
                   SubCategoryId == other.SubCategoryId &&
                   PhotoUrl == other.PhotoUrl;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, SubCategoryId, PhotoUrl);
        }
    }
}
