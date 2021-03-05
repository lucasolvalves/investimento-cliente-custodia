using Investimento.Cliente.Custodia.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Investimento.Cliente.Custodia.Domain.Entities
{
    public class Cliente
    {
        private List<Investimento> _investimentos;

        public Cliente(long accountId)
        {
            AccountId = accountId;
            _investimentos = new List<Investimento>();
            TiposInvestimento = new List<ETipoInvestimento>();
        }

        public Cliente(long accountId, List<ETipoInvestimento> tiposInvestimento)
        {
            AccountId = accountId;
            _investimentos = new List<Investimento>();
            TiposInvestimento = tiposInvestimento;
        }

        [JsonIgnore]
        public long AccountId { get; private set; }
        public double ValorTotal { get; private set; }
        public DateTime UltimaAtualizacao { get; private set; }
        [JsonIgnore]
        public IReadOnlyCollection<ETipoInvestimento> TiposInvestimento { get; private set; }
        public IReadOnlyCollection<Investimento> Investimentos { get { return _investimentos; } }

        public void AddInvestimento(Investimento investimento)
        {
            if (investimento == null)
                return;

            _investimentos.Add(investimento);
        }

        public void AddInvestimentos(IEnumerable<Investimento> investimentos)
        {
            if (investimentos == null)
                return;

            _investimentos.AddRange(investimentos);
        }

        public void CalcularTotalInvestimento()
        {
            if (_investimentos.Count == 0)
                ValorTotal = 0;

            ValorTotal = _investimentos.Sum(x => x.ValorTotal);
            UltimaAtualizacao = DateTime.Now;
        }
    }
}