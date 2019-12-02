using System;
using System.Collections.Generic;
using System.Text;

namespace ObrasBibliograficas.Domain.Interfaces
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(Guid id);
        IEnumerable<TEntity> GetAll();
        void Save(TEntity entity);
    }
}
