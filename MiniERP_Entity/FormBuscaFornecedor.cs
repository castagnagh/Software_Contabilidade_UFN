using MiniERP_Entity.DataModels;
using System;
using System.Collections;
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
    public partial class FormBuscaFornecedor : Form
    {
        Contexto contexto = new Contexto();
        DataTable dt = new DataTable();
        private int _idFornecedor;
        private string _razaoSocial;

        public int IdFornecedor { get; private set; }
        public string RazaoSocial { get; private set; }

        public FormBuscaFornecedor()
        {
            InitializeComponent();
            AtualizarLista();

        }

        private void buttonSelecionarFornecedor_Click(object sender, EventArgs e)
        {
            if (listViewFornecedoresBusca.SelectedItems.Count > 0)
            {
                ListView.SelectedListViewItemCollection linha = this.listViewFornecedoresBusca.SelectedItems;

                foreach (ListViewItem item in linha)
                {
                    IdFornecedor = int.Parse(item.SubItems[0].Text);
                    RazaoSocial = item.SubItems[1].Text;
                }

                this.Close();
            }
        }

        private void buscaFormFornecedores(object sender, KeyEventArgs e)
        {
            try
            {
                string cnpj = maskedTextBoxBusca.Text;
                List<Fornecedor> listaBuscaFornecedor = new List<Fornecedor>();
                listaBuscaFornecedor.Clear();
                listaBuscaFornecedor = contexto.Fornecedores
                    .Where(f => f.Cnpj.Contains(cnpj))
                    .ToList();

                listaBuscaFornecedor.Sort((a, b) => a.RazaoSocial.CompareTo(b.RazaoSocial));

                listViewFornecedoresBusca.Items.Clear();
                foreach (var f in listaBuscaFornecedor)
                {
                    string[] itens = { f.Id.ToString(), f.RazaoSocial, f.Cnpj };
                    listViewFornecedoresBusca.Items.Add(new ListViewItem(itens));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro");
            }
        }

        private void AtualizarLista()
        {
            List<Fornecedor> listaFornecedores =
                (from Fornecedor f in contexto.Fornecedores select f)
                    .ToList<Fornecedor>();
            listaFornecedores.Sort((a, b) => a.RazaoSocial.CompareTo(b.RazaoSocial));

            listViewFornecedoresBusca.Items.Clear();

            foreach (var f in listaFornecedores)
            {
                string[] itens = { f.Id.ToString(), f.RazaoSocial, f.Cnpj };
                listViewFornecedoresBusca.Items.Add(new ListViewItem(itens));
            }
        }
    }
}
