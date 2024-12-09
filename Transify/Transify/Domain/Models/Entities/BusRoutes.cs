using System.ComponentModel.DataAnnotations;

namespace Transify.Domain.Models.Entities
{
    public class BusRoutes
    {
        /// <summary>
        /// Gets or sets the unique identifier for a bus route.
        /// </summary>
        [Key]
        public int RouteId { get; set; }

        /// <summary>
        /// Gets or sets the starting location of the bus route.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FromLocation { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the destination location of the bus route.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ToLocation { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the estimated duration for the bus route.
        /// </summary>
        [Required]
        public TimeSpan EstimatedDuration { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the route was created in the system.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the route information was last updated, if applicable.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
