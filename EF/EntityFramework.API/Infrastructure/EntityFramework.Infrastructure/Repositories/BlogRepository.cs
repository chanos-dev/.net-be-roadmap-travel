using EntityFramework.Domain;
using EntityFramework.Infrastructure.Common.Interface;

namespace EntityFramework.Infrastructure.Repositories
{
    public class BlogRepository : IRepository<Blog>
    {
        private readonly EFContext _context;

        public BlogRepository(EFContext context)
        {
            _context = context;
        }

        public bool Add(Blog entity)
        {
            _context.Blogs.Add(entity);
            return _context.SaveChanges() == 1;
        }

        public bool Delete(int id)
        {
            Blog blog = _context.Blogs.Find(id) ?? throw new Exception("not found.");

            _context.Blogs.Remove(blog);
            return _context.SaveChanges() == 1;
        }

        public Blog? Get(int id)
        {
            return _context.Blogs.Find(id);
        }

        public ICollection<Blog> GetAll()
        {
            return _context.Blogs.ToList();
        }

        public bool Update(Blog entity)
        {
            if (_context.Blogs.Find(entity.Id) is null)
                return false;

            _context.Blogs.Update(entity);
            return _context.SaveChanges() == 1;
        }
    }
}
