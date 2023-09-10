using BookStoreApi.Models;
using Mapster;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB_API.Configure;
using System.Linq.Expressions;

namespace MongoDB_API.Repository
{
    public class MongoDBRepository : IMongoDBRepository<BookCollection>
    {
        private readonly IMongoCollection<BookCollection> _collection;

        public MongoDBRepository(IOptions<BookStoreDatabase> options)
        {
            MongoClient client = new(options.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(options.Value.DatabaseName);
            _collection = database.GetCollection<BookCollection>(options.Value.BooksCollectionName);
        } 

        public async Task<IEnumerable<BookCollectionDto>> GetExpressionAsync(Expression<Func<BookCollection, bool>> filter, CancellationToken token)
        {
            var books = await (await _collection.FindAsync(filter, cancellationToken: token)).ToListAsync(token);
            return ConvertEntityToDto(books);
        }

        public async Task<IEnumerable<BookCollectionDto>> GetFilterAsync(FilterDefinition<BookCollection> filter, CancellationToken token)
        {
            var books = await (await _collection.FindAsync(filter, cancellationToken: token)).ToListAsync(token);
            return ConvertEntityToDto(books);
        }

        public async Task<BookCollectionDto> InsertAsync(BookCollectionDto bookDto, CancellationToken token)
        {
            BookCollection book = bookDto.Adapt<BookCollection>();
            await _collection.InsertOneAsync(book, cancellationToken: token);
            return book.Adapt<BookCollectionDto>();
        }

        public async Task<IEnumerable<BookCollectionDto>> InsertManyAsync(List<BookCollectionDto> bookDtos, CancellationToken token)
        {
            List<BookCollection> books = bookDtos.Adapt<List<BookCollection>>();
            await _collection.InsertManyAsync(books, cancellationToken: token);
            return books.Select(book => book.Adapt<BookCollectionDto>());
        }

        public async Task<BookCollectionDto> UpdateAsync(BookCollectionDto bookDto, CancellationToken token)
        {
            BookCollection book = bookDto.Adapt<BookCollection>();
            await _collection.ReplaceOneAsync(book => book.Id == ObjectId.Parse(bookDto.Id), book, cancellationToken: token);
            return bookDto;
        }

        private IEnumerable<BookCollectionDto> ConvertEntityToDto(List<BookCollection> books)
            => books.Select(book => book.Adapt<BookCollectionDto>());
    }
}
