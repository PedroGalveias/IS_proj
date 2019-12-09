namespace AlertsForm
{
    partial class AlertForm
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
            this.listBoxCondicoesAtivas = new System.Windows.Forms.ListBox();
            this.buttonAtivar = new System.Windows.Forms.Button();
            this.buttonDesativar = new System.Windows.Forms.Button();
            this.groupBoxAtivas = new System.Windows.Forms.GroupBox();
            this.listBoxCondicoesDesativas = new System.Windows.Forms.ListBox();
            this.groupBoxDesativas = new System.Windows.Forms.GroupBox();
            this.groupBoxNews = new System.Windows.Forms.GroupBox();
            this.numericUpDownValor2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownValor1 = new System.Windows.Forms.NumericUpDown();
            this.buttonAdicionar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxOperacao = new System.Windows.Forms.ComboBox();
            this.labelValor = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLimpar = new System.Windows.Forms.Button();
            this.groupBoxAtivas.SuspendLayout();
            this.groupBoxDesativas.SuspendLayout();
            this.groupBoxNews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValor2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValor1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxCondicoesAtivas
            // 
            this.listBoxCondicoesAtivas.FormattingEnabled = true;
            this.listBoxCondicoesAtivas.ItemHeight = 16;
            this.listBoxCondicoesAtivas.Location = new System.Drawing.Point(15, 30);
            this.listBoxCondicoesAtivas.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxCondicoesAtivas.Name = "listBoxCondicoesAtivas";
            this.listBoxCondicoesAtivas.Size = new System.Drawing.Size(231, 260);
            this.listBoxCondicoesAtivas.TabIndex = 0;
            // 
            // buttonAtivar
            // 
            this.buttonAtivar.Location = new System.Drawing.Point(291, 25);
            this.buttonAtivar.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAtivar.Name = "buttonAtivar";
            this.buttonAtivar.Size = new System.Drawing.Size(184, 59);
            this.buttonAtivar.TabIndex = 1;
            this.buttonAtivar.Text = "Ativar Condição";
            this.buttonAtivar.UseVisualStyleBackColor = true;
            this.buttonAtivar.Click += new System.EventHandler(this.buttonAtivar_Click);
            // 
            // buttonDesativar
            // 
            this.buttonDesativar.Location = new System.Drawing.Point(268, 30);
            this.buttonDesativar.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDesativar.Name = "buttonDesativar";
            this.buttonDesativar.Size = new System.Drawing.Size(184, 59);
            this.buttonDesativar.TabIndex = 2;
            this.buttonDesativar.Text = "Desativar Condição";
            this.buttonDesativar.UseVisualStyleBackColor = true;
            this.buttonDesativar.Click += new System.EventHandler(this.buttonDesativar_Click);
            // 
            // groupBoxAtivas
            // 
            this.groupBoxAtivas.Controls.Add(this.buttonDesativar);
            this.groupBoxAtivas.Controls.Add(this.listBoxCondicoesAtivas);
            this.groupBoxAtivas.Location = new System.Drawing.Point(40, 230);
            this.groupBoxAtivas.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxAtivas.Name = "groupBoxAtivas";
            this.groupBoxAtivas.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxAtivas.Size = new System.Drawing.Size(465, 309);
            this.groupBoxAtivas.TabIndex = 3;
            this.groupBoxAtivas.TabStop = false;
            this.groupBoxAtivas.Text = "Condicoes Ativas";
            // 
            // listBoxCondicoesDesativas
            // 
            this.listBoxCondicoesDesativas.FormattingEnabled = true;
            this.listBoxCondicoesDesativas.ItemHeight = 16;
            this.listBoxCondicoesDesativas.Location = new System.Drawing.Point(35, 25);
            this.listBoxCondicoesDesativas.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxCondicoesDesativas.Name = "listBoxCondicoesDesativas";
            this.listBoxCondicoesDesativas.Size = new System.Drawing.Size(247, 244);
            this.listBoxCondicoesDesativas.TabIndex = 4;
            // 
            // groupBoxDesativas
            // 
            this.groupBoxDesativas.Controls.Add(this.listBoxCondicoesDesativas);
            this.groupBoxDesativas.Controls.Add(this.buttonAtivar);
            this.groupBoxDesativas.Location = new System.Drawing.Point(533, 235);
            this.groupBoxDesativas.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxDesativas.Name = "groupBoxDesativas";
            this.groupBoxDesativas.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxDesativas.Size = new System.Drawing.Size(499, 303);
            this.groupBoxDesativas.TabIndex = 5;
            this.groupBoxDesativas.TabStop = false;
            this.groupBoxDesativas.Text = "Condicoes Desativas";
            // 
            // groupBoxNews
            // 
            this.groupBoxNews.Controls.Add(this.numericUpDownValor2);
            this.groupBoxNews.Controls.Add(this.numericUpDownValor1);
            this.groupBoxNews.Controls.Add(this.buttonAdicionar);
            this.groupBoxNews.Controls.Add(this.label3);
            this.groupBoxNews.Controls.Add(this.comboBoxOperacao);
            this.groupBoxNews.Controls.Add(this.labelValor);
            this.groupBoxNews.Controls.Add(this.comboBoxType);
            this.groupBoxNews.Controls.Add(this.label2);
            this.groupBoxNews.Controls.Add(this.label1);
            this.groupBoxNews.Controls.Add(this.buttonLimpar);
            this.groupBoxNews.Location = new System.Drawing.Point(43, 17);
            this.groupBoxNews.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxNews.Name = "groupBoxNews";
            this.groupBoxNews.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxNews.Size = new System.Drawing.Size(988, 199);
            this.groupBoxNews.TabIndex = 6;
            this.groupBoxNews.TabStop = false;
            this.groupBoxNews.Text = "Nova Alerta";
            // 
            // numericUpDownValor2
            // 
            this.numericUpDownValor2.DecimalPlaces = 2;
            this.numericUpDownValor2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownValor2.Location = new System.Drawing.Point(585, 80);
            this.numericUpDownValor2.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownValor2.Name = "numericUpDownValor2";
            this.numericUpDownValor2.Size = new System.Drawing.Size(164, 22);
            this.numericUpDownValor2.TabIndex = 11;
            // 
            // numericUpDownValor1
            // 
            this.numericUpDownValor1.DecimalPlaces = 2;
            this.numericUpDownValor1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownValor1.Location = new System.Drawing.Point(132, 82);
            this.numericUpDownValor1.Name = "numericUpDownValor1";
            this.numericUpDownValor1.Size = new System.Drawing.Size(169, 22);
            this.numericUpDownValor1.TabIndex = 10;
            // 
            // buttonAdicionar
            // 
            this.buttonAdicionar.Location = new System.Drawing.Point(847, 80);
            this.buttonAdicionar.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAdicionar.Name = "buttonAdicionar";
            this.buttonAdicionar.Size = new System.Drawing.Size(117, 54);
            this.buttonAdicionar.TabIndex = 9;
            this.buttonAdicionar.Text = "Adicionar";
            this.buttonAdicionar.UseVisualStyleBackColor = true;
            this.buttonAdicionar.Click += new System.EventHandler(this.buttonAdicionar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(409, 60);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "operação";
            // 
            // comboBoxOperacao
            // 
            this.comboBoxOperacao.FormattingEnabled = true;
            this.comboBoxOperacao.Location = new System.Drawing.Point(385, 80);
            this.comboBoxOperacao.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxOperacao.Name = "comboBoxOperacao";
            this.comboBoxOperacao.Size = new System.Drawing.Size(129, 24);
            this.comboBoxOperacao.TabIndex = 6;
            this.comboBoxOperacao.SelectedValueChanged += new System.EventHandler(this.comboBoxOperacao_SelectedValueChanged);
            // 
            // labelValor
            // 
            this.labelValor.AutoSize = true;
            this.labelValor.Location = new System.Drawing.Point(45, 87);
            this.labelValor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelValor.Name = "labelValor";
            this.labelValor.Size = new System.Drawing.Size(45, 17);
            this.labelValor.TabIndex = 5;
            this.labelValor.Text = "Valor:";
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(132, 28);
            this.comboBoxType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(169, 24);
            this.comboBoxType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tipo Sensor:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 1;
            // 
            // buttonLimpar
            // 
            this.buttonLimpar.Location = new System.Drawing.Point(847, 23);
            this.buttonLimpar.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLimpar.Name = "buttonLimpar";
            this.buttonLimpar.Size = new System.Drawing.Size(117, 30);
            this.buttonLimpar.TabIndex = 0;
            this.buttonLimpar.Text = "Limpar Dados";
            this.buttonLimpar.UseVisualStyleBackColor = true;
            this.buttonLimpar.Click += new System.EventHandler(this.ButtonLimpar_Click);
            // 
            // AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.groupBoxNews);
            this.Controls.Add(this.groupBoxDesativas);
            this.Controls.Add(this.groupBoxAtivas);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AlertForm";
            this.Text = "AlertForm";
            this.groupBoxAtivas.ResumeLayout(false);
            this.groupBoxDesativas.ResumeLayout(false);
            this.groupBoxNews.ResumeLayout(false);
            this.groupBoxNews.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValor2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValor1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxCondicoesAtivas;
        private System.Windows.Forms.Button buttonAtivar;
        private System.Windows.Forms.Button buttonDesativar;
        private System.Windows.Forms.GroupBox groupBoxAtivas;
        private System.Windows.Forms.ListBox listBoxCondicoesDesativas;
        private System.Windows.Forms.GroupBox groupBoxDesativas;
        private System.Windows.Forms.GroupBox groupBoxNews;
        private System.Windows.Forms.Button buttonAdicionar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxOperacao;
        private System.Windows.Forms.Label labelValor;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLimpar;
        private System.Windows.Forms.NumericUpDown numericUpDownValor1;
        private System.Windows.Forms.NumericUpDown numericUpDownValor2;
    }
}

