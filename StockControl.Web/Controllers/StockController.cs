using StockControl.Abstraction.Services;
using StockControl.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockControl.Web.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockService _stockService;

        public  StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public ActionResult Index()
        {
           var result= _stockService.GetAll();

            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Stock stock)
        {

            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            _stockService.Insert(stock);

            return RedirectToAction("Index", "Stock");
        }

        public ActionResult Update(int id)
        {
            var stock = _stockService.FindById(id);

            return View("Update", stock);
        }

        [HttpPost]
        public ActionResult Update(Stock stock)
        {
            if(!ModelState.IsValid)
            {
                return View("Update");
            }
            _stockService.Update(stock);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _stockService.Delete(id);

            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public JsonResult GetById(int id)
        {
            var stock = _stockService.FindById(id);

            var jsonModel = new
            {
                Price = stock.Price,
                Count = stock.Count,
                Discount = stock.Discount,
                TaxRate = stock.TaxRate,
            };

            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }
    }
}