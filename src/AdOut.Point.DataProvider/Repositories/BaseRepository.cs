﻿using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AdOut.Point.DataProvider.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
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

        public IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Where(predicate);
        }
    }
}