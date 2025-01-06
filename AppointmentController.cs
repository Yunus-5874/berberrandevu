using Microsoft.AspNetCore.Mvc;
using BerberWebSitesi.Models;
using System.Linq;
using BerberWebSitesi.Data;

namespace BerberWebSitesi.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly BerberDbContext _context;

        public AppointmentController(BerberDbContext context)
        {
            _context = context;
        }



        public IActionResult Create()
        {
            // Müşteri, hizmet ve çalışanları alıyoruz
            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Employees = _context.Employees.ToList();

            // Hizmetleri veritabanından alıyoruz ve Duration'ı TotalMinutes ile double'a dönüştürüyoruz
            var services = _context.Services
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    DurationInMinutes = s.Duration, // TimeSpan -> double (dakika cinsinden)
                    s.Price
                })
                .ToList();

            // Hizmetleri ViewBag ile gönderiyoruz
            ViewBag.Services = services;

            return View();
        }




        // Yeni randevuyu kaydeder
        [HttpPost]
        public IActionResult CreateAppointment(int customerId, int serviceId, int employeeId, DateTime date, TimeSpan startTime)
        {
            // 1. Hizmet kontrolü
            var service = _context.Services.FirstOrDefault(s => s.Id == serviceId);
            if (service == null)
            {
                return BadRequest("Seçilen işlem bulunamadı.");
            }

            // 2. Çalışan kontrolü
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee == null)
            {
                return BadRequest("Seçilen çalışan bulunamadı.");
            }

            // 3. Çalışanın yetkinlik kontrolü
            if (employee.Services == null || service.Name == null || !employee.Services.Contains(service.Name))
            {
                return BadRequest("Seçilen işlem bu çalışan tarafından yapılamaz.");
            }

            // 4. Randevu çakışma kontrolü
            var endTime = startTime.Add(TimeSpan.FromMinutes(service.Duration));
            var overlappingAppointments = _context.Appointments.Any(a =>
                a.EmployeeId == employeeId &&
                a.Date == date &&
                ((startTime >= a.StartTime && startTime < a.EndTime) || // Başlangıç çakışması
                 (endTime > a.StartTime && endTime <= a.EndTime)));    // Bitiş çakışması

            if (overlappingAppointments)
            {
                return BadRequest("Bu zaman diliminde çalışan başka bir randevuya sahip.");
            }

            // 5. Randevuyu kaydetme
            var appointment = new Appointment
            {
                CustomerId = customerId,
                EmployeeId = employeeId,
                ServiceId = serviceId,
                Date = date,
                StartTime = startTime,
                EndTime = endTime
            };
            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            // 6. Sonuç ve toplam ücret
            var totalPrice = service.Price;
            return Ok(new
            {
                Message = "Randevu başarıyla oluşturuldu.",
                StartTime = startTime,
                EndTime = endTime,
                EmployeeName = employee.Id, // Çalışan adı
                TotalPrice = totalPrice
            });
        }
    }
}
