using Taxually.TechnicalTest.Contracts;
using Taxually.TechnicalTest.Controllers;

namespace Taxually.TechnicalTest.Interfaces
{
    public interface IVatRegistrationService
    {
        public Task VatRegistrationForGB(VatRegistrationRequest request);
        public Task VatRegistrationForFR(VatRegistrationRequest request);
        public Task VatRegistrationForDE(VatRegistrationRequest request);
    }
}
