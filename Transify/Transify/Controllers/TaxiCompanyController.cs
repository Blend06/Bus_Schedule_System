﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transify.Models.Enums;
using Transify.Filters;
using Transify.Data;

using Transify.Models.TaxiRequest;
using Transify.Models.Utilities;
using System.Threading.Tasks;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Transify.Interfaces;

namespace Transify.Controllers
{
    [Route("Admin/TaxiCompany")]
    [ServiceFilter(typeof(AdminOnlyFilter))]
    public class TaxiCompanyController : Controller
    {
        private readonly ITaxiCompanyService _taxiCompanyService;

        public TaxiCompanyController(
            ITaxiCompanyService taxiCompanyService)
        {
            _taxiCompanyService = taxiCompanyService;
        }

        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany([FromBody] TaxiCompanyRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _taxiCompanyService.AddCompanyAndAssignUserAsync(model);
                if (result)
                    return Ok(ResponseFactory.SuccessResponse("Company added successfully.", result));

                return BadRequest(ResponseFactory.ErrorResponse(ResponseCodes.InvalidData, "Invalid data provided or operation failed."));
            }
            return BadRequest(ResponseFactory.ErrorResponse(ResponseCodes.InvalidData, ResponseMessages.InvalidData));
        }

        [HttpGet("GetCompanies")]
        public IActionResult GetCompanies()
        {
            var viewModel = _taxiCompanyService.GetAllCompaniesWithTaxis();

            if (viewModel == null)
            {
                return NotFound(ResponseFactory.ErrorResponse(ResponseCodes.NotFound, "Taxi Companies not found."));
            }

            return Ok(viewModel);
        }

        [HttpGet("GetTaxiCompanyUsers")]
        public IActionResult GetTaxiCompanyUsers()
        {
            var taxiCompanyUser = _taxiCompanyService.GetUnassignedTaxiCompanyUsers();

            if (taxiCompanyUser == null)
            {
                return NotFound(ResponseFactory.ErrorResponse(ResponseCodes.NotFound, "Users in Taxi Company not found."));
            }

            return Ok(taxiCompanyUser);
        }

        [HttpGet("GetTaxiCompanyUser/{companyId}")]
        public IActionResult GetTaxiCompanyUser(int companyId)
        {
            var company = _taxiCompanyService.GetTaxiCompanyUser(companyId);
            if (company == null)
            {
                return NotFound(ResponseFactory.ErrorResponse(ResponseCodes.NotFound, "Taxi Company not found."));
            }

            return Ok(new { success = true, data = company });
        }

        [HttpPut("EditCompany/{id}")]
        public async Task<IActionResult> EditCompany(int id, [FromBody] TaxiCompanyRequest model)
        {
            var result = await _taxiCompanyService.EditCompanyAndAssignUserAsync(id, model);
            if (!result)
            {
                return NotFound(ResponseFactory.ErrorResponse(ResponseCodes.NotFound, "Company not found."));
            }

            return Ok(ResponseFactory.SuccessResponse("Company updated successfully.", result));
        }

        [HttpDelete("DeleteCompany/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var success = await _taxiCompanyService.DeleteCompanyAsync(id);
            if (!success)
            {
                return NotFound(ResponseFactory.ErrorResponse(ResponseCodes.NotFound, "Company not found."));
            }

            return Ok(ResponseFactory.SuccessResponse("Company and its taxis deleted successfully (soft delete).", success));
        }
    }
}