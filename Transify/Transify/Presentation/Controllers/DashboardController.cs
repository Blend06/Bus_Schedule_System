using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Transify.Domain.Models;
using Transify.Presentation.Filters;
using AutoMapper;
using Transify.Infrastructure.Data;
using Transify.Domain.Interfaces;
using Transify.Presentation.ViewModel.Bus;
using Transify.Presentation.ViewModel.TaxiRequest;
using Transify.Application.Services;
using Transify.Domain.Models.Enums;
using Transify.Domain.Models.Utilities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transify.Presentation.ViewModel.Dashboard;

namespace Transify.Presentation.Controllers
{
    [ServiceFilter(typeof(AdminBaseFilter))]
    [Route("Dashboard")]
    public class DashboardController : Controller
    {
        private readonly IBusCompanyService _busCompanyService;
        private readonly ITaxiCompanyService _taxiCompanyService;
        private readonly IDashboardService _dashboardService;

        public DashboardController(ITaxiCompanyService taxiCompanyService, IBusCompanyService busCompanyService ,IDashboardService dashboardService)
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