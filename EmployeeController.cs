using BerberWebSitesi.Data;
using BerberWebSitesi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BerberWebSitesi.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly BerberDbContext _context;

        public EmployeeController(BerberDbContext context)
        {
            _context = context;
        }

        // Çalışanları listeleme
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        // Çalışan ekleme (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Çalışan ekleme (POST)
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // Çalışan düzenleme (GET)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Çalışan düzenleme (POST)
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // Çalışan silme (GET)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Çalışan silme (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
