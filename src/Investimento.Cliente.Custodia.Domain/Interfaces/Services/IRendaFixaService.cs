using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Domain.Interfaces.Services
{
    public interface IRendaFixaService
    {
        Task<List<Entities.Investimento>> GetRendasFixasByAccountIdAsync(long accountId, CancellationTokenSource cancellationToken = null);
    }
}
