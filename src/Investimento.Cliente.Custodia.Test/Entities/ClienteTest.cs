using Investimento.Cliente.Custodia.Domain.Enums;
using Investimento.Cliente.Custodia.Test.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Test.Entities
{
    [TestClass]
    public class ClienteTest
    {
        [TestMethod]
        public void WhenCustomerHasInvesmentsTheValorTotalNeedToBeMoreThanZero()
        {
            var cliente = ClienteBuilder.Create();
            cliente.CalcularTotalInvestimento();

            Assert.IsTrue(cliente.UltimaAtualizacao < DateTime.Now);
            Assert.IsTrue(cliente.ValorTotal > 0);
        }

        [TestMethod]
        public void WhenCustomerHasNoInvesmentsTheValorTotalNeedToBeZero()
        {
            var cliente = ClienteBuilder.CreateWithoutInvestment();
            cliente.CalcularTotalInvestimento();

            Assert.IsTrue(cliente.UltimaAtualizacao < DateTime.Now);
            Assert.IsTrue(cliente.ValorTotal == 0);
        }
    }
}
