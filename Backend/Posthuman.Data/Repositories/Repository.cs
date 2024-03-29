﻿using Microsoft.EntityFrameworkCore;
using Posthuman.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Posthuman.Data.Repositories
{
    /// <summary>
    /// Implementation of generic repository that is responsible for handling data (model) manipulations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> :IRepository<TEntity> where TEntity : class 
    {
        protected readonly PosthumanContext Context;

        public Repository(PosthumanContext context)
        {
            Context = context;
        }

        public async ValueTask<TEntity> GetByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }


        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }


        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
       

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            // TODO - fix!
            throw new NotImplementedException();
        }
        public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            // TODO - fix!
            throw new NotImplementedException();
        }
    }
}
