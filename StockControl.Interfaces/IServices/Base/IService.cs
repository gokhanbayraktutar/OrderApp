using System.Collections.Generic;
using System.Linq;

namespace StokTakip.Interfaces.IServices.Base
{
    public interface IService<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
    }
}
