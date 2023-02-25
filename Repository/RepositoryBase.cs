using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> :IRepositoryBase<T> where T : class
    {
        protected RepositoryContext context;
        public RepositoryBase(RepositoryContext _context)
        {
            context = _context;
        }

        public IQueryable<T> FindAll() => context.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition)=>context.Set<T>().Where(condition).AsNoTracking();
        public void Create(T entity) =>context.Set<T>().Add(entity);
        public void Update(T entity) => context.Set<T>().Update(entity);
        public void Delete(T entity) => context.Set<T>().Remove(entity);

            
    }
}
