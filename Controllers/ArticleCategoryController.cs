using Microsoft.AspNetCore.Mvc;
using Spoof_Times.ContextDb;
using Spoof_Times.Models.Categories;
using System.Reflection.Metadata.Ecma335;

namespace Spoof_Times.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticleCategoryController : Controller
    {
        private readonly AppDbContext DbContext;

        public ArticleCategoryController(AppDbContext _dbContext)
        {
           DbContext = _dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(DbContext.ArticleCategories.ToList());
        }

        [HttpPost]
        public IActionResult PostCategory(PostCategories postCategories)
        {
            var categoryEntity = new ArticleCategory()
            {
                Category = postCategories.Category,
                CategoriesURL = postCategories.CategoriesURL,
            };

            DbContext.ArticleCategories.Add(categoryEntity);
            DbContext.SaveChanges();

            return Ok(categoryEntity);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateCategory(UpdateCategory updateCategory, Guid id)
        {
            var category = DbContext.ArticleCategories.Find(id);

            if (category == null) BadRequest($"Category of {category} does not exist");

            category.Category = updateCategory.Category;
            category.CategoriesURL = updateCategory.CategoriesURL;

            DbContext.SaveChanges();

            return Ok(category);
        }
    }
}
