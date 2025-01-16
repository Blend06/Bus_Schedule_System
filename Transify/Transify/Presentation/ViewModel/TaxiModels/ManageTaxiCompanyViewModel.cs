using Transify.ViewModel.TaxiModels;
using System.Collections.Generic;

namespace Transify.Presentation.ViewModel.TaxiModels
{
    public class ManageTaxiCompanyViewModel
    {
        public List<TaxiCompanyViewModel> TaxiCompanies { get; set; } = new List<TaxiCompanyViewModel>();
    }
}
