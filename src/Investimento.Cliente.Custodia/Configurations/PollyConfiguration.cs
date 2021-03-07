using KissLog;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Configurations
{
    public static class PollyConfiguration
    {
        private static ILogger _logger = Logger.Factory.Get();

        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => !msg.IsSuccessStatusCode)
                    .WaitAndRetryAsync(3, retryCount => TimeSpan.FromSeconds(Math.Pow(2, retryCount)),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.Warn($"Polly: Retentativa: {retryCount}");
                    });
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => !msg.IsSuccessStatusCode)
                .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1));
        }

        public static IAsyncPolicy<HttpResponseMessage> GetFallback()
        {
            return Policy<HttpResponseMessage>
                .Handle<BrokenCircuitException>()
                .FallbackAsync(FallbackAction);
        }

        private static Task<HttpResponseMessage> FallbackAction(CancellationToken arg)
        {
            _logger.Warn($"Polly: Fallback");
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}
