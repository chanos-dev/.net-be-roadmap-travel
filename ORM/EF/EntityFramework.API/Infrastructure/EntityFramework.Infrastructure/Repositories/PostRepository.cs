using EntityFramework.Domain;
using EntityFramework.Infrastructure.Common.Interface;

namespace EntityFramework.Infrastructure.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private readonly EFContext _context;

        public PostRepository(EFContext context)
        {
            _context = context;
        }

        public bool Add(Post entity)
        {
            if (_context.Blogs.Find(entity.BlogId) is null)
                return false;

            _context.Posts.Add(entity);
            return _context.SaveChanges() == 1;
        }

        public bool Delete(int id)
        {
            Post post = _context.Posts.Find(id) ?? throw new Exception("not found.");

            _context.Posts.Remove(post);
            return _context.SaveChanges() == 1;
        }

        public Post? Get(int id)
        {
            return _context.Posts.Find(id);
        }

        public ICollection<Post> GetAll()
        {
            return _context.Posts.ToList();
        }

        public bool Update(Post entity)
        {
            if (_context.Posts.Find(entity.Id) is null)
                return false;

            _context.Posts.Update(entity);
            return _context.SaveChanges() == 1;
        }
    }
}
