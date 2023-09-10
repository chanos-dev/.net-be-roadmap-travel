using BookStoreApi.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MongoDB_API.Repository
{
    public interface IMongoDBRepository<TDocument>
    {
        Task<IEnumerable<BookCollectionDto>> GetExpressionAsync(Expression<Func<TDocument, bool>> filter, CancellationToken token);
        Task<IEnumerable<BookCollectionDto>> GetFilterAsync(FilterDefinition<TDocument> filter, CancellationToken token);
        Task<BookCollectionDto> InsertAsync(BookCollectionDto bookDto, CancellationToken token);
        Task<IEnumerable<BookCollectionDto>> InsertManyAsync(List<BookCollectionDto> bookDtos, CancellationToken token);
        Task<BookCollectionDto> UpdateAsync(BookCollectionDto bookDto, CancellationToken token);
    }
}
