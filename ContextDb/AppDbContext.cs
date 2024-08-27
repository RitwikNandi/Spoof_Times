using Microsoft.EntityFrameworkCore;
using Spoof_Times.Models;
using Spoof_Times.Models.Articles;
using Spoof_Times.Models.Categories;

namespace Spoof_Times.ContextDb
{
    public class AppDbContext : DbContext 
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<SpoofArticle> SpoofArticles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
    }
}
