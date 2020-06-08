using StockControl.Abstraction.Services;
using StockControl.Data.Context;
using StockControl.Data.Entities;
using StockControl.Services.Services.Base;
using System.Collections.Generic;
using System.Linq;

namespace StockControl.Services.Services
{
    public class OrderService : Service<Order>, IOrderService
    {
        DataContext _context;

        public OrderService(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<Customer> GetCustomers()
        {
            return _context.Set<Customer>().ToList();
        }
        public List<Stock> GetStocks()
        {
            return _context.Set<Stock>().ToList();
        }
    }
}
