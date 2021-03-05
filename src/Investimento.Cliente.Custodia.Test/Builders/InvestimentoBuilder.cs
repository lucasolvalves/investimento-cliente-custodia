using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Test.Builders
{
    public static class InvestimentoBuilder
    {
        public static List<Domain.Entities.Investimento> CreateTesourosDiretos()
        {
            var investimentos = new List<Domain.Entities.Investimento>
            {
                new Domain.Entities.Investimento("Tesouro Selic 2025", 799.4720, 829.68, DateTime.Parse("2025-03-01T00:00:00"), 3.0207999999999973, 705.228)
            };

            return investimentos;
        }

        public static List<Domain.Entities.Investimento> CreateRendasFixas()
        {
            var investimentos = new List<Domain.Entities.Investimento>
            {
                new Domain.Entities.Investimento("BANCO MAXIMA", 2000, 2097.85, DateTime.Parse("2021-03-09T00:00:00"), 4.892499999999996, 1971.9789999999998)
            };

            return investimentos;
        }

        public static List<Domain.Entities.Investimento> CreateFundos()
        {
            var investimentos = new List<Domain.Entities.Investimento>
            {
                new Domain.Entities.Investimento("ALASKA", 1000, 1159, DateTime.Parse("2022-10-01T00:00:00"), 23.849999999999998, 985.15)
            };

            return investimentos;
        }
    }
}
