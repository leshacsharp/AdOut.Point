using AdOut.Point.Model.Interfaces.Managers;
using AdOut.Point.Model.Interfaces.Repositories;
using System;

namespace AdOut.Point.Core.Managers
{
    //todo: mb need to delete the base manager
    public abstract class BaseManager<TEntity> : IBaseManager<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;
        public BaseManager(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public void Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _repository.Create(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _repository.Delete(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _repository.Update(entity);
        }
    }
}
