using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<string> GetConsolidatedInvestmentsByAccountIdAsync(long accountId, CancellationToken cancellationToken = default);
    }
}
