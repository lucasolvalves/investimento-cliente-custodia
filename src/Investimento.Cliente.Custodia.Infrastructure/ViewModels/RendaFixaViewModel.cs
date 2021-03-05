using System;

namespace Investimento.Cliente.Custodia.Infrastructure.ViewModels
{
    public class RendaFixaViewModel
    {
        public double ValorInvestido { get; set; }
		public double ValorTotal { get; set; }
		public double Quantidade { get; set; }
		public DateTime DataDeVencimento { get; set; }
		public double Iof { get; set; }
		public double Ir { get; set; }
		public double ValorResgate { get; set; }
		public double OutrasTaxas { get; set; }
		public double Taxas { get; set; }
		public string Indice { get; set; }
		public string Tipo { get; set; }
		public string Nome { get; set; }
		public bool GuarantidoFGC { get; set; }
		public DateTime DataDeCompra { get; set; }
		public double PrecoUnitario { get; set; }
		public bool Primario { get; set; }
	}
}
