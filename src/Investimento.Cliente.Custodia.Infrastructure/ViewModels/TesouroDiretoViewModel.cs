using System;

namespace Investimento.Cliente.Custodia.Infrastructure.ViewModels
{
    public class TesouroDiretoViewModel
	{
        public string Nome { get; set; }
        public double ValorInvestido { get; set; }
        public double ValorTotal { get; set; }
        public DateTime DataDeVencimento { get; set; }
        public DateTime DataDeCompra { get; set; }
        public double Iof { get; set; }
        public double Ir { get; set; }
        public double ValorResgate { get; set; }
        public string Indice { get; set; }
        public string Tipo { get; set; }
    }
}
