using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Domain.Interfaces.Services
{
    public interface IFundoService
    {
        Task<List<Entities.Investimento>> GetFundosByAccountIdAsync(long accountId, CancellationTokenSource cancellationTokenSource);
    }
}