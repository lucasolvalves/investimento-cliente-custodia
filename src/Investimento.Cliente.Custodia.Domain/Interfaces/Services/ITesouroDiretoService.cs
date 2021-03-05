using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Domain.Interfaces.Services
{
    public interface ITesouroDiretoService
    {
        Task<List<Entities.Investimento>> GetTesourosDiretosByAccountIdAsync(long accountId, CancellationTokenSource cancellationTokenSource = null);
    }
}
