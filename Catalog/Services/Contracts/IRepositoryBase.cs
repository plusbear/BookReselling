using System;
using System.Linq;
using System.Linq.Expressions;

namespace Catalog.Services.Repository
{
    interface IRepositoryBase<Entity>
    {
        IQueryable<Entity> FindAll(bool trackChanges);
        IQueryable<Entity> FindByCondition(Expression<Func<Entity, bool>> expression, bool trackChanges);
        void CreateEntity(Entity entity);
        void UpdateEntity(Entity entity);
        void DeleteEntity(Entity entity);
    }
}
