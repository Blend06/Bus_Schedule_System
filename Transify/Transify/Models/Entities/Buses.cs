﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Transify.Models.Enums;
namespace Transify.Models.Entities
{

    /// <summary>
    /// Represents a bus entity with details including its company, status, capacity, and timestamps.
    /// </summary>
    public class Buses
    {
        /// <summary>
        /// Gets or sets the unique identifier for a bus.
        /// </summary>
        [Key]
        public int BusId { get; set; }

        /// <summary>
        /// Gets or sets the bus number, which is a unique number assigned to each bus.
        /// </summary>
        [Required]
        public int BusNumber { get; set; }

        /// <summary>
        /// Gets or sets the seating capacity of the bus.
        /// </summary>
        [Required]
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the associated bus company.
        /// </summary>
        [Required]
        [ForeignKey("BusCompany")]
        public int BusCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the bus company associated with this bus.
        /// </summary>
        public BusCompany BusCompany { get; set; } = null!;

        /// <summary>
        /// Gets or sets the current status of the bus, indicating if it is active, inactive, or under maintenance.
        /// </summary>
        public BusStatus Status { get; set; } = BusStatus.Active;

        /// <summary>
        /// Gets or sets the date and time when the bus was created in the system.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the bus information was last updated, if applicable.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// The collection of bus schedules associated with this bus.
        /// </summary>
        public ICollection<BusSchedule> BusSchedules { get; set; } = new List<BusSchedule>();

        /// <summary>
        /// Indicates whether this bus is marked as deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}