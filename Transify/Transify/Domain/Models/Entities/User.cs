﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Transify.Domain.Models.Enums;

namespace Transify.Domain.Models.Entities
{
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hashed password of the user.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's role within the application (e.g., User, Admin).
        /// </summary>
        [Required]
        public UserRole Role { get; set; } = UserRole.User;

        /// <summary>
        /// Gets or sets a value indicating whether the user has administrative privileges.
        /// </summary>
        public bool IsAdmin { get; set; } = false;

        /// <summary>
        /// Gets or sets the date and time when the user was created.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the collection of taxi reservations associated with the user.
        /// </summary>
        public ICollection<TaxiReservations> TaxiReservations { get; set; } = new List<TaxiReservations>();

        /// <summary>
        /// Gets or sets the collection of taxi bookings made by the user.
        /// </summary>
        public ICollection<TaxiBookings> TaxiBookings { get; set; } = new List<TaxiBookings>();

        /// <summary>
        /// Gets or sets the collection of notifications for the user.
        /// </summary>
        public ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();

        /// <summary>
        /// Gets or sets a value indicating whether the user is marked as deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
