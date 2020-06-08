using StockControl.Abstraction.Services;
using StockControl.Data.Entities;
using StockControl.Data.Entities.Base;
using System.Web.Mvc;

namespace StockControl.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var result = _customerService.GetAll();

            return View(result);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {

            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            _customerService.Insert(customer);

            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Update(int id)
        {
            var customer = _customerService.FindById(id);

            return View("Update", customer);
        }

        [HttpPost]
        public ActionResult Update(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            _customerService.Update(customer);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _customerService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}