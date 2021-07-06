using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    interface IRepositoryBase<Entity>
    {
        IQueryable<Entity> FindAll(bool trackChanges);
        IQueryable<Entity> FindByCondition(Expression<Func<Entity, bool>> expression, bool trackChanges);
        void Create(Entity entity);
        void Update(Entity entity);
        void Delete(Entity entity);
    }
}
