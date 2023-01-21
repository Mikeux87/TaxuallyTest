using System.Text;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Contracts;
using Taxually.TechnicalTest.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        private readonly ILogger<VatRegistrationController> _logger;
        private readonly IVatRegistrationService _service;

        public VatRegistrationController(
            ILogger<VatRegistrationController> logger,
            IVatRegistrationService service) 
        {
            _logger= logger;
            _service= service;
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request)
        {
            _logger.LogInformation($"{this.GetType().Name} -> {nameof(Post)} called.");

            try
            {
                switch (request.Country)
                {
                    case "GB":
                        await _service.VatRegistrationForGB(request);
                        break;
                    case "FR":
                        await _service.VatRegistrationForFR(request);
                        break;
                    case "DE":
                        await _service.VatRegistrationForDE(request);
                        break;
                    default:
                        throw new Exception("Country not supported");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Unexpected exception opccured: {ex.Message}");
                throw;
            }

            _logger.LogInformation($"{this.GetType().Name} -> {nameof(Post)} finished.");

            return Ok();
        }
    }
}
