using Newtonsoft.Json;
using System;

namespace Investimento.Cliente.Custodia.Domain.Entities
{
    public class Investimento
    {
        public Investimento(string nome, double valorInvestido, double valorTotal, DateTime dataDeVencimento, double ir, double valorResgate)
        {
            Nome = nome;
            ValorInvestido = valorInvestido;
            ValorTotal = valorTotal;
            DataDeVencimento = dataDeVencimento;
            IR = ir;
            ValorResgate = valorResgate;
        }

        public string Nome { get; private set; }
        public double ValorInvestido { get; private set; }
        public double ValorTotal { get; private set; }
        [JsonProperty("vencimento")]
        public DateTime DataDeVencimento { get; private set; }
        public double IR { get; private set; }
        public double ValorResgate { get; private set; }
    }
}