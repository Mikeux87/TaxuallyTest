using Moq;
using Microsoft.Extensions.Logging;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace UnitTest.ControllerTests
{
    public class VatRegistrationControllerTest
    {

        private Mock<ILogger<VatRegistrationController>> _loggerMock;
        private Mock<IVatRegistrationService> _serviceMock;
        public VatRegistrationController _controller;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<VatRegistrationController>>();
            _serviceMock= new Mock<IVatRegistrationService>();
            _controller = new VatRegistrationController(_loggerMock.Object, _serviceMock.Object);
        }

        [Test]
        [TestCase("GB")]
        [TestCase("FR")]
        [TestCase("DE")]
        public async Task VatRegistrationForGB_HappyCase(string country)
        {
            VatRegistrationRequest request = new()
            {
                Country = country
            };

            var response = await _controller.Post(request);

            Assert.That(response, Is.Not.Null);
            var actionResult = response as OkResult;
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

            Assert.Pass();
        }

        [Test]
        [TestCase("GB")]
        public void VatRegistrationForGB_ServiceThrowException(string country)
        {
            VatRegistrationRequest request = new()
            {
                Country = country
            };

            _serviceMock.Setup(x => x.VatRegistrationForGB(It.IsAny<VatRegistrationRequest>())).Throws<Exception>();

            Assert.ThrowsAsync<Exception>(() => _controller.Post(request));

            Assert.Pass();
        }

        [Test]
        [TestCase("USA")]
        public void VatRegistrationForGB_NotSupportedCountry(string country)
        {
            VatRegistrationRequest request = new()
            {
                Country = country
            };

            _serviceMock.Setup(x => x.VatRegistrationForGB(It.IsAny<VatRegistrationRequest>())).Throws<Exception>();

            Assert.ThrowsAsync<Exception>(() => _controller.Post(request));

            Assert.Pass();
        }
    }
}
