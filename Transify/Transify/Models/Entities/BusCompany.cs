﻿using System.ComponentModel.DataAnnotations;

namespace Transify.Models.Entities
{

    public class BusCompany
    {
        /// <summary>
        /// Gets or sets the unique identifier for a bus company.
        /// </summary>
        [Key]
        public int BusCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the bus company.
        /// </summary>
        [Required]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the contact information for the bus company.
        /// </summary>
        [Required]
        public string ContactInfo { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the bus company was created in the system.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Identifier of the user who owns or manages the taxi company.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the bus company information was last updated, if applicable.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the collection of buses associated with this bus company.
        /// </summary>
        public ICollection<Buses> Buses { get; set; } = new List<Buses>();

        /// <summary>
        /// Gets or sets the collection of bus routes associated with this bus company.
        /// </summary>
        public ICollection<BusRoutes> BusRoutes { get; set; } = new List<BusRoutes>();

        /// <summary>
        /// Gets or sets the collection of bus schedules associated with this bus company.
        /// </summary>
        public ICollection<BusSchedule> BusSchedules { get; set; } = new List<BusSchedule>();

        /// <summary>
        /// Gets or sets the collection of bus reservations associated with this bus company.
        /// </summary>
        public ICollection<BusReservations> BusReservations { get; set; } = new List<BusReservations>();

        /// <summary>
        /// User who owns or manages the taxi company.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether the bus company is marked as deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
