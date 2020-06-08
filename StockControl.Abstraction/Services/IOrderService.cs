using StockControl.Abstraction.Services.Base;
using StockControl.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace StockControl.Abstraction.Services
{
    public interface IOrderService : IService<Order>
    {
        List<Customer> GetCustomers();
        List<Stock> GetStocks();
    }
}
