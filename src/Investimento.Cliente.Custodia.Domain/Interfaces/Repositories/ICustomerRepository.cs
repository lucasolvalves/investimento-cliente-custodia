using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Entities.Cliente GetCustomerByAccountIdAsync(long accountId);
        Entities.Cliente GetInvestmentsByAccountIdAsync(long accountId);      
    }
}
