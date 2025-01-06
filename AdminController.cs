using Microsoft.AspNetCore.Mvc;
using BerberWebSitesi.Data;
using BerberWebSitesi.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BerberWebSitesi.Controllers
{
    public class AdminController : Controller
    {
        private readonly BerberDbContext _context;

        public AdminController(BerberDbContext context)
        {
            _context = context;
        }

        // Admin giriş sayfasını göster
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Username == username && a.Password == password);

            if (admin != null)
            {
                HttpContext.Session.SetString("Admin", "LoggedIn");
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }
        }

        // Admin paneli ana sayfası
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") != "LoggedIn")
            {
                return RedirectToAction("Login");
            }

            return View(_context.Employees.ToList());
        }
        public IActionResult ListCustomers()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        // Çalışanları Listeleme
        public IActionResult EmployeeList()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        // Çalışan Ekleme (GET)
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        // Çalışan Ekleme (POST)
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("EmployeeList");
            }
            return View(employee);
        }

        // Çalışan Düzenleme (GET)
        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Çalışan Düzenleme (POST)
        [HttpPost]
        public IActionResult EditEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return RedirectToAction("EmployeeList");
            }
            return View(employee);
        }

        // Çalışan Silme (GET)
        [HttpGet]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Çalışan Silme (POST)
        [HttpPost, ActionName("DeleteEmployee")]
        public IActionResult DeleteEmployeeConfirmed(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction("EmployeeList");
        }


        // Admin çıkışı
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

