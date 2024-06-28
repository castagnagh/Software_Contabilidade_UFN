namespace MiniERP_Entity
{
    partial class FormBuscaFornecedor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            buttonSelecionarFornecedor = new Button();
            listViewFornecedoresBusca = new ListView();
            id = new ColumnHeader();
            razaoSocial = new ColumnHeader();
            cnpj = new ColumnHeader();
            maskedTextBoxBusca = new MaskedTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 26);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 0;
            label1.Text = "Busca por CNPJ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 89);
            label2.Name = "label2";
            label2.Size = new Size(144, 15);
            label2.TabIndex = 2;
            label2.Text = "Fornecedores cadastrados";
            // 
            // buttonSelecionarFornecedor
            // 
            buttonSelecionarFornecedor.Location = new Point(369, 414);
            buttonSelecionarFornecedor.Name = "buttonSelecionarFornecedor";
            buttonSelecionarFornecedor.Size = new Size(75, 23);
            buttonSelecionarFornecedor.TabIndex = 4;
            buttonSelecionarFornecedor.Text = "Selecionar";
            buttonSelecionarFornecedor.UseVisualStyleBackColor = true;
            buttonSelecionarFornecedor.Click += buttonSelecionarFornecedor_Click;
            // 
            // listViewFornecedoresBusca
            // 
            listViewFornecedoresBusca.Columns.AddRange(new ColumnHeader[] { id, razaoSocial, cnpj });
            listViewFornecedoresBusca.Cursor = Cursors.Hand;
            listViewFornecedoresBusca.FullRowSelect = true;
            listViewFornecedoresBusca.Location = new Point(26, 107);
            listViewFornecedoresBusca.MultiSelect = false;
            listViewFornecedoresBusca.Name = "listViewFornecedoresBusca";
            listViewFornecedoresBusca.Size = new Size(418, 301);
            listViewFornecedoresBusca.TabIndex = 5;
            listViewFornecedoresBusca.UseCompatibleStateImageBehavior = false;
            listViewFornecedoresBusca.View = View.Details;
            // 
            // id
            // 
            id.Text = "Id";
            // 
            // razaoSocial
            // 
            razaoSocial.Text = "Razão Social";
            razaoSocial.Width = 160;
            // 
            // cnpj
            // 
            cnpj.Text = "CNPJ";
            cnpj.Width = 160;
            // 
            // maskedTextBoxBusca
            // 
            maskedTextBoxBusca.Location = new Point(26, 44);
            maskedTextBoxBusca.Mask = "00,000,000/0000-00";
            maskedTextBoxBusca.Name = "maskedTextBoxBusca";
            maskedTextBoxBusca.Size = new Size(418, 23);
            maskedTextBoxBusca.TabIndex = 6;
            maskedTextBoxBusca.KeyUp += buscaFormFornecedores;
            // 
            // FormBuscaFornecedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(466, 449);
            Controls.Add(maskedTextBoxBusca);
            Controls.Add(listViewFornecedoresBusca);
            Controls.Add(buttonSelecionarFornecedor);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormBuscaFornecedor";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Pesquisa de Fornecedor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button buttonSelecionarFornecedor;
        private Button button1;
        private ListView listViewFornecedoresBusca;
        private ColumnHeader razaoSocial;
        private ColumnHeader cnpj;
        private ColumnHeader id;
        private MaskedTextBox maskedTextBoxBusca;
    }
}