using Castle.Core.Internal;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using MiniERP_Entity.DataModels;

namespace MiniERP_Entity
{
    public partial class Form1 : Form
    {
        Contexto contexto = new Contexto();
        List<ClienteProduto> listaCarrinho = new List<ClienteProduto>();
        List<ClienteProduto> listaItensDaNota = new List<ClienteProduto>();
        public Form1()
        {
            InitializeComponent();
            AtualizaListaClientes();
            AtualizaListaFornecedores();
            AtualizaListaProdutos();
            AtualizaListaProdutosPatrimonio();
            AtualizaListaItensLoja();
            AtualizarRelatorio();
            buscarRelatorio();

            listViewItensLoja.Enabled = false;
            buttonSelecionarItemLoja.Enabled = false;
            buttonPesquisaClienteLoja.Enabled = true;
            buttonRemoverNomePesquisaAreaitens.Visible = false;
            buttonAddQtdItemLoja.Enabled = false;
            textBoxInformeQtd.Enabled = false;
            textBoxPrecoProdutoVenda.Visible = false;
            labelPrecoProdutoVenda.Visible = false;
        }

        //CÓDIGO RELACIONADO A TAB CONTROL "GERENCIAR CLIENTE"
        private void buttonCadastrarCliente_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            try
            {
                //verifica se não existe nenhum campo null
                if (String.IsNullOrEmpty(textBoxNomeCliente.Text) || String.IsNullOrEmpty(textBoxCpfCliente.Text))
                {
                    MessageBox.Show("Não pode haver campo vazio, por favor preencha-os", "Aviso");
                }
                else
                {
                    //verifica se o cpf já esta cadastrado
                    if (VerificaCpfCliente(textBoxCpfCliente.Text))
                    {
                        MessageBox.Show("Esse CPF já está cadastrado!", "Alerta");
                        textBoxCpfCliente.Text = String.Empty;
                    }
                    else
                    {
                        cliente.Nome = textBoxNomeCliente.Text;
                        cliente.Cpf = textBoxCpfCliente.Text;
                        contexto.Clientes.Add(cliente);
                        contexto.SaveChanges();
                        textBoxNomeCliente.Text = String.Empty;
                        textBoxCpfCliente.Text = String.Empty;
                        MessageBox.Show("Cliente cadastrado com sucesso!", "Aviso");
                        AtualizaListaClientes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Cadastrar" + ex.Message, "Aviso");
            }
        }

        private bool VerificaCpfCliente(string cpf)
        {
            // Verifica se há algum cliente com o mesmo CPF no contexto
            return contexto.Clientes.Any(c => c.Cpf == cpf);
        }

        private void AtualizaListaClientes()
        {
            try
            {
                //LINQ
                //faz a consulta e grava em uma lista
                List<Cliente> listaCli =
                    (from Cliente c in contexto.Clientes select c)
                    .ToList<Cliente>();
                //ordena pelo nome
                listaCli.Sort((a, b) => a.Nome.CompareTo(b.Nome));
                //limpar a lista para popular novamente ordenada
                listViewClientes.Items.Clear();
                //foreach para percorrer a lista
                foreach (var c in listaCli)
                {
                    string[] itens = { c.Nome, c.Cpf };
                    listViewClientes.Items.Add(new ListViewItem(itens));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Cadastrar" + ex.Message, "Aviso");
            }
        }

        //Evento KeyUp da busca pelo nome do cliente - toda vez que o evento for acionado esse metodo é executado
        private void KeyUpBuscarCliente(object sender, KeyEventArgs e)
        {
            //ele armazena o conteudo digitado
            string nome = textBoxBuscaCliente.Text;
            try
            {
                //Cria uma lista
                List<Cliente> listaBusca = new List<Cliente>();

                //limpa a lista antes, de jogar todos os dados pra listaBusca para sempre atualizar as buscas de acordo com o digitado
                listaBusca.Clear();

                // Consulta LINQ - adiciona na lista conforme o texto no textBoxBuscaCliente
                listaBusca = contexto.Clientes
                    .Where(c => c.Nome.Contains(nome))
                    .ToList<Cliente>();

                //Ordena pelo nome
                listaBusca.Sort((a, b) => a.Nome.CompareTo(b.Nome));

                // Atualiza a ListView com os resultados
                listViewClientes.Items.Clear();

                // Adiciona os clientes à ListView
                foreach (var cliente in listaBusca)
                {
                    string[] itens = { cliente.Nome, cliente.Cpf };
                    listViewClientes.Items.Add(new ListViewItem(itens));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar clientes: " + ex.Message, "Aviso");
            }
        }
        //FINAL DO CÓDIGO RELACIONADO A TAB CONTROL "GERENCIAR CLIENTE"

        //INICIO DO CÓDIGO RELACIONADO A TAB CONTROL "GERENCIAR FORNECEDORES"
        private void buttonCadastrarFornecedor_Click(object sender, EventArgs e)
        {
            try
            {
                Fornecedor fornecedor = new Fornecedor();
                if (String.IsNullOrEmpty(textBoxRazaoSocialFornecedor.Text) || String.IsNullOrEmpty(maskedTextBoxCnpjFornecedor.Text))
                {
                    MessageBox.Show("Não pode haver campo vazio, por favor preencha-os", "Aviso");
                }
                else
                {
                    if (VerificaCnpjFornecedor(maskedTextBoxCnpjFornecedor.Text))
                    {
                        MessageBox.Show("Esse CPF já está cadastrado!", "Alerta");
                        maskedTextBoxCnpjFornecedor.Text = String.Empty;
                    }
                    else
                    {
                        fornecedor.RazaoSocial = textBoxRazaoSocialFornecedor.Text;
                        fornecedor.Cnpj = maskedTextBoxCnpjFornecedor.Text;
                        contexto.Fornecedores.Add(fornecedor);
                        contexto.SaveChanges();
                        MessageBox.Show("Fornecedor cadastrado com sucesso!", "Aviso");
                        textBoxRazaoSocialFornecedor.Text = String.Empty;
                        maskedTextBoxCnpjFornecedor.Text = String.Empty;
                        AtualizaListaFornecedores();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro: " + ex.Message, "Erro");
            }
        }

        private bool VerificaCnpjFornecedor(string cnpj)
        {
            return contexto.Fornecedores.Any(f => f.Cnpj == cnpj);
        }

        public void AtualizaListaFornecedores()
        {
            List<Fornecedor> listaFornecedores =
                (from Fornecedor f in contexto.Fornecedores select f)
                    .ToList<Fornecedor>();
            listaFornecedores.Sort((a, b) => a.RazaoSocial.CompareTo(b.RazaoSocial));

            listViewFornecedores.Items.Clear();

            foreach (var f in listaFornecedores)
            {
                string[] itens = { f.RazaoSocial, f.Cnpj };
                listViewFornecedores.Items.Add(new ListViewItem(itens));
            }
        }

        private void KeyUpBuscarFornecedorCnpj(object sender, KeyEventArgs e)
        {
            try
            {
                string cnpj = textBoxBuscaCnpjFornecedor.Text;
                List<Fornecedor> listaBuscaFornecedor = new List<Fornecedor>();
                listaBuscaFornecedor.Clear();
                listaBuscaFornecedor = contexto.Fornecedores
                    .Where(f => f.Cnpj.Contains(cnpj))
                    .ToList();

                listaBuscaFornecedor.Sort((a, b) => a.RazaoSocial.CompareTo(b.RazaoSocial));

                listViewFornecedores.Items.Clear();
                foreach (var f in listaBuscaFornecedor)
                {
                    string[] itens = { f.RazaoSocial, f.Cnpj };
                    listViewFornecedores.Items.Add(new ListViewItem(itens));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro");
            }
        }
        //FIM DO CÓDIGO RELACIONADO A TAB CONTROL "GERENCIAR FORNECEDORES"
        private void buttonPesquisarForneceProduto_Click(object sender, EventArgs e)
        {
            FormBuscaFornecedor bf = new FormBuscaFornecedor();
            bf.ShowDialog();
            textBoxIdForneceProduto.Text = bf.IdFornecedor.ToString();
            textBoxNomeForneceProduto.Text = bf.RazaoSocial;
        }
        private (decimal? patrimonio, decimal? caixa, decimal? comprasProdutos, decimal? impostoRecuperar) BuscarUltimosValoresRelatorio(Contexto contexto)
        {
            var ultimoPatrimonio = contexto.Relatorios
                .Where(r => r.Patrimonio.HasValue)
                .OrderByDescending(r => r.RelatorioId)
                .Select(r => r.Patrimonio)
                .FirstOrDefault();

            var ultimoCaixa = contexto.Relatorios
                .Where(r => r.Caixa.HasValue)
                .OrderByDescending(r => r.RelatorioId)
                .Select(r => r.Caixa)
                .FirstOrDefault();

            var ultimasComprasProdutos = contexto.Relatorios
                .Where(r => r.ComprasProdutos.HasValue)
                .OrderByDescending(r => r.RelatorioId)
                .Select(r => r.ComprasProdutos)
                .FirstOrDefault();

            var ultimoImpostoRecuperar = contexto.Relatorios
                .Where(r => r.ImpostoRecuperar.HasValue)
                .OrderByDescending(r => r.RelatorioId)
                .Select(r => r.ImpostoRecuperar)
                .FirstOrDefault();

            return (ultimoPatrimonio, ultimoCaixa, ultimasComprasProdutos, ultimoImpostoRecuperar);
        }


        private void buttonCadastrarProduto_Click(object sender, EventArgs e)
        {
            try
            {
                Produto p = new Produto();
                Relatorio r = new Relatorio();
                if (String.IsNullOrEmpty(textBoxDescricaoProduto.Text) ||
                    String.IsNullOrEmpty(textBoxPrecoProduto.Text) ||
                    String.IsNullOrEmpty(textBoxEstoqueProduto.Text) ||
                    String.IsNullOrEmpty(textBoxIdForneceProduto.Text) ||
                    String.IsNullOrEmpty(textBoxNomeForneceProduto.Text))
                {
                    MessageBox.Show("Não pode haver campo vazio, por favor preencha-os", "Aviso");
                }
                else
                {
                    p.Nome = textBoxDescricaoProduto.Text;
                    p.Preco = decimal.Parse(textBoxPrecoProduto.Text);

                    //consulta e faz a soma de quanto já gastei comprando produtos pra poder adicionar no relatorio depois
                    //decimal valorTotalProdutosCompradosBancoDados = contexto.Relatorios.Sum(r => r.ComprasProdutos.HasValue ? (decimal)r.ComprasProdutos.Value : 0M);

                    p.QtdEstoque = int.Parse(textBoxEstoqueProduto.Text);
                    p.FornecedorId = int.Parse(textBoxIdForneceProduto.Text);

                    var (ultimoPatrimonio, ultimoCaixa, ultimasComprasProdutos, ultimoImpostoRecuperar) = BuscarUltimosValoresRelatorio(contexto);

                    // Subtrair o valor do produto do caixa
                    var novoCaixa = (ultimoCaixa ?? 0) - (decimal.Parse(textBoxPrecoProduto.Text) * int.Parse(textBoxEstoqueProduto.Text));

                    // Atualizar os valores no relatório
                    r.Caixa = novoCaixa;

                    if (radioButtonRevenda.Checked)
                    {
                        var totalCompra = decimal.Parse(textBoxPrecoProduto.Text) * int.Parse(textBoxEstoqueProduto.Text);
                        p.PrecoVenda = decimal.Parse(textBoxPrecoProdutoVenda.Text);
                        //calcula o imposto a recuperar 17% para salvar no banco!
                        var novoComprasProdutos = (ultimasComprasProdutos ?? 0) + (decimal.Parse(textBoxPrecoProduto.Text) * int.Parse(textBoxEstoqueProduto.Text));
                        var novoImpostoRecuperar = (ultimasComprasProdutos ?? 0) + (decimal.Round(totalCompra * 0.17M, 2));
                        r.ComprasProdutos = novoComprasProdutos;
                        r.ImpostoRecuperar = novoImpostoRecuperar;
                    }
                    else if (radioButtonConsumo.Checked)
                    {
                        // Adicionar o valor do produto ao patrimônio
                        var novoPatrimonio = (ultimoPatrimonio ?? 0) + (decimal.Parse(textBoxPrecoProduto.Text) * int.Parse(textBoxEstoqueProduto.Text));
                        p.PrecoVenda = 0.00M;
                        r.Patrimonio = novoPatrimonio;
                    }
                    else
                    {
                        MessageBox.Show("O produto é para revenda ou patrimônio?", "Aviso");
                    }

                    contexto.Produtos.Add(p);
                    contexto.Relatorios.Add(r);
                    contexto.SaveChanges();

                    textBoxPrecoProdutoVenda.Text = String.Empty;
                    textBoxDescricaoProduto.Text = String.Empty;
                    textBoxPrecoProduto.Text = String.Empty;
                    textBoxEstoqueProduto.Text = String.Empty;
                    textBoxIdForneceProduto.Text = String.Empty;
                    textBoxNomeForneceProduto.Text = String.Empty;
                    textBoxValorTotalCompra.Text = String.Empty;
                    labelIcmsRecuperar.Text = "R$0";
                    radioButtonConsumo.Checked = true;
                    MessageBox.Show("Produto cadastrado com sucesso", "Aviso");
                    AtualizaListaProdutos();
                    AtualizaListaProdutosPatrimonio();
                    AtualizaListaItensLoja();
                    buscarRelatorio();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocoreu um erro: " + ex.Message, "Erro");
            }
        }


        private void AtualizaListaProdutos()
        {
            List<Produto> listaProdutos = new List<Produto>();

            listaProdutos =
                (from Produto p in contexto.Produtos
                 where p.PrecoVenda != 0.00M
                 select p)
                    .ToList<Produto>();
            listaProdutos.Sort((a, b) => a.Nome.CompareTo(b.Nome));

            listViewProdutos.Items.Clear();

            foreach (var produto in listaProdutos)
            {
                string[] itens = { produto.Nome, produto.Preco.ToString(), produto.PrecoVenda.ToString(), produto.QtdEstoque.ToString(), produto.Fornecedor.RazaoSocial.ToString() };
                listViewProdutos.Items.Add(new ListViewItem(itens));
            }
        }

        private void AtualizaListaProdutosPatrimonio()
        {
            List<Produto> listaProdutosPatrimonio = new List<Produto>();

            listaProdutosPatrimonio =
                (from Produto p in contexto.Produtos
                 where p.PrecoVenda == 0.00M
                 select p)
                    .ToList<Produto>();
            listaProdutosPatrimonio.Sort((a, b) => a.Nome.CompareTo(b.Nome));

            listViewPatrimonios.Items.Clear();

            foreach (var produto in listaProdutosPatrimonio)
            {
                string[] itensPatrimonio = { produto.Nome, produto.Preco.ToString(), produto.QtdEstoque.ToString(), produto.Fornecedor.RazaoSocial.ToString() };
                listViewPatrimonios.Items.Add(new ListViewItem(itensPatrimonio));
            }
        }

        private void buscarProdutoKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string produto = textBoxBuscarProduto.Text;
                List<Produto> listaBuscaProduto = new List<Produto>();
                listaBuscaProduto.Clear();


                listaBuscaProduto = contexto.Produtos
                    .Where(p => p.Nome.Contains(produto))
                    .ToList<Produto>();


                listaBuscaProduto.Sort((a, b) => a.Nome.CompareTo(b.Nome));
                listViewProdutos.Items.Clear();

                foreach (var p in listaBuscaProduto)
                {
                    string[] itens = { p.Nome, p.Preco.ToString(), p.QtdEstoque.ToString(), p.Fornecedor.RazaoSocial };
                    listViewProdutos.Items.Add(new ListViewItem(itens));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message);
            }
        }

        private void buttonPesquisaClienteLoja_Click(object sender, EventArgs e)
        {
            listaCarrinho.Clear();
            FormBuscarCliente bc = new FormBuscarCliente();
            bc.ShowDialog();
            int idClienteSelecionado = bc.IdCliente;
            string nomeSelecionado = bc.Nome;

            if (String.IsNullOrEmpty(idClienteSelecionado.ToString()) || String.IsNullOrEmpty(nomeSelecionado))
            {
                MessageBox.Show("Você não selecionou nenhum cliente!", "Aviso");
            }
            else
            {
                textBoxIdClienteHiddenLoja.Text = idClienteSelecionado.ToString();
                textBoxPesquisaClienteLoja.Text = nomeSelecionado.ToString();
                listViewItensLoja.Enabled = true;
                buttonSelecionarItemLoja.Enabled = true;
                buttonPesquisaClienteLoja.Enabled = false;
                buttonRemoverNomePesquisaAreaitens.Visible = true;
                buttonRemoverNomePesquisaAreaitens.Enabled = true;
                buttonAddQtdItemLoja.Enabled = true;
                textBoxInformeQtd.Enabled = true;
            }

        }

        private void AtualizaListaItensLoja()
        {
            List<Produto> listaProdutos = new List<Produto>();

            listaProdutos =
                (from Produto p in contexto.Produtos
                 where p.PrecoVenda != 0.00M
                 select p)
                    .ToList<Produto>();
            listaProdutos.Sort((a, b) => a.Nome.CompareTo(b.Nome));

            listViewItensLoja.Items.Clear();

            foreach (var produto in listaProdutos)
            {
                string precoVenda = produto.PrecoVenda.HasValue
                    ? produto.PrecoVenda.Value.ToString("F2")
                    : "0.00";
                string[] itens = { produto.Id.ToString(), produto.Nome, precoVenda, produto.QtdEstoque.ToString(), produto.Fornecedor.RazaoSocial.ToString() };
                listViewItensLoja.Items.Add(new ListViewItem(itens));
            }





        }

        private void buttonSelecionarItemLoja_Click(object sender, EventArgs e)
        {
            ClienteProduto cp = new ClienteProduto();
            if (listViewItensLoja.SelectedItems.Count > 0)
            {
                ListView.SelectedListViewItemCollection linha = this.listViewItensLoja.SelectedItems;

                foreach (ListViewItem item in linha)
                {
                    //pega o Id do produto na ListView na tab Loja
                    int idProduto = int.Parse(item.SubItems[0].Text);
                    //Pega o nome do produto na coluna de indice 1 da listView na tab Loja
                    string nomeProduto = item.SubItems[1].Text;
                    decimal precoProduto = decimal.Parse(item.SubItems[2].Text);
                    cp.ProdutoId = idProduto;
                    cp.Produto = new Produto { Nome = nomeProduto };
                    cp.PrecoUnitario = precoProduto;
                    cp.QtdTotal = 1;
                    cp.PrecoTotal = 1 * precoProduto;
                    listaCarrinho.Add(cp);
                    AtualizarListaItens();
                }
            }
            else
            {
                MessageBox.Show("Selecione um item", "Aviso");
            }
        }

        private void AtualizarListaItens()
        {
            decimal valorTotalItem = 0;
            decimal valorTotalCompra = 0;

            listViewCarrinhoCompraLoja.Items.Clear();

            foreach (var i in listaCarrinho)
            {
                int index = listaCarrinho.IndexOf(i);
                valorTotalItem = i.PrecoUnitario * i.QtdTotal;

                string[] itens = { index.ToString(), i.Produto.Nome, i.PrecoUnitario.ToString("F2"), i.QtdTotal.ToString(), valorTotalItem.ToString("F2") };
                listViewCarrinhoCompraLoja.Items.Add(new ListViewItem(itens));
                valorTotalCompra += valorTotalItem;
                labelPrecoTotal.Text = valorTotalCompra.ToString("F2");
            }
        }

        private void buttonRemoverNomePesquisaAreaitens_Click(object sender, EventArgs e)
        {
            buttonCancelarCompra_Click(sender, e);
            textBoxIdClienteHiddenLoja.Text = String.Empty;
            textBoxPesquisaClienteLoja.Text = String.Empty;
            listViewItensLoja.Enabled = false;
            buttonSelecionarItemLoja.Enabled = false;
            buttonPesquisaClienteLoja.Enabled = true;
            buttonRemoverNomePesquisaAreaitens.Visible = true;
            buttonAddQtdItemLoja.Enabled = false;
        }

        private void buttonCancelarCompra_Click(object sender, EventArgs e)
        {
            listaCarrinho.Clear();
            listViewCarrinhoCompraLoja.Items.Clear();
            labelPrecoTotal.Text = String.Empty;
        }
        private void buttonAddQtdItemLoja_Click(object sender, EventArgs e)
        {
            textBoxInformeQtd.Focus();
            string quantidade = textBoxInformeQtd.Text;
            ClienteProduto cp = new ClienteProduto();

            if (!String.IsNullOrEmpty(textBoxInformeQtd.Text))
            {
                if (listViewItensLoja.SelectedItems.Count > 0)
                {
                    ListView.SelectedListViewItemCollection linha = this.listViewItensLoja.SelectedItems;

                    foreach (ListViewItem item in linha)
                    {
                        //pega o Id do produto na ListView na tab Loja
                        int idProduto = int.Parse(item.SubItems[0].Text);
                        //Pega o nome do produto na coluna de indice 1 da listView na tab Loja
                        string nomeProduto = item.SubItems[1].Text;

                        decimal precoProduto = decimal.Parse(item.SubItems[2].Text);
                        cp.ProdutoId = idProduto;
                        cp.Produto = new Produto { Nome = nomeProduto };
                        cp.PrecoUnitario = precoProduto;
                        cp.QtdTotal = int.Parse(quantidade);
                        cp.PrecoTotal = int.Parse(quantidade) * precoProduto;
                        listaCarrinho.Add(cp);
                        AtualizarListaItens();
                        textBoxInformeQtd.Text = String.Empty;

                    }
                }
                else
                {
                    MessageBox.Show("Selecione um item", "Aviso");
                }
            }
            else
            {
                MessageBox.Show("Digite a quantidade e selecione o item", "Aviso");
                return;
            }
        }

        private (decimal? vendasProdutos, decimal? impostoPagar, decimal? caixa) BuscarUltimosValoresVendasEImposto(Contexto contexto)
        {
            var ultimasVendasProdutos = contexto.Relatorios
                .Where(r => r.VendasProdutos.HasValue)
                .OrderByDescending(r => r.RelatorioId)
                .Select(r => r.VendasProdutos)
                .FirstOrDefault();

            var ultimoImpostoPagar = contexto.Relatorios
                .Where(r => r.ImpostoPagar.HasValue)
                .OrderByDescending(r => r.RelatorioId)
                .Select(r => r.ImpostoPagar)
                .FirstOrDefault();

            var ultimoCaixa = contexto.Relatorios
                .Where(r => r.Caixa.HasValue)
                .OrderByDescending(r => r.RelatorioId)
                .Select(r => r.Caixa)
                .FirstOrDefault();

            return (ultimasVendasProdutos, ultimoImpostoPagar, ultimoCaixa);
        }

        private void buttonFinalizarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                Relatorio r = new Relatorio();
                int clienteID = int.Parse(textBoxIdClienteHiddenLoja.Text);
                decimal valorTotalCompra = decimal.Parse(labelPrecoTotal.Text);

                List<ClienteProduto> listaClienteProdutos = new List<ClienteProduto>();
                foreach (var produtoCarrinho in listaCarrinho)
                {

                    ClienteProduto clipro = new ClienteProduto
                    {
                        ProdutoId = produtoCarrinho.ProdutoId,
                        PrecoTotal = produtoCarrinho.PrecoTotal,
                        QtdTotal = produtoCarrinho.QtdTotal,
                        PrecoUnitario = produtoCarrinho.PrecoUnitario
                    };

                    listaClienteProdutos.Add(clipro);
                }
                contexto.Notas.Add(new Nota
                {
                    ClienteId = clienteID,
                    PrecoTotalCompra = valorTotalCompra,
                    clienteProdutos = listaClienteProdutos
                });
                //tras os ultimos dados não nulos do banco
                var (ultimaVendaProdutos, ultimoImpostoPagar, ultimoCaixa) = BuscarUltimosValoresVendasEImposto(contexto);

                //faz o calculo com os ultimos dados e adiciona os novos valores a uma nova variavel 
                var novoVendasProdutos = (ultimaVendaProdutos ?? 0) + (decimal.Parse(labelPrecoTotal.Text));
                var novoImpostoPagar = (ultimoImpostoPagar ?? 0) + (decimal.Parse(labelPrecoTotal.Text)* 0.17M);
                var novoCaixa = (ultimoCaixa ?? 0) + decimal.Parse(labelPrecoTotal.Text);

                //salva no banco os valores atualizados
                r.VendasProdutos = novoVendasProdutos;
                r.ImpostoPagar = novoImpostoPagar;
                r.Caixa = novoCaixa;
                contexto.Relatorios.Add(r);
                contexto.SaveChanges();
                MessageBox.Show("Pedido finalizado!");
                listaCarrinho.Clear();
                listViewCarrinhoCompraLoja.Items.Clear();
                labelPrecoTotal.Text = String.Empty;
                buttonRemoverNomePesquisaAreaitens.Enabled = false;
                buttonRemoverNomePesquisaAreaitens_Click(sender, e);
                buscarRelatorio();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message);
            }
        }


        private void buttonPesquisarClienteNotas_Click(object sender, EventArgs e)
        {
            FormBuscarCliente bc = new FormBuscarCliente();
            bc.ShowDialog();

            int idClienteSelecionado = bc.IdCliente;
            string nomeSelecionado = bc.Nome;

            if (String.IsNullOrEmpty(idClienteSelecionado.ToString()) || String.IsNullOrEmpty(nomeSelecionado))
            {
                MessageBox.Show("Você não selecionou nenhum cliente!", "Aviso");
            }
            else
            {
                textBoxIdHiddenClienteNotas.Text = idClienteSelecionado.ToString();
                textBoxNomeClienteNotas.Text = nomeSelecionado.ToString();
            }
        }

        private void buttonPesquisarNotas_Click(object sender, EventArgs e)
        {
            listViewNotasCliente.Items.Clear();
            int id = int.Parse(textBoxIdHiddenClienteNotas.Text);
            List<Nota> listaNotas = new List<Nota>();
            listaNotas.Clear();
            listaNotas =
                (from Nota n in contexto.Notas select n)
                .Where(c => c.ClienteId == id)
                .ToList<Nota>();

            listViewNotasCliente.Items.Clear();

            foreach (var item in listaNotas)
            {
                string[] itens = { item.Id.ToString(), item.Cliente.Nome, item.PrecoTotalCompra.ToString(), item.Data.ToString() };
                listViewNotasCliente.Items.Add(new ListViewItem(itens));
            }
        }

        private void visualizeNota_Click(object sender, EventArgs e)
        {
            if (listViewNotasCliente.SelectedItems.Count > 0)
            {
                ListView.SelectedListViewItemCollection linha = this.listViewNotasCliente.SelectedItems;

                listViewNotas.Items.Clear();
                foreach (ListViewItem item in linha)
                {
                    int idNota = int.Parse(item.SubItems[0].Text);
                    textBoxIdHiddenNota.Text = idNota.ToString();
                    listaItensDaNota = contexto.ClientesProdutos
                        .Where(cp => cp.NotaId == idNota)
                        .ToList();
                    foreach (var i in listaItensDaNota)
                    {
                        string[] itens = { i.Id.ToString(), i.Produto.Nome, i.QtdTotal.ToString(), i.PrecoUnitario.ToString(), i.PrecoTotal.ToString() };
                        listViewNotas.Items.Add(new ListViewItem(itens));
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um item", "Aviso");
            }
        }

        private void buttonEmitirPDF_Click(object sender, EventArgs e)
        {
            string idNota = textBoxIdHiddenNota.Text;
            string nomeDoArquivo = ($"nota{idNota}.pdf");
            GerarPdf(nomeDoArquivo);
        }

        private void GerarPdf(string arquivo)
        {
            using (PdfWriter wPdf = new PdfWriter(arquivo, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0)))
            {
                var pdfDocument = new PdfDocument(wPdf);

                var document = new Document(pdfDocument, PageSize.A4);

                //cria a tabela passa as porcentagens e usa toda a largura da pagina
                float[] columnWidths = { 5, 40, 8, 10, 15 };
                Table tabela = new Table(UnitValue.CreatePercentArray(columnWidths))
                    .UseAllAvailableWidth();

                //Titulo da tabela
                var fonte = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                tabela.AddHeaderCell(new Cell(1, 5).Add(new Paragraph("Emissão de Nota Nr " + textBoxIdHiddenNota.Text)
                    .SetFont(fonte)
                    .SetFontSize(18)
                    .SetPadding(10)
                    .SetTextAlignment(TextAlignment.CENTER)));

                //Cabeçalho com os titulos das colunas
                tabela.AddHeaderCell(new Cell()
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph("Cod")));
                tabela.AddHeaderCell(new Cell()
                    .SetPaddingLeft(5)
                    .Add(new Paragraph("Descrição do Produto")));
                tabela.AddHeaderCell(new Cell()
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph("Qtd")));
                tabela.AddHeaderCell(new Cell()
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetPaddingRight(10)
                    .Add(new Paragraph("Preço Un")));
                tabela.AddHeaderCell(new Cell()
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetPaddingRight(10)
                    .Add(new Paragraph("Preço Total")));
                tabela.SetSkipFirstHeader(false);

                decimal valorTotalCompra = 0;
                foreach (ClienteProduto i in listaItensDaNota)
                {
                    valorTotalCompra += i.PrecoTotal;
                    tabela.AddCell(new Cell()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .Add(new Paragraph(i.Id.ToString())));

                    tabela.AddCell(new Cell()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .Add(new Paragraph(i.Produto.Nome)));

                    tabela.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER)
                        .Add(new Paragraph(i.QtdTotal.ToString())));

                    tabela.AddCell(new Cell()
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetPaddingRight(10)
                        .Add(new Paragraph(i.PrecoUnitario.ToString())));

                    tabela.AddCell(new Cell()
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetPaddingRight(10)
                        .Add(new Paragraph(i.PrecoTotal.ToString())));
                }
                tabela.AddCell(new Cell(1, 5)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetPaddingRight(10)
                        .Add(new Paragraph("TOTAL: " + valorTotalCompra.ToString())));

                var cellFooter = new Cell(1, 5)
                    .Add(new Paragraph("Cliente: " + textBoxNomeClienteNotas.Text))
                    .SetFont(fonte)
                    .SetFontSize(10)
                    .SetPaddingLeft(10)
                    .SetBorder(Border.NO_BORDER);

                tabela.AddFooterCell(cellFooter);

                document.Add(tabela);
                document.Close();

                pdfDocument.Close();

                MessageBox.Show("Arquivo PDF gerado em " + arquivo);

            }
        }

