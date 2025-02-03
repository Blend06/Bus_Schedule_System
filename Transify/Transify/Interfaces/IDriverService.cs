using Transify.ViewModel.Authenticate;
using Transify.ViewModel.TaxiModels;
using Transify.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transify.Models.TaxiRequest;

namespace Transify.Services
{
    /// <summary>
    /// Provides functionality for managing drivers in the application.
    /// </summary>
    public interface IDriverService
    {
        /// <summary>
        /// Adds a new driver to the authenticated user's taxi company.
        /// </summary>
        Task<User> AddDriverAsync(RegisterDriverRequest model);

        /// <summary>
        /// Retrieves a list of drivers associated with the authenticated user's taxi company.
        /// </summary>
        List<DriverRequest> GetDrivers();

        /// <summary>
        /// Edits the details of an existing driver within the authenticated user's taxi company.
        /// </summary>
        Task<bool> EditDriverAsync(int id, EditDriverRequest model);

        /// <summary>
        /// Deletes an existing driver within the authenticated user's taxi company by marking them as deleted.
        /// </summary>
        Task<bool> DeleteDriverAsync(int id);
    }
}