using Investimento.Cliente.Custodia.Domain.Interfaces.Repositories;
using Investimento.Cliente.Custodia.Domain.Interfaces.Services;
using KissLog;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/clientes")]

    public class CustomersController : MainController
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ILogger logger, ICustomerService customerService, ICustomerRepository customerRepository) : base(logger)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
        }

        [HttpGet("{accountId:long}/investimentos_consolidados")]
        public async Task<IActionResult> GetConsolidatedInvestments(long accountId)
        {
            if (GetCustomerAsync(accountId) == null)
                return NotFound();

            var clienteInvestimentos = await _customerService.GetConsolidatedInvestmentsByAccountIdAsync(accountId);

            if (clienteInvestimentos == null)
                return NoContent();

            return CustomResponse(clienteInvestimentos);
        }

        private Domain.Entities.Cliente GetCustomerAsync(long cpf)
        {
            return _customerRepository.GetCustomerByAccountIdAsync(cpf);
        }
    }
}
