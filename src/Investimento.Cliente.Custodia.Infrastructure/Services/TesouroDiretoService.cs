using Investimento.Cliente.Custodia.Domain.Extensions;
using Investimento.Cliente.Custodia.Domain.Interfaces.Services;
using Investimento.Cliente.Custodia.Infrastructure.ViewModels;
using KissLog;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Infrastructure.Services
{
    public class TesouroDiretoService : BaseService, ITesouroDiretoService
    {
        public TesouroDiretoService(IHttpClientFactory httpFactory, ILogger logger, IConfiguration configuration)
                                    : base(httpFactory, logger, configuration) { }

        public async Task<List<Domain.Entities.Investimento>> GetTesourosDiretosByAccountIdAsync(long accountId, CancellationTokenSource cancellationToken = null)
        {
            try
            {
                var listTesourosDiretos = new List<Domain.Entities.Investimento>();
                var jsonString = await ConsumeEndpoint("investimento", string.Format(_configuration.GetSection("AppSettings:InvestimentoServicos:TesouroDireto")?.Value, accountId), cancellationToken);
                var items = jsonString.JsonGetByName("data");

                if (string.IsNullOrEmpty(items))
                {
                    cancellationToken.Cancel();
                    return listTesourosDiretos;
                }

                var tesourosDiretosViewModel = JsonConvert.DeserializeObject<List<TesouroDiretoViewModel>>(items);
                tesourosDiretosViewModel?.ForEach(x => listTesourosDiretos.Add(new Domain.Entities.Investimento(x.Nome, x.ValorInvestido, x.ValorTotal, x.DataDeVencimento, x.Ir, x.ValorResgate)));
                return listTesourosDiretos;
            }
            catch (Exception ex)
            {
                cancellationToken.Cancel();
                _logger.Critical("Exception: erro ao realizar uma chamada HTTP:" + ex);
                return null;
            }
        }
    }
}