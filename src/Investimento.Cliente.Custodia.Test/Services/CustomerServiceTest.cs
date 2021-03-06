using Investimento.Cliente.Custodia.Domain.Interfaces.Repositories;
using Investimento.Cliente.Custodia.Domain.Interfaces.Services;
using Investimento.Cliente.Custodia.Domain.Services;
using Investimento.Cliente.Custodia.Test.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Test.Services
{
    [TestClass]
    public class CustomerServiceTest
    {
        private Mock<ITesouroDiretoService> _tesouroDiretoService;
        private Mock<IRendaFixaService> _rendaFixaService;
        private Mock<IFundoService> _fundoService;
        private Mock<ICustomerRepository> _customerRepository;
        private Mock<ICacheService> _cacheService;

        [TestInitialize]
        public void Initialize()
        {
            _tesouroDiretoService = new Mock<ITesouroDiretoService>();
            _rendaFixaService = new Mock<IRendaFixaService>();
            _fundoService = new Mock<IFundoService>();
            _customerRepository = new Mock<ICustomerRepository>();
            _cacheService = new Mock<ICacheService>();
        }

        [TestMethod]
        public async Task ShouldReturnInvesmentsWhenCustomerHasAndTheServicesIsUp()
        {
            _customerRepository.Setup(x => x.GetInvestmentsByAccountIdAsync(It.IsAny<long>())).Returns(ClienteBuilder.CreateWithOnlyTypesInvestment());
            _tesouroDiretoService.Setup(x => x.GetTesourosDiretosByAccountIdAsync(It.IsAny<long>(), It.IsAny<CancellationTokenSource>())).ReturnsAsync(InvestimentoBuilder.CreateTesourosDiretos());
            _rendaFixaService.Setup(x => x.GetRendasFixasByAccountIdAsync(It.IsAny<long>(), It.IsAny<CancellationTokenSource>())).ReturnsAsync(InvestimentoBuilder.CreateRendasFixas());
            _fundoService.Setup(x => x.GetFundosByAccountIdAsync(It.IsAny<long>(), It.IsAny<CancellationTokenSource>())).ReturnsAsync(InvestimentoBuilder.CreateFundos());
            _cacheService.Setup(x => x.SetAsync(It.IsAny<string>(), It.IsAny<Domain.Entities.Cliente>(), It.IsAny<TimeSpan>())).ReturnsAsync("UnitTest");

            var service = new CustomerService(_tesouroDiretoService.Object, _rendaFixaService.Object, _fundoService.Object, _customerRepository.Object, _cacheService.Object);
            var result = await service.GetConsolidatedInvestmentsByAccountIdAsync(123456);

            Assert.IsTrue(result == "UnitTest");
        }

        [TestMethod]
        public async Task ShouldReturnInvesmentsWhenCustomerHasAndTheServicesIsDown()
        {
            _customerRepository.Setup(x => x.GetInvestmentsByAccountIdAsync(It.IsAny<long>())).Returns(ClienteBuilder.CreateWithOnlyTypesInvestment());
            _tesouroDiretoService.Setup(x => x.GetTesourosDiretosByAccountIdAsync(It.IsAny<long>(), It.IsAny<CancellationTokenSource>())).ReturnsAsync((List<Domain.Entities.Investimento>)null);
            _rendaFixaService.Setup(x => x.GetRendasFixasByAccountIdAsync(It.IsAny<long>(), It.IsAny<CancellationTokenSource>())).ReturnsAsync((List<Domain.Entities.Investimento>)null);
            _fundoService.Setup(x => x.GetFundosByAccountIdAsync(It.IsAny<long>(), It.IsAny<CancellationTokenSource>())).ReturnsAsync((List<Domain.Entities.Investimento>)null);
            _cacheService.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync("UnitTest");

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            cancellationTokenSource.Cancel();

            var service = new CustomerService(_tesouroDiretoService.Object, _rendaFixaService.Object, _fundoService.Object, _customerRepository.Object, _cacheService.Object);
            var result = await service.GetConsolidatedInvestmentsByAccountIdAsync(123456, cancellationToken);

            Assert.IsTrue(result == "UnitTest");
        }
    }
}
