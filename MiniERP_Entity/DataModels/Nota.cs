using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniERP_Entity.DataModels
{
    public class Nota
    {
        public int Id {  get; set; }
        public decimal PrecoTotalCompra { get; set; }
        public DateTime Data { get; set; }
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ClienteProduto> clienteProdutos { get; set; }
    }
}
