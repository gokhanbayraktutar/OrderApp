using StockControl.Data.Entities.Base;
using System;

namespace StockControl.Data.Entities
{
    public class Order : Entity
    {
        public int CustomerId { get; set; }

        public int StockId { get; set; }

        public int Count { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalStockPrice { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal TotalTaxPrice { get; set; }

        public decimal TotalPrice { get; set; }


        public virtual Customer Customer { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
