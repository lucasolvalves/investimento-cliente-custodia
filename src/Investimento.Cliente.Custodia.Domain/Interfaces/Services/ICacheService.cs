using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Domain.Interfaces.Services
{
    public interface ICacheService
    {
        Task<string> SetAsync<T>(string chave, T data, TimeSpan time);
        Task<string> GetAsync(string chave);
    }
}
