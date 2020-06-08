using StockControl.Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockControl.Data.Entities
{
    public class Stock : Entity
    {
        [Required(ErrorMessage = "Stok Adı boş bırakılamaz!")]
        [MaxLength(50, ErrorMessage = "Lütfen en fazla 50 karakter giriniz!")]
        public string StockName { get; set; }

        [Required(ErrorMessage = "Stok Kodu boş bırakılamaz!")]
        [MinLength(3, ErrorMessage = "Lütfen en az 3 karakter giriniz!")]
        public string StockCode { get; set; }

        [Required(ErrorMessage = "Stok Adet boş bırakılamaz!")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Birim Fiyat boş bırakılamaz!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "İskonto Oranı boş bırakılamaz!")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Vergi Oranı boş bırakılamaz!")]
        public decimal TaxRate { get; set; }


        public virtual ICollection<Order> Orders { get; set; }
    }
}