﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Transify.Models;
using Transify.Filters;
using AutoMapper;
using Transify.Data;
using Transify.ViewModel.Bus;
using Transify.Models.TaxiRequest;
using Transify.Services;
using Transify.Models.Enums;
using Transify.Models.Utilities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transify.ViewModel.Dashboard;
using Transify.Filters;
using Transify.Interfaces;
using Transify.Models.TaxiRequest;

using Transify.ViewModel.Bus;

namespace Transify.Controllers
{
    [ServiceFilter(typeof(AdminBaseFilter))]
    [Route("Dashboard")]
    public class DashboardController : Controller
    {
        private readonly IBusCompanyService _busCompanyService;
        private readonly ITaxiCompanyService _taxiCompanyService;
        private readonly IDashboardService _dashboardService;

        public DashboardController(ITaxiCompanyService taxiCompanyService, IBusCompanyService busCompanyService, IDashboardService dashboardService)
        {
            _taxiCompanyService = taxiCompanyService;
            _busCompanyService = busCompanyService;
            _dashboardService = dashboardService;
        }

        public IActionResult Dashboard()
        {
            var model = _dashboardService.GetDashboardData();
            return View(model);
        }

        [HttpGet("Bus")]
        public IActionResult Bus()
        {
            var busCompanies = _busCompanyService.GetAllBusCompanies();
            var viewModel = new ManageBusCompanyViewModel
            {
                BusCompanies = busCompanies
            };

            return View(viewModel);
        }

        [HttpGet("Taxi")]
        public IActionResult Taxi()
        {
            var taxiCompanies = _taxiCompanyService.GetAllCompanies();

            var viewModel = new ManageTaxiCompanyRequest
            {
                TaxiCompanies = taxiCompanies
            };

            return View(viewModel);
        }
    }
}