using EntityFramework.Domain;
using EntityFramework.Infrastructure.Common.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IRepository<Post> _repository;
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger, IRepository<Post> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<ICollection<PostDto>> Get()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<PostDto> Get(int id)
        {
            return Ok(_repository.Get(id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] PostDto Post)
        {            
            _repository.Add(new Post()
            {
                Id = Post.Id,
                Title = Post.Title,
                Content = Post.Content,
                BlogId = Post.BlogId,
            });

            return CreatedAtAction(nameof(Get), Post);
        }

        [HttpPut]
        public ActionResult Update([FromBody] PostDto Post)
        {
            _repository.Update(new Post()
            {
                Id = Post.Id,
                Title = Post.Title,
                Content = Post.Content,
                BlogId = Post.BlogId,
            });

            return Ok(Post);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        { 
            return Ok(_repository.Delete(id));
        }
    }
}