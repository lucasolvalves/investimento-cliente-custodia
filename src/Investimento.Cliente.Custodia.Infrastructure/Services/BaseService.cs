using KissLog;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Infrastructure.Services
{
    public abstract class BaseService
    {
        private readonly IHttpClientFactory _httpFactory;
        protected static IConfiguration _configuration;
        protected readonly ILogger _logger;

        public BaseService(IHttpClientFactory httpFactory, ILogger logger, IConfiguration configuration)
        {
            _httpFactory = httpFactory;
            _logger = logger;
            _configuration = configuration;
        }

        protected async Task<string> ConsumeEndpoint(string nomeCliente, string requestUri, CancellationTokenSource cancellationToken = null)
        {
            try
            {
                using (HttpClient httpclient = _httpFactory.CreateClient(nomeCliente))
                using (HttpResponseMessage httpResponse = await httpclient.GetAsync(requestUri))
                {
                    var body = await httpResponse.Content?.ReadAsStringAsync();

                    if (!httpResponse.IsSuccessStatusCode)
                        cancellationToken.Cancel();

                    _logger.Info("RequestUrl: " + httpResponse.RequestMessage.RequestUri?.ToString() +
                                "\nMethod: " + httpResponse.RequestMessage.Method?.ToString() +
                                "\nResponseStatusCode: " + httpResponse?.StatusCode +
                                "\nResponseBody: " + body, "ConsomeEndpoint", 20);

                    return !string.IsNullOrWhiteSpace(body) ? body : null;
                }
            }
            catch (Exception ex)
            {
                cancellationToken.Cancel();
                _logger.Critical("Exception: erro ao realizar uma chamada HTTP:" + ex);
                return string.Empty;
            }

        }
    }
}
