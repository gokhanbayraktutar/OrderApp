using StockControl.Abstraction.Services;
using StockControl.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StockControl.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IStockService _stockService;
        private readonly ICustomerService _customerService;

        public OrderController(IOrderService orderService, IStockService stockService, ICustomerService customerService)
        {
            _orderService = orderService;
            _stockService = stockService;
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var result = _orderService.GetAll();

            return View(result);
        }

        public ActionResult NewOrder()
        {
            SetComboboxes();
            return View();
        }

        [HttpPost]
        public ActionResult NewOrder(Order order)
        {
            if (order.CustomerId == 0 || order.StockId == 0 || order.Count == 0)
            {
                ViewBag.RequiredError = true;
                SetComboboxes();
                return View(order);
            }

            var stock = _stockService.FindById(order.StockId);

            if (stock.Count < order.Count)
            {
                ViewBag.StockError = stock.Count;
                SetComboboxes();
                return View(order);
            }

            var totalStockPrice = stock.Price * order.Count;
            var totalDiscount = totalStockPrice * stock.Discount / 100;
            var item = totalStockPrice - totalDiscount;
            var totalTaxPrice = item * stock.TaxRate / 100;
            var totalPrice = totalStockPrice - totalDiscount + totalTaxPrice;

            order.TotalDiscount = Math.Round(totalDiscount, 2);
            order.TotalStockPrice = Math.Round(totalStockPrice, 2);
            order.TotalTaxPrice = Math.Round(totalTaxPrice, 2);
            order.TotalPrice = Math.Round(totalPrice, 2);
            order.OrderDate = DateTime.Now;

            var result = _orderService.Insert(order);

            if (result > 0)
            {
                stock.Count -= order.Count;
                _stockService.Update(stock);

                var customer = _customerService.FindById(order.CustomerId);

                SendEmail(customer, stock, order);
            }

            return RedirectToAction("Index", "Order");
        }

        private void SetComboboxes()
        {
            var customers = _orderService.GetCustomers().Select(x => new SelectListItem
            {
                Text = x.CustomerName,
                Value = x.Id.ToString()
            }).ToList();

            ViewBag.customers = customers;

            var stocks = _orderService.GetStocks().Select(x => new SelectListItem
            {
                Text = x.StockName,
                Value = x.Id.ToString()
            }).ToList();

            ViewBag.stocks = stocks;
        }

        private void SendEmail(Customer customer, Stock stock, Order order)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("", ""); //Bu kısma göndericinin mail adresi ve şifre girilmesi gerekmektedir.

            var mail = new MailMessage();
            mail.From = new MailAddress(""); //Bu kısma gönderici mail adresi girilmelidir.
            mail.To.Add(customer.Email);
            mail.Subject = "Siparişiniz Alındı";
            mail.IsBodyHtml = true;
            mail.Body = $"<htl><div>Siparişiniz işleme alınmıştır. Siparişiniz ile ilgili detaylı bilgiyi aşağıda size sunuyoruz.</div><br><br><div><pre>Ürün Kodu          :{stock.StockCode}</pre><pre>Ürün Adı	   :{stock.StockName}</pre><pre>Ürün Adedi	   :{order.Count}</pre><pre>Birim Fiyatı	   :{stock.Price}₺</pre><pre>Ürün Tutarı	   :{order.TotalStockPrice}₺</pre><pre>İskonto Tutarı	   :{order.TotalDiscount}₺</pre><pre>Kdv Tutarı	   :{order.TotalTaxPrice}₺</pre><pre><b><h3>Toplam Tutar    :{order.TotalPrice}₺</h3></b></pre></div></html>";
            client.Send(mail);
        }
    }
}