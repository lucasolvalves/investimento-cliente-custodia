using System;
using System.Collections.Generic;
using System.Text;

namespace Investimento.Cliente.Custodia.Infrastructure.ViewModels
{
    public class FundoViewModel
    {
        public double ValorInvestido { get; set; }
		public double ValorTotal { get; set; }
		public DateTime DataDeVencimento { get; set; }
		public DateTime DataDeCompra { get; set; }
		public double Iof { get; set; }
		public double Ir { get; set; }
		public double ValorResgate { get; set; }
		public string Nome { get; set; }
		public double TotalTaxas { get; set; }
		public double Quantidade { get; set; }
	}
}
