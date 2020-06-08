using StockControl.Abstraction.Services;
using StockControl.Data.Context;
using StockControl.Data.Entities;
using StockControl.Services.Services.Base;

namespace StockControl.Services.Services
{
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private DataContext _context;

        public CustomerService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
