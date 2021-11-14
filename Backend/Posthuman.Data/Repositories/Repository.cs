using Microsoft.EntityFrameworkCore;
using Posthuman.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Posthuman.Data.Repositories
{
    public class Repository<TEntity> :IRepository<TEntity> where TEntity : class 
    {
        protected readonly PosthumanContext Context;

        public Repository(PosthumanContext context)
        {
            this.Context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        //public async IAsyncEnumerable<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        //}


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }


        public async ValueTask<TEntity> GetByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }


        //public async Task<TEntity> GetByIdAsync(int id)
        //{
        //    return await Context.Set<TEntity>().FindAsync(id);
        //}

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }


        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
            //return;// await Context.Set<TEntity>().Update(entity);
        }

        public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
            // Context.Set<TEntity>().UpdateRange(entities);
        }
    }
}
