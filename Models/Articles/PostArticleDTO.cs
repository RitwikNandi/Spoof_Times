﻿namespace Spoof_Times.Models.Articles
{
    public class PostArticleDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Content { get; set; }
        public required string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ImageUrl { get; set; }
        public required string Category { get; set; }
        public string[] Tags { get; set; }
    }
}
