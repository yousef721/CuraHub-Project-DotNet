using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Access.Layer.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        /*
         CRUD
         C : Create
         R : Retrive
         U : Update 
         D : Delete
         */
        public void Create(T entity);
        public void Update(T entity);

        public void Delete(T entity);
        public IQueryable<T> Retrive(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProps = null, bool tracked = true);
        public T? RetriveItem(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProps = null, bool trancked = true);


    }
}
