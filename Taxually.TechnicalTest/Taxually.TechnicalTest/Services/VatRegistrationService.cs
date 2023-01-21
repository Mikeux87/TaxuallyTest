using System.Text;
using System.Xml.Serialization;
using Taxually.TechnicalTest.Contracts;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Interfaces;

namespace Taxually.TechnicalTest.Services
{
    public class VatRegistrationService : IVatRegistrationService
    {
        private readonly ILogger<VatRegistrationService> _logger;
        private readonly TaxuallyHttpClient _httpClient;
        //private readonly ITaxuallyQueueClient _queueClient;


        public VatRegistrationService(
            ILogger<VatRegistrationService> logger,
            TaxuallyHttpClient httpClient
            ) 
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task VatRegistrationForDE(VatRegistrationRequest request)
        {
            // Germany requires an XML document to be uploaded to register for a VAT number
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
                serializer.Serialize(stringwriter, request);
                var xml = stringwriter.ToString();
                var xmlQueueClient = new TaxuallyQueueClient();
                // Queue xml doc to be processed
                await xmlQueueClient.EnqueueAsync("vat-registration-xml", xml);
            }
        }

        public async Task VatRegistrationForFR(VatRegistrationRequest request)
        {
            // France requires an excel spreadsheet to be uploaded to register for a VAT number
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            var excelQueueClient = new TaxuallyQueueClient();
            // Queue file to be processed
            await excelQueueClient.EnqueueAsync("vat-registration-csv", csv);
        }

        public async Task VatRegistrationForGB(VatRegistrationRequest request)
        {
            // UK has an API to register for a VAT number
            //var httpClient = new TaxuallyHttpClient();
            await _httpClient.PostAsync(request);
        }
    }
}
