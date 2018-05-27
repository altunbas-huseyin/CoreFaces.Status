using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Status.Services
{
    public interface IBaseService<TEntity>
    {
        TEntity GetById(int id);
        int Save(TEntity tEntity);
        bool Update(TEntity tEntity);
        bool Delete(int id);
        bool DropTables();
        bool EnsureCreated();
    }
}
