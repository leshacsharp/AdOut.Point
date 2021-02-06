﻿using AdOut.Extensions.Repositories;
using AdOut.Point.Model.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdOut.Point.DataProvider.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : PersistentEntity
    {
        protected IDatabaseContext Context { get; set; }
        protected DbSet<TEntity> Table { get; set; }

        public BaseRepository(IDatabaseContext context)
        {
            Context = context;
            Table = Context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            Table.Add(entity);
        }

        public void Update(TEntity entity)
        {
            Table.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public ValueTask<TEntity> GetByIdAsync(params object[] id)
        {
            return Table.FindAsync(id);
        }

        public IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Where(predicate);
        }
    }
}
