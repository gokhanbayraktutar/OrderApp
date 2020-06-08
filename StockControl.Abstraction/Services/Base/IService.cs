using System.Collections.Generic;

namespace StockControl.Abstraction.Services.Base
{
    public interface IService<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();

        TEntity FindById(int id);

        int Insert(TEntity entity);

        int Update(TEntity entity);

        bool Delete(int id);
    }
}
