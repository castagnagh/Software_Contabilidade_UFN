namespace MiniERP_Entity
{
    partial class FormBuscarCliente
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
            listViewClientesBusca = new ListView();
            id = new ColumnHeader();
            nome = new ColumnHeader();
            cpf = new ColumnHeader();
            buttonSelecionarCliente = new Button();
            label2 = new Label();
            maskedTextBox1 = new MaskedTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 30);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 0;
            label1.Text = "Buscar por CPF";
            // 
            // listViewClientesBusca
            // 
            listViewClientesBusca.Columns.AddRange(new ColumnHeader[] { id, nome, cpf });
            listViewClientesBusca.FullRowSelect = true;
            listViewClientesBusca.Location = new Point(28, 100);
            listViewClientesBusca.MultiSelect = false;
            listViewClientesBusca.Name = "listViewClientesBusca";
            listViewClientesBusca.Size = new Size(411, 308);
            listViewClientesBusca.TabIndex = 2;
            listViewClientesBusca.UseCompatibleStateImageBehavior = false;
            listViewClientesBusca.View = View.Details;
            // 
            // id
            // 
            id.Text = "Id";
            // 
            // nome
            // 
            nome.Text = "Nome";
            nome.Width = 165;
            // 
            // cpf
            // 
            cpf.Text = "CPF";
            cpf.Width = 165;
            // 
            // buttonSelecionarCliente
            // 
            buttonSelecionarCliente.Location = new Point(364, 414);
            buttonSelecionarCliente.Name = "buttonSelecionarCliente";
            buttonSelecionarCliente.Size = new Size(75, 23);
            buttonSelecionarCliente.TabIndex = 3;
            buttonSelecionarCliente.Text = "Selecionar";
            buttonSelecionarCliente.UseVisualStyleBackColor = true;
            buttonSelecionarCliente.Click += buttonSelecionarCliente_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 82);
            label2.Name = "label2";
            label2.Size = new Size(115, 15);
            label2.TabIndex = 4;
            label2.Text = "Clientes cadastrados";
            // 
            // maskedTextBox1
            // 
            maskedTextBox1.Location = new Point(28, 48);
            maskedTextBox1.Mask = "000,000,000-00";
            maskedTextBox1.Name = "maskedTextBox1";
            maskedTextBox1.Size = new Size(411, 23);
            maskedTextBox1.TabIndex = 0;
            maskedTextBox1.Click += maskedTextBox1_Click;
            maskedTextBox1.KeyUp += KeyUp_BuscarClientePorCpf;
            // 
            // FormBuscarCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(466, 449);
            Controls.Add(maskedTextBox1);
            Controls.Add(label2);
            Controls.Add(buttonSelecionarCliente);
            Controls.Add(listViewClientesBusca);
            Controls.Add(label1);
            Name = "FormBuscarCliente";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Pesquisa de Cliente";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private ListView listViewClientesBusca;
        private Button buttonSelecionarCliente;
        private Label label2;
        private ColumnHeader id;
        private ColumnHeader nome;
        private ColumnHeader cpf;
        private MaskedTextBox maskedTextBox1;
    }
}