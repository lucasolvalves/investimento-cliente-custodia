using Investimento.Cliente.Custodia.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Test.Builders
{
    public static class ClienteBuilder
    {
        public static Domain.Entities.Cliente Create()
        {
            var investimentos = new List<Domain.Entities.Investimento>
            {
                new Domain.Entities.Investimento("Tesouro Selic 2025", 799.4720, 829.68, DateTime.Parse("2025-03-01T00:00:00"), 3.0207999999999973, 705.228),
                new Domain.Entities.Investimento("BANCO MAXIMA", 2000, 2097.85, DateTime.Parse("2021-03-09T00:00:00"), 4.892499999999996, 1971.9789999999998),
                new Domain.Entities.Investimento("ALASKA", 1000, 1159, DateTime.Parse("2022-10-01T00:00:00"), 23.849999999999998, 985.15)
            };


            var cliente = new Domain.Entities.Cliente(123456, new List<ETipoInvestimento> { ETipoInvestimento.FD, ETipoInvestimento.RD, ETipoInvestimento.TD });
            cliente.AddInvestimentos(investimentos);

            return cliente;
        }

        public static Domain.Entities.Cliente CreateWithoutInvestment()
        {
            return new Domain.Entities.Cliente(123456, new List<ETipoInvestimento> { ETipoInvestimento.None });
        }

        public static Domain.Entities.Cliente CreateWithOnlyTypesInvestment()
        {
            return new Domain.Entities.Cliente(123456, new List<ETipoInvestimento> { ETipoInvestimento.FD, ETipoInvestimento.RD, ETipoInvestimento.TD });
        }
    }
}
