using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxually.TechnicalTest;
using Taxually.TechnicalTest.Contracts;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Services;

namespace UnitTest.ServiceTests
{
    public class VatRegistrationServiceTest
    {
        private Mock<ILogger<VatRegistrationService>> _loggerMock;
        private Mock<TaxuallyHttpClient> _httpClientMock;
        public VatRegistrationService _service;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<VatRegistrationService>>();
            _httpClientMock = new Mock<TaxuallyHttpClient>();
            _service = new VatRegistrationService(_loggerMock.Object, _httpClientMock.Object);
        }

        [Test]
        public void VatRegistrationForDE_HappyCase()
        {
            VatRegistrationRequest request = new()
            {
                Country = "DE"
            };

            //await _service.VatRegistrationForDE(request);
            Assert.DoesNotThrowAsync(() => _service.VatRegistrationForDE(request));

            Assert.Pass();
        }

        [Test]
        public void VatRegistrationForGB_HappyCase()
        {
            VatRegistrationRequest request = new()
            {
                Country = "GB"
            };

            //await _service.VatRegistrationForGB(request);
            Assert.DoesNotThrowAsync(() => _service.VatRegistrationForGB(request));

            Assert.Pass();
        }

        [Test]
        public void VatRegistrationForFR_HappyCase()
        {
            VatRegistrationRequest request = new()
            {
                Country = "FR"
            };

            //await _service.VatRegistrationForFR(request);
            Assert.DoesNotThrowAsync(() => _service.VatRegistrationForFR(request));

            Assert.Pass();
        }
    }
}
