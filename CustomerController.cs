using Microsoft.AspNetCore.Mvc;
using BerberWebSitesi.Data;
using BerberWebSitesi.Models;

namespace BerberWebSitesi.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BerberDbContext _context;

        public CustomerController(BerberDbContext context)
        {
            _context = context;
        }

        // Müşteri Ol formunu göster
        public IActionResult Create()
        {
            return View();
        }

        // Müşteri bilgilerini kaydet
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Success");
            }

            return View(customer);
        }

        // Başarılı kayıt sonrası mesaj
        public IActionResult Success()
        {
            return View();
        }
    }
}
