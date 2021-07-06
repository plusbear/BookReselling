using AutoMapper;
using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.Services.Repository
{
    public abstract class RepositoryBase<Entity> : IRepositoryBase<Entity> where Entity: class
    {
        public RepositoryBase(CatalogContext catalogContext, IMapper autoMapper)
        {
            _catalogContext = catalogContext;
            _autoMapper = autoMapper;
        }

        protected CatalogContext _catalogContext { get; set; }
        protected IMapper _autoMapper { get; set; }

        public IQueryable<Entity> FindAll(bool trackChanges)
        {
            if (trackChanges)
                return _catalogContext.Set<Entity>();
            else
                return _catalogContext.Set<Entity>().AsNoTracking();
        }

        public IQueryable<Entity> FindByCondition(Expression<Func<Entity, bool>> expression, bool trackChanges)
        {
            if (trackChanges)
                return _catalogContext.Set<Entity>().Where(expression);
            else
                return _catalogContext.Set<Entity>().Where(expression).AsNoTracking();
        }

        public void Create(Entity entity)
        {
            _catalogContext.Add(entity);
        }

        public void Delete(Entity entity)
        {
            _catalogContext.Remove(entity);
        }

        public void Update(Entity entity)
        {
            _catalogContext.Update(entity);
        }
    }
}
