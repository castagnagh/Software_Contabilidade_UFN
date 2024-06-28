using MiniERP_Entity.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniERP_Entity
{
    public partial class FormBuscarCliente : Form
    {
        Contexto contexto = new Contexto();

        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public FormBuscarCliente()
        {
            InitializeComponent();
            maskedTextBox1.Focus();
            maskedTextBox1.SelectionStart = 0;
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            List<Cliente> listaClientes =
                (from Cliente c in contexto.Clientes select c)
                    .ToList<Cliente>();
            listaClientes.Sort((a, b) => a.Nome.CompareTo(b.Nome));

            listViewClientesBusca.Items.Clear();

            foreach (var c in listaClientes)
            {
                string[] itens = { c.Id.ToString(), c.Nome, c.Cpf };
                listViewClientesBusca.Items.Add(new ListViewItem(itens));
            }
        }

        private void KeyUp_BuscarClientePorCpf(object sender, KeyEventArgs e)
        {
            try
            {
                string cpf = maskedTextBox1.Text;
                List<Cliente> listaBuscaCliente = new List<Cliente>();
                listaBuscaCliente.Clear();
                listaBuscaCliente = contexto.Clientes
                    .Where(c => c.Cpf.Contains(cpf))
                    .ToList();
                listaBuscaCliente.Sort((a, b) => a.Nome.CompareTo(b.Nome));

                listViewClientesBusca.Items.Clear();
                foreach (var c in listaBuscaCliente)
                {
                    string[] itens = { c.Id.ToString(), c.Nome, c.Cpf };
                    listViewClientesBusca.Items.Add(new ListViewItem(itens));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message);
            }
        }

        private void maskedTextBox1_Click(object sender, EventArgs e)
        {
            maskedTextBox1.SelectionStart = 0;
        }

        private void buttonSelecionarCliente_Click(object sender, EventArgs e)
        {
            if (listViewClientesBusca.SelectedItems.Count > 0)
            {
                ListView.SelectedListViewItemCollection linha = this.listViewClientesBusca.SelectedItems;

                foreach (ListViewItem item in linha)
                {
                    IdCliente = int.Parse(item.SubItems[0].Text);
                    Nome = item.SubItems[1].Text;
                }

                this.Close();
            }
        }
    }
}
