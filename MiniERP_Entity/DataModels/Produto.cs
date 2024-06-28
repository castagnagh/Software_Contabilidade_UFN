using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniERP_Entity.DataModels
{
    public class Produto
    {
        public int Id {  get; set; }
        public string Nome { get; set; }
        public decimal Preco {  get; set; }
        public decimal? PrecoVenda { get; set; }
        public decimal PrecoTotal { get; set; }
        public int QtdEstoque { get; set; }
        public int FornecedorId { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Produto produto && Nome == produto.Nome;
        }
    }
}
