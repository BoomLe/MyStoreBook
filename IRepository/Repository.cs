using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Book.IRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BookDbContext _db;
        internal DbSet<T>  dbset;
        public Repository(BookDbContext db )
        {
            _db = db;
            this.dbset = _db.Set<T>();
            _db.Products.Include(p => p.Category).Include(p=> p.CategoryId);
            
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter,string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            query = query.Where(filter);
           
            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            return query.FirstOrDefault();
            //Note:
            // _db.category.FirstOrDefault(u => u.Id = id) to same
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }
    }
}