        private void radioButtonRevenda_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRevenda.Checked)
            {
                labelPrecoProdutoVenda.Visible = true;
                textBoxPrecoProdutoVenda.Visible = true;

                // Verifica se os campos de preço e estoque estão preenchidos corretamente
                if (decimal.TryParse(textBoxPrecoProduto.Text, out decimal preco) &&
                    int.TryParse(textBoxEstoqueProduto.Text, out int estoque))
                {
                    decimal valorTotalCompra = preco * estoque;
                    textBoxValorTotalCompra.Text = valorTotalCompra.ToString("F2");

                    labelIcmsRecuperar.Text = (valorTotalCompra * 0.17M).ToString();
                }
                else
                {
                    MessageBox.Show("Por favor, preencha os campos de preço e estoque corretamente.", "Erro de entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    radioButtonRevenda.Checked = false;
                }
            }
            else
            {
                labelPrecoProdutoVenda.Visible = false;
                textBoxPrecoProdutoVenda.Visible = false;
                textBoxValorTotalCompra.Text = string.Empty;
                labelIcmsRecuperar.Text = string.Empty;
            }
        }


        private void radioButtonConsumo_CheckedChanged(object sender, EventArgs e)
        {
            labelPrecoProdutoVenda.Visible = false;
            textBoxPrecoProdutoVenda.Visible = false;
            textBoxPrecoProdutoVenda.Text = string.Empty;
            labelIcmsRecuperar.Text = "R$0";

        }

        private void AtualizarRelatorio()
        {
            var totaisRelatorio = new
            {
                SomaCapitalInicial = contexto.Relatorios.Sum(r => r.CapitalInicial.HasValue ? (decimal)r.CapitalInicial.Value : 0M),
                SomaVendasProdutos = contexto.Relatorios.Sum(r => r.VendasProdutos.HasValue ? (decimal)r.VendasProdutos.Value : 0M),
                SomaComprasProdutos = contexto.Relatorios.Sum(r => r.ComprasProdutos.HasValue ? (decimal)r.ComprasProdutos.Value : 0M),
                SomaImpostoRecuperar = contexto.Relatorios.Sum(r => r.ImpostoRecuperar.HasValue ? (decimal)r.ImpostoRecuperar.Value : 0M),
                SomaImpostoPagar = contexto.Relatorios.Sum(r => r.ImpostoPagar.HasValue ? (decimal)r.ImpostoPagar.Value : 0M)
            };

            decimal valorCaixa = totaisRelatorio.SomaCapitalInicial + totaisRelatorio.SomaVendasProdutos + totaisRelatorio.SomaImpostoRecuperar - totaisRelatorio.SomaImpostoPagar - totaisRelatorio.SomaComprasProdutos;

            textBoxProdutosVendidos.Text = totaisRelatorio.SomaVendasProdutos.ToString("F2");
            textBoxProdutosCompra.Text = totaisRelatorio.SomaComprasProdutos.ToString("F2");
            textBoxImpostoReceber.Text = totaisRelatorio.SomaImpostoRecuperar.ToString("F2");
            textBoxImpostoPagar.Text = totaisRelatorio.SomaImpostoPagar.ToString("F2");
            textBoxCaixa.Text = valorCaixa.ToString("F2");
        }

        private void leave_calcularPrecoTotalProduto(object sender, EventArgs e)
        {
            decimal valorTotalCompra = decimal.Parse(textBoxPrecoProduto.Text) * int.Parse(textBoxEstoqueProduto.Text);
            textBoxValorTotalCompra.Text = valorTotalCompra.ToString("F2");

            radioButtonConsumo.Checked = true;
        }

        private void buttonCadastrarCapital_Click(object sender, EventArgs e)
        {
            Relatorio r = new Relatorio();
            r.CapitalInicial = decimal.Parse(textBoxCapitalInicial.Text);
            r.Caixa = decimal.Parse(textBoxCapitalInicial.Text);

            textBoxCaixa.Text = (decimal.Parse(textBoxCapitalInicial.Text)).ToString("F2");
            textBoxCapitalInicialRel.Text = (decimal.Parse(textBoxCapitalInicial.Text)).ToString("F2");


            contexto.Relatorios.Add(r);
            contexto.SaveChanges();

            textBoxCapitalInicial.Enabled = false;
            buttonCadastrarCapital.Enabled = false;
        }
        private void buscarRelatorio()
        {
            using (var contexto = new Contexto())
            {
                var ultimoCapitalInicial = contexto.Relatorios
                    .Where(r => r.CapitalInicial.HasValue)
                    .OrderByDescending(r => r.RelatorioId)
                    .Select(r => r.CapitalInicial)
                    .FirstOrDefault();

                var ultimoPatrimonio = contexto.Relatorios
                    .Where(r => r.Patrimonio.HasValue)
                    .OrderByDescending(r => r.RelatorioId)
                    .Select(r => r.Patrimonio)
                    .FirstOrDefault();

                var ultimoVendasProdutos = contexto.Relatorios
                    .Where(r => r.VendasProdutos.HasValue)
                    .OrderByDescending(r => r.RelatorioId)
                    .Select(r => r.VendasProdutos)
                    .FirstOrDefault();

                var ultimoComprasProdutos = contexto.Relatorios
                    .Where(r => r.ComprasProdutos.HasValue)
                    .OrderByDescending(r => r.RelatorioId)
                    .Select(r => r.ComprasProdutos)
                    .FirstOrDefault();

                var ultimoImpostoPagar = contexto.Relatorios
                    .Where(r => r.ImpostoPagar.HasValue)
                    .OrderByDescending(r => r.RelatorioId)
                    .Select(r => r.ImpostoPagar)
                    .FirstOrDefault();

                var ultimoImpostoRecuperar = contexto.Relatorios
                    .Where(r => r.ImpostoRecuperar.HasValue)
                    .OrderByDescending(r => r.RelatorioId)
                    .Select(r => r.ImpostoRecuperar)
                    .FirstOrDefault();

                var ultimoCaixa = contexto.Relatorios
                    .Where(r => r.Caixa.HasValue)
                    .OrderByDescending(r => r.RelatorioId)
                    .Select(r => r.Caixa)
                    .FirstOrDefault();

                textBoxCapitalInicial.Text = ultimoCapitalInicial?.ToString() ?? string.Empty;
                if (!(textBoxCapitalInicial.Text).IsNullOrEmpty())
                {
                    textBoxCapitalInicial.Enabled = false;
                    buttonCadastrarCapital.Enabled = false;
                    textBoxCapitalInicialRel.Text = textBoxCapitalInicial.Text;
                }
                textBoxPatrimonioRel.Text = ultimoPatrimonio?.ToString() ?? string.Empty;
                textBoxCaixa.Text = ultimoCaixa?.ToString() ?? string.Empty;
                textBoxProdutosVendidos.Text = ultimoVendasProdutos?.ToString() ?? string.Empty;
                textBoxProdutosCompra.Text = ultimoComprasProdutos?.ToString() ?? string.Empty;
                textBoxImpostoPagar.Text = ultimoImpostoPagar?.ToString() ?? string.Empty;
                textBoxImpostoReceber.Text = ultimoImpostoRecuperar?.ToString() ?? string.Empty;
                
                // Convertendo os valores para decimal
                decimal patrimonio = ultimoPatrimonio ?? 0;
                decimal vendasProdutos = ultimoVendasProdutos ?? 0;
                decimal caixa = ultimoCaixa ?? 0;
                decimal capitalInicial = ultimoCapitalInicial ?? 0;
                decimal impostoPagar = ultimoImpostoPagar ?? 0;

                // Calculando o lucro
                decimal lucro = patrimonio + vendasProdutos + caixa - (capitalInicial + impostoPagar);

                // Convertendo o resultado para string
                string lucroString = lucro.ToString("F2");

                textBoxLucro.Text = lucroString;

            }
        }
    }
}