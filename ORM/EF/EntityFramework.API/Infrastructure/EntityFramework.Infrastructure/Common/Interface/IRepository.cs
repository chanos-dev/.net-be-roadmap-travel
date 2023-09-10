namespace EntityFramework.Infrastructure.Common.Interface
{
    public interface IRepository<T>
    {
        ICollection<T> GetAll();
        T? Get(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}
