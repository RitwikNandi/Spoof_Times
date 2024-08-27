using System.ComponentModel.DataAnnotations;

namespace Spoof_Times.Models
{
    public class ArticleTag
    {
        [Key] public Guid TagId { get; set; }
        public required string TagName { get; set; }
        public string TagURL { get; set; }
    }
}
