using CMS.Data.Access.Layer.Data;
using CMS.Data.Access.Layer.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Access.Layer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        DbSet<T> _dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<T>();
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<T> Retrive(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProps = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProps != null)
            {
                foreach (var prop in includeProps)
                {

                    query = query.Include(prop);
                }
            }

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return query;
        }

        public T? RetriveItem(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProps = null, bool trancked = true)
        {
            return Retrive(filter, includeProps, trancked).FirstOrDefault();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
