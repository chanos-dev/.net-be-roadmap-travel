using EntityFramework.Domain;
using EntityFramework.Infrastructure.Common.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IRepository<Blog> _repository;
        private readonly ILogger<BlogController> _logger;

        public BlogController(ILogger<BlogController> logger, IRepository<Blog> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<ICollection<BlogDto>> Get()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<BlogDto> Get(int id)
        {
            return Ok(_repository.Get(id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] BlogDto blog)
        {            
            _repository.Add(new Blog()
            {
                Id = blog.Id,
                URL = blog.URL,
            });

            return CreatedAtAction(nameof(Get), blog);
        }

        [HttpPut]
        public ActionResult Update([FromBody] BlogDto blog)
        {
            _repository.Update(new Blog()
            {
                Id = blog.Id,
                URL = blog.URL,
            });

            return Ok(blog);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        { 
            return Ok(_repository.Delete(id));
        }
    }
}