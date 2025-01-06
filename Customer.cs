using System.ComponentModel.DataAnnotations;

namespace BerberWebSitesi.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim zorunludur.")]
        public string ?FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim zorunludur.")]
        public string ?LastName { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string ?Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string ?Phone { get; set; }
    }
}
