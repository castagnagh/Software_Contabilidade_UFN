using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniERP_Entity.DataModels
{
    public class Relatorio
    {
        public int RelatorioId { get; set; }
        public decimal? CapitalInicial { get; set; }
        public decimal? Patrimonio { get; set; }
        public decimal? VendasProdutos { get; set; }
        public decimal? ComprasProdutos { get; set; }
        public decimal? ImpostoPagar { get; set; }
        public decimal? ImpostoRecuperar { get; set; }
        public decimal? Caixa { get; set; }

    }
}
