using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _db;
        internal DbSet<T> dbset;

        public Repository(ApplicationDBContext db)
        {
            _db = db;
            //_db.Products.Include(u => u.CoverType).Include(u => u.Category);

            this.dbset = _db.Set<T>();
        }
        public void Add(T item)
        {
            dbset.Add(item);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string? IncludeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (IncludeProperties != null)
            {
                foreach (var property in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate, string? IncludeProperties = null, bool tracked = true)
        {

            IQueryable<T> query;
            if (tracked)
            {
                query = dbset;
            }
            else
            {
                query = dbset.AsNoTracking();
            }


            query = query.Where(predicate);
            if (IncludeProperties != null)
            {
                foreach (var property in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T item)
        {
            dbset.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            dbset.RemoveRange(items);
        }


    }
}
