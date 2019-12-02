using ObrasBibliograficas.Domain.Entities;
using ObrasBibliograficas.Domain.Interfaces;
using ObrasBibliograficas.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObrasBibliograficas.Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context) => _context = context;

        public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().AsEnumerable();

        public TEntity GetById(Guid id)
        {
            var query = _context.Set<TEntity>().Where(e => e.Id == id);

            if (query.Any())
                return query.First();

            return null;
        }


        public void Save(TEntity entity) => _context.Set<Entity>().Add(entity);

    }
}
