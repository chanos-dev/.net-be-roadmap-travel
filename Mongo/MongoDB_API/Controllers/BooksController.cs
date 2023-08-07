using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB_API.Repository;

namespace MongoDB_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IMongoDBRepository<BookCollection> _repository;

        public BooksController(ILogger<BooksController> logger, IMongoDBRepository<BookCollection> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken token)
            => Ok(await _repository.GetExpressionAsync(_ => true, token));

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get([FromRoute] string id, CancellationToken token)
        //    => Ok(await _repository.GetExpressionAsync(book => book.Id == ObjectId.Parse(id), token));

        [HttpGet("{id}", Name = nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync([FromRoute]string id, CancellationToken token)
            => Ok(await _repository.GetExpressionAsync(book => book.Id == ObjectId.Parse(id), token));

        [HttpGet("{category}/{author}")]
        public async Task<IActionResult> GetByCategoryAndAuthorAsync([FromRoute] string category, [FromRoute] string author, CancellationToken token)
        {
            FilterDefinitionBuilder<BookCollection> builder = Builders<BookCollection>.Filter;
            FilterDefinition<BookCollection> filter = builder.And(
                    builder.Eq(book => book.Category, category),
                    builder.Eq(book => book.Author, author)
                );

            return Ok(await _repository.GetFilterAsync(filter, token));
        }

        [HttpPost]
        public async Task<IActionResult> Post(BookCollectionDto book, CancellationToken token)
        {
            BookCollectionDto created = await _repository.InsertAsync(book, token);
            return CreatedAtRoute(nameof(GetByIdAsync), new { id = created.Id }, created);
            //return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(BookCollectionDto book, CancellationToken token)
            => Ok(await _repository.UpdateAsync(book, token));
    }
}