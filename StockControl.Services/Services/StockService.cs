using StockControl.Abstraction.Services;
using StockControl.Data.Context;
using StockControl.Data.Entities;
using StockControl.Services.Services.Base;

namespace StockControl.Services.Services
{
    public class StockService : Service<Stock>, IStockService
    {
        private DataContext _context;

        public StockService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
