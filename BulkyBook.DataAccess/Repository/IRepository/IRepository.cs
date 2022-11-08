using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate, string? IncludeProperties = null, bool tracked = true);

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string? IncludeProperties = null);

        void Add(T item);
        void Remove(T item);

        void RemoveRange(IEnumerable<T> items);

    }
}
