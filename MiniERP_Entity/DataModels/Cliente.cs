using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MiniERP_Entity.DataModels
{
    public class Cliente
    {
        public int Id {  get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public virtual ICollection<Nota> Notas { get; set; }
        
        
        public override bool Equals(object? obj)
        {
            return obj is Cliente cliente && Cpf == cliente.Cpf;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cpf);
        }
    }
}
