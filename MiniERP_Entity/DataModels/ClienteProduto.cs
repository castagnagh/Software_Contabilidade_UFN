using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniERP_Entity.DataModels
{
    public class ClienteProduto
    {

        public int Id { get; set; }
        public int ProdutoId {  get; set; }
        public int QtdTotal { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }
        public int NotaId {  get; set; }
        public virtual Nota Nota { get; set; }
        public virtual Produto Produto { get; set; }

    }
}
