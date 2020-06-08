using StockControl.Data.Context;
using StockControl.Data.Entities.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace StockControl.Services.Services.Base
{
    public abstract class Service<TEntity> where TEntity : Entity
    {
        private readonly DataContext _context;

        protected Service(DataContext context)
        {
            _context = context;
        }

        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking().ToList();
        }

        public TEntity FindById(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public int Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            
            return _context.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            _context.Set<TEntity>().AddOrUpdate(entity);
            
            return _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var row = FindById(id);
            
            _context.Set<TEntity>().Remove(row);

            return _context.SaveChanges() > 0;
        }
    }
}
