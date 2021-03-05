using Investimento.Cliente.Custodia.Domain.Interfaces.Services;
using KissLog;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Infrastructure.Services
{
    public class RedisService : BaseService, ICacheService
    {
        public RedisService(IHttpClientFactory httpFactory, ILogger logger, IConfiguration configuration)
                            : base(httpFactory, logger, configuration) { }

        private static Lazy<ConnectionMultiplexer> _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis"));
        });

        private static ConnectionMultiplexer _redisConnection
        {
            get
            {
                return _connectionMultiplexer.Value;
            }
        }

        public async Task<string> SetAsync<T>(string chave, T data, TimeSpan time)
        {
            try
            {
                IDatabase db = _redisConnection.GetDatabase();

                if (data == null)
                    return null;

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };

                var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings { Formatting = Formatting.None, ContractResolver = contractResolver });

                await db.StringSetAsync(chave, json, time);

                return json;
            }
            catch (Exception ex)
            {
                _logger.Critical("Exception: erro ao inserir dados no redis:" + ex);
                return null;
            }
        }

        public async Task<string> GetAsync(string chave)
        {
            try
            {
                IDatabase db = _redisConnection.GetDatabase();
                var res = await db.StringGetAsync(chave);

                if (res.IsNull)
                    return null;
                else
                {
                    return res;
                }
            }
            catch (Exception ex)
            {
                _logger.Critical("Exception: Error ao buscar dados no redis:" + ex);
                return null;
            }
        }
    }
}
