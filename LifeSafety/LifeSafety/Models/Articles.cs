namespace LifeSafety.Models
{
    public class Articles
    {
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
    }
}
