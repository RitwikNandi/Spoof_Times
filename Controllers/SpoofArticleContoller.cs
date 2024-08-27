using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Spoof_Times.ContextDb;
using Spoof_Times.Models.Articles;

namespace Spoof_Times.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpoofArticleContoller : Controller
    {
        public readonly AppDbContext DbContext;

        public SpoofArticleContoller(AppDbContext _dbContext)
        {
            DbContext = _dbContext;
        }

        [HttpGet]
        public IActionResult GetAllArticles()
        {
            //var responseList = DbContext.SpoofArticles.ToList();
            //var response = new GetResponseObject{
            //    Obj = new { responseList }
            //};
            return Ok(DbContext.SpoofArticles.ToList());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetArticleById(Guid id) {

            var articleById = DbContext.SpoofArticles.Find(id);

            if(articleById == null) return NotFound($"article {id} not found");

            return Ok(articleById);
        }

        [HttpPost]
        public IActionResult PostArticle(PostArticleDTO postArticle) {

            var articleEntity = new SpoofArticle() {

                Title = postArticle.Title,
                Description = postArticle.Description,
                Content = postArticle.Content,
                Author = postArticle.Author,
                CreatedDate = DateTime.Now,
                ImageUrl = postArticle.ImageUrl,
                Category = postArticle.Category,
                Tags = postArticle.Tags,
            };

            DbContext.SpoofArticles.Add(articleEntity);
            DbContext.SaveChanges();

            return Ok(articleEntity);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateArticle(Guid id, UpdateArticle updateArticle) {
            var article = DbContext.SpoofArticles.Find(id);
            if (article == null) return NotFound($"{id} not found !!!");

            article.Title = updateArticle.Title;
            article.Description = updateArticle.Description;
            article.Content = updateArticle.Content;
            article.ImageUrl = updateArticle.ImageUrl;
            article.Category = updateArticle.Category;
            article.Tags = updateArticle.Tags;

            DbContext.SaveChanges();

            return Ok(article);
        }

        [HttpPatch]
        [Route("{id:Guid}")]
        public IActionResult PartialUpdateArticle(JsonPatchDocument<SpoofArticle> jsonPatchDocument, Guid id)
        {
            if (jsonPatchDocument == null) return BadRequest("article not found");

            var article = DbContext.SpoofArticles.Find(id);

            if (article == null) return NotFound($"Sorry no such article with id {id} was found");
            
            jsonPatchDocument.ApplyTo(article, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return new ObjectResult(article);

            //return Ok(jsonPatchDocument);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteArticle(Guid id)
        {
            var article = DbContext.SpoofArticles.Find(id);

            if (article == null) return NotFound($"{id} not found");

            string? articleTitle = article.Title;

            DbContext.SpoofArticles.Remove(article);
            DbContext.SaveChanges();

            return Ok($"{articleTitle} has been removed and is not recoverable");
        }

        [HttpGet]
        [Route("search=")]
        public IActionResult SearchQueryString([FromQuery] string query) {

            if (string.IsNullOrEmpty(query)) return BadRequest("search cannot be empty");

            var searchQuery = query.ToLower().Split(" ");
            var searchResult = DbContext.SpoofArticles.Where(obj => 
            searchQuery.Any(term => 
            obj.Title.ToLower().Contains(term) || 
            obj.Author.ToLower().Contains(term) || 
            obj.Description.ToLower().Contains(term) || 
            obj.Content.ToLower().Contains(term))).ToList();

            return Ok(searchResult);
        }
    }
}
