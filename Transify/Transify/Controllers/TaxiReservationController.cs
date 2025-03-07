﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transify.Data;
using Transify.Interfaces;
using Transify.Models.Entities;
using Transify.Models.Enums;
using Transify.Models.TaxiRequest;
using Transify.Filters;

namespace Transify.Controllers
{
    [Route("api/TaxiReservation")]
    public class TaxiReservationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITaxiReservationService _taxiReservationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TaxiReservationController(AppDbContext context, ITaxiReservationService taxiReservationService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _taxiReservationService = taxiReservationService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("SearchAvailableTaxis")]
        public async Task<IActionResult> SearchAvailableTaxis()
        {
            var taxiCompanies = await _taxiReservationService.SearchAvailableTaxisAsync();

            return Ok(new { success = true, companies = taxiCompanies });
        }

        [ServiceFilter(typeof(LoginRequiredFilter))]
        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] TaxiReservationViewModel model)
        {
            return await _taxiReservationService.CreateReservationAsync(model);
        }
    }
}