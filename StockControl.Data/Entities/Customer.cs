using StockControl.Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockControl.Data.Entities
{
    public class Customer : Entity
    {
        [Required(ErrorMessage = "Müşteri Adı Soyadı boş bırakılamaz!")]
        [MaxLength(50, ErrorMessage = "Lütfen en fazla 50 karakter giriniz!")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Müşteri Kodu boş bırakılamaz.")]
        [MaxLength(11, ErrorMessage = "Lütfen en fazla 11 karakter giriniz!")]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Vergi No boş bırakılamaz!")]
        public string TaxNumber { get; set; }

        [Required(ErrorMessage = "Vergi Dairesi boş bırakılamaz!")]
        [MaxLength(50, ErrorMessage = "Lütfen en fazla 50 karakter giriniz!")]
        public string TaxName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(11, ErrorMessage = "Geçerli bir telefon numarası değil!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email boş bırakılamaz!")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                   @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                   @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                   ErrorMessage = "Email adresi geçersiz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Yetkili Adı boş bırakılamaz!")]
        [MaxLength(50, ErrorMessage = "Lütfen en fazla 50 karakter giriniz!")]
        public string AdminName { get; set; }


        public virtual ICollection<Order> Orders { get; set; }
    }
}
