﻿using Transify.Data;
using Transify.Interfaces;
using Transify.Models.Enums;
using Transify.ViewModel.Dashboard;

namespace Transify.Services
{
    public class DashboardService: IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public DashboardViewModel GetDashboardData()
        {
            var totalUsers = _context.Users.Count();
            var taxiDrivers = _context.Users.Count(u => u.Role == UserRole.Driver);
            var buses = _context.Buses.Count();
            var taxiBookings = _context.TaxiBookings.Count();
            var taxiReservations = _context.TaxiReservations.Count();
            var busSchedules = _context.BusSchedules.Count();
            var taxiCompanies = _context.TaxiCompanies.Count();
            var busCompanies = _context.BusCompanies.Count();

            return new DashboardViewModel
            {
                TotalUsers = totalUsers,
                TaxiDrivers = taxiDrivers,
                Buses = buses,
                TaxiBookings = taxiBookings,
                TaxiReservations = taxiReservations,
                BusSchedules = busSchedules,
                TaxiCompanies = taxiCompanies,
                BusCompanies = busCompanies
            };

        }
     }
}