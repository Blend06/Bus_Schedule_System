using Transify.Data;
using Transify.Models.Entities;
using Transify.ViewModel.TaxiModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Transify.Models.Enums;
using Transify.Interfaces;
using Transify.Models.TaxiRequest;

namespace Transify.Services
{
    public class TaxiService : ITaxiService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthenticateService _authService;

        public TaxiService(AppDbContext context, IMapper mapper, IAuthenticateService service)
        {
            _context = context;
            _mapper = mapper;
            _authService = service;
        }

        public async Task<List<Models.TaxiRequest.TaxiRequest>> GetTaxisAsync()
        {
            var companyId = _authService.GetCurrentCompanyId();

            if (!companyId.HasValue)
                throw new UnauthorizedAccessException("No user is logged in or no associated company found.");

            var taxis = await _context.Taxis
                .Include(t => t.TaxiCompany)
                .Include(t => t.Driver)
                .Where(t => t.TaxiCompanyId == companyId.Value && !t.IsDeleted)
                .ToListAsync();

            return taxis.Select(t => new Models.TaxiRequest.TaxiRequest
            {
                TaxiId = t.TaxiId,
                LicensePlate = t.LicensePlate,
                TaxiCompanyId = t.TaxiCompanyId,
                CompanyName = t.TaxiCompany.CompanyName,
                DriverId = t.DriverId,
                DriverName = t.DriverId.HasValue
                    ? $"{t.Driver.FirstName} {t.Driver.LastName}"
                    : "No Driver Assigned"
            }).ToList();
        }

        public async Task<Taxi> AddTaxiAsync(Models.TaxiRequest.AddTaxiRequest model)
        {
            var taxi = _mapper.Map<Taxi>(model);
            _context.Taxis.Add(taxi);
            await _context.SaveChangesAsync();
            return taxi;
        }

        public async Task<bool> EditTaxiAsync(int id, Models.TaxiRequest.EditTaxiRequest model)
        {
            var taxi = await _context.Taxis.FindAsync(id);
            if (taxi == null)
                return false;

            taxi.LicensePlate = model.LicensePlate;
            taxi.DriverId = model.DriverId;
            taxi.UpdatedAt = DateTime.UtcNow;

            _context.Taxis.Update(taxi);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Models.TaxiRequest.DriverRequest>> GetAvailableDriversAsync(int? taxiId = null)
        {
            var companyId = _authService.GetCurrentCompanyId();
            if (!companyId.HasValue)
                throw new UnauthorizedAccessException("No user is logged in or no associated company found.");

            var assignedDriverIds = await _context.Taxis
                .Where(t => t.TaxiCompanyId == companyId.Value && !t.IsDeleted && (taxiId == null || t.TaxiId != taxiId))
                .Select(t => t.DriverId)
                .ToListAsync();

            var drivers = await _context.Users
                .Where(u => u.CompanyId == companyId && !assignedDriverIds.Contains(u.UserId) && !u.IsDeleted && u.Role == UserRole.Driver)
                .Select(u => new Models.TaxiRequest.DriverRequest
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                })
                .ToListAsync();

            return drivers;
        }

        public Task<bool> DeleteTaxiAsync(int id)
        {
            throw new NotImplementedException();
        }

        public TaxiCompany GetCompanyDetails()
        {
            throw new NotImplementedException();
        }

        Task<List<TaxiRequest>> ITaxiService.GetTaxisAsync()
        {
            throw new NotImplementedException();
        }

        Task<Taxi> ITaxiService.AddTaxiAsync(AddTaxiRequest model)
        {
            throw new NotImplementedException();
        }

        Task<bool> ITaxiService.EditTaxiAsync(int id, EditTaxiRequest model)
        {
            throw new NotImplementedException();
        }

        Task<List<DriverRequest>> ITaxiService.GetAvailableDriversAsync(int? taxiId)
        {
            throw new NotImplementedException();
        }
    }
}
