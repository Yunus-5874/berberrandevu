using System.ComponentModel.DataAnnotations;

namespace BerberWebSitesi.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        public string ?Name { get; set; } // Hizmet Adı
        public double Duration { get; set; }

        [Required]
        public decimal Price { get; set; } // Fiyat
    }
}
