namespace SparkNest.UI.MVC.Models.Comment
{
    public class CommentVM
    {
        public int? Id { get; set; }
        public string? ProductId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Content { get; set; }
        public string? PhotoUrl { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
    }
}
