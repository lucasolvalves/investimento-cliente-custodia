using Investimento.Cliente.Custodia.Domain.Interfaces.Repositories;
using Investimento.Cliente.Custodia.Domain.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ITesouroDiretoService _tesouroDiretoService;
        private readonly IRendaFixaService _rendaFixaService;
        private readonly IFundoService _fundoService;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICacheService _cacheService;
        private CancellationTokenSource _cancellationTokenSource;

        public CustomerService(ITesouroDiretoService tesouroDiretoService, IRendaFixaService rendaFixaService, IFundoService fundoService,
                               ICustomerRepository customerRepository, ICacheService cacheService)
        {
            _tesouroDiretoService = tesouroDiretoService;
            _rendaFixaService = rendaFixaService;
            _fundoService = fundoService;
            _customerRepository = customerRepository;
            _cancellationTokenSource = new CancellationTokenSource();
            _cacheService = cacheService;
        }

        public async Task<string> GetConsolidatedInvestmentsByAccountIdAsync(long accountId, CancellationToken? cancellationToken = null)
        {
            var cancelToken = CheckIfTokenIsNull(cancellationToken);

            var clientInvestments = await GetClientNewsInvestmentsAsync(accountId, _cancellationTokenSource);

            if (!cancelToken.IsCancellationRequested)
                return await _cacheService.SetAsync(accountId.ToString(), clientInvestments, TimeSpan.FromHours(12));

            return await _cacheService.GetAsync(accountId.ToString());
        }

        private async Task<Entities.Cliente> GetClientNewsInvestmentsAsync(long accountId, CancellationTokenSource cancellationTokenSource = null)
        {
            var cliente = _customerRepository.GetInvestmentsByAccountIdAsync(accountId);

            //Está partindo do pressuposto que os serviços já são otimizados com estrategia de cache
            foreach (var tipoInvestimento in cliente.TiposInvestimento)
            {
                switch (tipoInvestimento)
                {
                    case Enums.ETipoInvestimento.TD:
                        cliente.AddInvestimentos(await _tesouroDiretoService.GetTesourosDiretosByAccountIdAsync(accountId, cancellationTokenSource));
                        break;
                    case Enums.ETipoInvestimento.RD:
                        cliente.AddInvestimentos(await _rendaFixaService.GetRendasFixasByAccountIdAsync(accountId, cancellationTokenSource));
                        break;
                    case Enums.ETipoInvestimento.FD:
                        cliente.AddInvestimentos(await _fundoService.GetFundosByAccountIdAsync(accountId, cancellationTokenSource));
                        break;
                    default:
                        break;
                }
            }

            cliente.CalcularTotalInvestimento();
            return cliente;
        }

        private CancellationToken CheckIfTokenIsNull(CancellationToken? cancellationToken)
        {
            if (cancellationToken == null)
                return _cancellationTokenSource.Token;

            return cancellationToken.Value;
        }
    }
}