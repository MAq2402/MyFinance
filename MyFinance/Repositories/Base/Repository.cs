using MyFinance.DbContexts;
using MyFinance.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyFinance.Repositories.Base
{
    public abstract class Repository<T> : IRepository<T> where T:Entity
    {
        protected AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }


        public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual T GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public virtual bool Save()
        {
            if (_context.SaveChanges()>0)
            {
                return true;
            }
            return false;
        }

        public virtual void Update(T entity)
        {
            
        }
    }
}
