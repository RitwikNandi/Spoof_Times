using System.ComponentModel.DataAnnotations;

namespace Spoof_Times.Models.Categories
{
    public class ArticleCategory
    {
        [Key] public Guid CategoryId { get; set; }
        public required string Category { get; set; }
        public string CategoriesURL { get; set; }
    }
}
