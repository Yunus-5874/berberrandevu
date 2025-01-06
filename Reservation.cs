using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerberWebSitesi.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("BarberId")]
        public Barber Barber { get; set; }

        [Required(ErrorMessage = "Randevu tarihi zorunludur.")]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "Hizmet türü zorunludur.")]
        [MaxLength(200)]
        public string ServiceType { get; set; }
    }
}
