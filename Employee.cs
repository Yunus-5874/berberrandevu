using System.ComponentModel.DataAnnotations;

namespace BerberWebSitesi.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim zorunludur.")]
        public string ?FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim zorunludur.")]
        public string ?LastName { get; set; }

        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email giriniz.")]
        public string ?Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string ?Phone { get; set; }

        [Required(ErrorMessage = "Yapılan işlemler zorunludur.")]
        public string ?Services { get; set; } // Yapılan işlemler

        [Required(ErrorMessage = "Çalışma günleri zorunludur.")]
        public string ?WorkDays { get; set; } // Çalışma günleri (örn. Pazartesi-Cuma)

        [Required(ErrorMessage = "Çalışma saatleri zorunludur.")]
        public string ?WorkHours { get; set; } // Çalışma saatleri (örn. 09:00-18:00)

        [Required(ErrorMessage = "İzin günleri zorunludur.")]
        public string ?LeaveDays { get; set; } // İzin günleri (örn. Cumartesi-Pazar)
    }
}
