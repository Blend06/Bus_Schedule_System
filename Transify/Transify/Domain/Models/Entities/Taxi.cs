using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Transify.Domain.Models.Enums;

namespace Transify.Domain.Models.Entities
{
    public class Taxi
    {
        /// <summary>
        /// Gets or sets the unique identifier for the taxi.
        /// </summary>
        [Key]
        public int TaxiId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the taxi company associated with the taxi.
        /// </summary>
        [ForeignKey("TaxiCompany")]
        public int TaxiCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the license plate of the taxi.
        /// </summary>
        [StringLength(20)]
        public string LicensePlate { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the driver for the taxi.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string DriverName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the current status of the taxi (e.g., Available, In-Use, etc.).
        /// </summary>
        [Required]
        public TaxiStatus Status { get; set; } = TaxiStatus.Available;

        /// <summary>
        /// Gets or sets the date and time when the taxi was created.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the taxi was last updated.
        /// Nullable to allow for no updates.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the taxi company associated with this taxi.
        /// </summary>
        public TaxiCompany TaxiCompany { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of taxi reservations associated with the taxi.
        /// </summary>
        public ICollection<TaxiReservations> TaxiReservations { get; set; } = new List<TaxiReservations>();

        /// <summary>
        /// Gets or sets the collection of taxi bookings associated with the taxi.
        /// </summary>
        public ICollection<TaxiBookings> TaxiBookings { get; set; } = new List<TaxiBookings>();

        /// <summary>
        /// Gets or sets a value indicating whether the taxi is marked as deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}