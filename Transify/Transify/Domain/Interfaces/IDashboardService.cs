using Transify.Presentation.ViewModel.Dashboard;

namespace Transify.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for a service that provides dashboard data.
    /// </summary>
    public interface IDashboardService
    {
        /// <summary>
        /// Retrieves aggregated data for the dashboard.
        /// </summary>
        DashboardViewModel GetDashboardData();
    }
}