﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Transify.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Transify.Models.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Transify.Models.Enums;
using Transify.Models.Utilities;
using Transify.Data;
using Transify.ViewModel.BusReservation;
using Transify.Filters;

namespace Transify.Controllers
{
    //[ServiceFilter(typeof(LoginRequiredFilter))]
    [ServiceFilter(typeof(LoginRequiredFilter))]
    [Route("UserActivity")]
    public class UserActivityController : Controller
    {
        private readonly ITaxiReservationService _taxiReservationService;
        private readonly ITaxiBookingService _taxiBookingService;
        private readonly ILogger<UserActivityController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserActivityController(
            ITaxiReservationService taxiReservationService,
              ITaxiBookingService taxiBookingService,
            ILogger<UserActivityController> logger,
            IHttpContextAccessor httpContextAccessor,
            AppDbContext context,
            IMapper mapper)
        {
            _taxiReservationService = taxiReservationService;
            _taxiBookingService = taxiBookingService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("MyReservations")]
        public async Task<IActionResult> MyReservations()
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                _logger.LogWarning("User ID not found in session.");
                return View("Error", new { message = "User not found in session. Please log in again." });
            }

            try
            {
                var reservations = await _taxiReservationService.GetReservationsForUserAsync(userId.Value);
                return View("MyReservations", reservations);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "User with ID {UserId} not found", userId);
                return View("Error", new { message = "User not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving reservations for user {UserId}", userId);
                return View("Error", new { message = "An error occurred while retrieving your reservations." });
            }
        }

        [HttpGet("MyBookings")]
        public async Task<IActionResult> MyBookings()
        {

            try
            {
                var bookings = await _taxiBookingService.GetBookingsForUserAsync();
                return View("MyBookings", bookings);
            }
            catch (KeyNotFoundException ex)
            {
                return View("Error", new { message = "User not found." });
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }


        [HttpPost("CancelBooking/{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            try
            {
                var success = await _taxiBookingService.CancelBookingAsync(id);

                if (!success)
                {
                    return BadRequest(new { message = "Booking not found, does not belong to you, or is not pending." });
                }

                _logger.LogInformation("Booking with ID {BookingId} canceled successfully", id);
                return RedirectToAction("MyBookings");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while canceling booking {BookingId}", id);
                return View("Error", new { message = "An error occurred while canceling your booking." });
            }
        }

        [HttpGet("MyBusSeats")]
        public async Task<IActionResult> MyBusSeats()
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return Unauthorized();
            }

            try
            {
                var busReservations = await _context.BusReservations
                    .Include(r => r.Schedule)
                      .ThenInclude(s => s.Route)
                    .Where(r => r.UserId == userId.Value)
                    .ToListAsync();

                var reservationViewModels = _mapper.Map<List<MyBusReservationViewModel>>(busReservations);
                return View(reservationViewModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving bus reservations for user {UserId}", userId);
                return View("Error", new { message = "An error occurred while retrieving your bus reservations." });
            }
        }

        [HttpPost("CancelReservation/{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return Unauthorized();
            }

            try
            {
                var success = await _taxiReservationService.CancelReservationAsync(id, userId.Value);

                if (!success)
                {
                    _logger.LogWarning("Reservation with ID {ReservationId} cannot be canceled because it is not pending or does not belong to user {UserId}.", id, userId);
                    return BadRequest(new { message = "Reservation not found, does not belong to you, or is not pending." });
                }

                _logger.LogInformation("Reservation with ID {ReservationId} canceled successfully.", id);
                return RedirectToAction("MyReservations");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while canceling reservation with ID {ReservationId}.", id);
                return View("Error", new { message = "An error occurred while canceling your reservation." });
            }
        }

        [HttpPost("CancelBusReservation/{id}")]
        public async Task<IActionResult> CancelBusReservation(int id)
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return Unauthorized();
            }

            try
            {
                var busReservation = await _context.BusReservations
                    .FirstOrDefaultAsync(r => r.ReservationId == id && r.UserId == userId.Value);

                if (busReservation == null)
                {
                    _logger.LogWarning("Bus reservation with ID {ReservationId} not found or does not belong to user {UserId}.", id, userId);
                    return NotFound(new { message = "Reservation not found or does not belong to you." });
                }

                if (busReservation.Status != BusReservationStatus.Pending)
                {
                    _logger.LogWarning("Bus reservation with ID {ReservationId} cannot be canceled as it is not in 'Pending' status.", id);
                    return BadRequest(new { message = "Reservation cannot be canceled because it is not pending." });
                }

                busReservation.Status = BusReservationStatus.Canceled;
                _context.BusReservations.Update(busReservation);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Bus reservation with ID {ReservationId} has been canceled successfully.", id);
                return RedirectToAction("MyBusSeats");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while canceling bus reservation with ID {ReservationId}.", id);
                return View("Error", new { message = "An error occurred while canceling your bus reservation." });
            }
        }
    }
}