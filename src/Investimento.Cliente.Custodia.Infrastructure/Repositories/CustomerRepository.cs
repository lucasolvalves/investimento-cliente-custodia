using Investimento.Cliente.Custodia.Domain.Enums;
using Investimento.Cliente.Custodia.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace Investimento.Cliente.Custodia.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Domain.Entities.Cliente GetCustomerByAccountIdAsync(long accountId)
        {
            if (accountId == 123456)
                return new Domain.Entities.Cliente(123456);
            else
                return null;
        }

        public Domain.Entities.Cliente GetInvestmentsByAccountIdAsync(long accountId)
        {
            if (accountId == 123456)
                return new Domain.Entities.Cliente(123456, new List<ETipoInvestimento>() { ETipoInvestimento.TD, ETipoInvestimento.RD, ETipoInvestimento.FD });
            else
                return new Domain.Entities.Cliente(123456);
        }
    }
}
