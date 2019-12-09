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
            this.listBoxCondicoes = new System.Windows.Forms.ListBox();
            this.buttonAtivar = new System.Windows.Forms.Button();
            this.buttonDesativar = new System.Windows.Forms.Button();
            this.groupBoxAtivas = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBoxDesativas = new System.Windows.Forms.GroupBox();
            this.groupBoxNews = new System.Windows.Forms.GroupBox();
            this.buttonLimpar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxValor = new System.Windows.Forms.TextBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.labelValor = new System.Windows.Forms.Label();
            this.comboBoxOperacao = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxValor2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxAtivas.SuspendLayout();
            this.groupBoxDesativas.SuspendLayout();
            this.groupBoxNews.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxCondicoes
            // 
            this.listBoxCondicoes.FormattingEnabled = true;
            this.listBoxCondicoes.Location = new System.Drawing.Point(11, 24);
            this.listBoxCondicoes.Name = "listBoxCondicoes";
            this.listBoxCondicoes.Size = new System.Drawing.Size(174, 212);
            this.listBoxCondicoes.TabIndex = 0;
            // 
            // buttonAtivar
            // 
            this.buttonAtivar.Location = new System.Drawing.Point(218, 20);
            this.buttonAtivar.Name = "buttonAtivar";
            this.buttonAtivar.Size = new System.Drawing.Size(138, 48);
            this.buttonAtivar.TabIndex = 1;
            this.buttonAtivar.Text = "Ativar Condição";
            this.buttonAtivar.UseVisualStyleBackColor = true;
            // 
            // buttonDesativar
            // 
            this.buttonDesativar.Location = new System.Drawing.Point(201, 24);
            this.buttonDesativar.Name = "buttonDesativar";
            this.buttonDesativar.Size = new System.Drawing.Size(138, 48);
            this.buttonDesativar.TabIndex = 2;
            this.buttonDesativar.Text = "Desativar Condição";
            this.buttonDesativar.UseVisualStyleBackColor = true;
            // 
            // groupBoxAtivas
            // 
            this.groupBoxAtivas.Controls.Add(this.buttonDesativar);
            this.groupBoxAtivas.Controls.Add(this.listBoxCondicoes);
            this.groupBoxAtivas.Location = new System.Drawing.Point(30, 187);
            this.groupBoxAtivas.Name = "groupBoxAtivas";
            this.groupBoxAtivas.Size = new System.Drawing.Size(349, 251);
            this.groupBoxAtivas.TabIndex = 3;
            this.groupBoxAtivas.TabStop = false;
            this.groupBoxAtivas.Text = "Condicoes Ativas";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(26, 20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(186, 199);
            this.listBox1.TabIndex = 4;
            // 
            // groupBoxDesativas
            // 
            this.groupBoxDesativas.Controls.Add(this.listBox1);
            this.groupBoxDesativas.Controls.Add(this.buttonAtivar);
            this.groupBoxDesativas.Location = new System.Drawing.Point(400, 191);
            this.groupBoxDesativas.Name = "groupBoxDesativas";
            this.groupBoxDesativas.Size = new System.Drawing.Size(374, 246);
            this.groupBoxDesativas.TabIndex = 5;
            this.groupBoxDesativas.TabStop = false;
            this.groupBoxDesativas.Text = "Condicoes Desativas";
            // 
            // groupBoxNews
            // 
            this.groupBoxNews.Controls.Add(this.button1);
            this.groupBoxNews.Controls.Add(this.textBoxValor2);
            this.groupBoxNews.Controls.Add(this.label3);
            this.groupBoxNews.Controls.Add(this.comboBoxOperacao);
            this.groupBoxNews.Controls.Add(this.labelValor);
            this.groupBoxNews.Controls.Add(this.comboBoxType);
            this.groupBoxNews.Controls.Add(this.textBoxValor);
            this.groupBoxNews.Controls.Add(this.label2);
            this.groupBoxNews.Controls.Add(this.label1);
            this.groupBoxNews.Controls.Add(this.buttonLimpar);
            this.groupBoxNews.Location = new System.Drawing.Point(32, 14);
            this.groupBoxNews.Name = "groupBoxNews";
            this.groupBoxNews.Size = new System.Drawing.Size(741, 162);
            this.groupBoxNews.TabIndex = 6;
            this.groupBoxNews.TabStop = false;
            this.groupBoxNews.Text = "New Alert";
            // 
            // buttonLimpar
            // 
            this.buttonLimpar.Location = new System.Drawing.Point(635, 19);
            this.buttonLimpar.Name = "buttonLimpar";
            this.buttonLimpar.Size = new System.Drawing.Size(88, 24);
            this.buttonLimpar.TabIndex = 0;
            this.buttonLimpar.Text = "Limpar Dados";
            this.buttonLimpar.UseVisualStyleBackColor = true;
            this.buttonLimpar.Click += new System.EventHandler(this.ButtonLimpar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sensor Type:";
            // 
            // textBoxValor
            // 
            this.textBoxValor.Location = new System.Drawing.Point(99, 65);
            this.textBoxValor.Name = "textBoxValor";
            this.textBoxValor.Size = new System.Drawing.Size(128, 20);
            this.textBoxValor.TabIndex = 3;
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(99, 23);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(128, 21);
            this.comboBoxType.TabIndex = 4;
            // 
            // labelValor
            // 
            this.labelValor.AutoSize = true;
            this.labelValor.Location = new System.Drawing.Point(34, 71);
            this.labelValor.Name = "labelValor";
            this.labelValor.Size = new System.Drawing.Size(34, 13);
            this.labelValor.TabIndex = 5;
            this.labelValor.Text = "Valor:";
            // 
            // comboBoxOperacao
            // 
            this.comboBoxOperacao.FormattingEnabled = true;
            this.comboBoxOperacao.Location = new System.Drawing.Point(273, 65);
            this.comboBoxOperacao.Name = "comboBoxOperacao";
            this.comboBoxOperacao.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOperacao.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(307, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "operação";
            // 
            // textBoxValor2
            // 
            this.textBoxValor2.Location = new System.Drawing.Point(439, 64);
            this.textBoxValor2.Name = "textBoxValor2";
            this.textBoxValor2.Size = new System.Drawing.Size(141, 20);
            this.textBoxValor2.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(635, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 44);
            this.button1.TabIndex = 9;
            this.button1.Text = "Adicionar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBoxNews);
            this.Controls.Add(this.groupBoxDesativas);
            this.Controls.Add(this.groupBoxAtivas);
            this.Name = "AlertForm";
            this.Text = "AlertForm";
            this.groupBoxAtivas.ResumeLayout(false);
            this.groupBoxDesativas.ResumeLayout(false);
            this.groupBoxNews.ResumeLayout(false);
            this.groupBoxNews.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxCondicoes;
        private System.Windows.Forms.Button buttonAtivar;
        private System.Windows.Forms.Button buttonDesativar;
        private System.Windows.Forms.GroupBox groupBoxAtivas;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBoxDesativas;
        private System.Windows.Forms.GroupBox groupBoxNews;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxValor2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxOperacao;
        private System.Windows.Forms.Label labelValor;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.TextBox textBoxValor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLimpar;
    }
}

