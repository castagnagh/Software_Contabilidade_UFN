using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniERP_Entity.DataModels
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj {  get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Fornecedor fornecedor && Cnpj == fornecedor.Cnpj;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cnpj);
        }
    }
}
