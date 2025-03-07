﻿using System.ComponentModel.DataAnnotations;
using Transify.Models.Enums;

namespace Transify.Models.Entities
{
    public class BusReservations
    {
        /// <summary>
        /// Gets or sets the primary key for the bus reservation.
        /// </summary>
        [Key]
        public int ReservationId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the reservation is made.
        /// </summary>
        [Required]
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Gets or sets the number of seats reserved.
        /// </summary>
        [Required]
        public int NumberOfSeats { get; set; }

        /// <summary>
        /// Gets or sets the total amount paid for the reservation.
        /// </summary>
        [Required]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the payment status for the reservation.
        /// </summary>
        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the creation date and time of the reservation, defaulting to the current UTC time.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the last updated date and time of the reservation. Null if never updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the schedule ID linked to this reservation.
        /// </summary>
        [Required]
        public int ScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the user ID of the person who made the reservation.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// The nullable unique identifier for the associated bus company.
        /// </summary>
        public int? BusCompanyId { get; set; }

        /// <summary>
        /// The bus company associated with this entity.
        /// </summary>
        public BusCompany BusCompany { get; set; } = null!;

        /// <summary>
        /// Gets or sets the current status of the reservation.
        /// </summary>
        [Required]
        public BusReservationStatus Status { get; set; } = BusReservationStatus.Pending;

        /// <summary>
        /// Navigation property for accessing the bus schedule associated with the reservation.
        /// </summary>
        public BusSchedule? Schedule { get; set; } = null!;

        /// <summary>
        /// Navigation property for accessing the user who made the reservation.
        /// </summary>
        public User User { get; set; } = null!;
    }
}
