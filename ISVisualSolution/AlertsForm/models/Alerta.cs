namespace AlertsForm.models
{
	class Alerta
	{
		public int Id { get; set; }
		public string Tipo { get; set; }
		public string Operacao { get; set; }
		public double Valor1 { get; set; }
		public double Valor2 { get; set; }
		public bool Ativo { get; set; }

		public Alerta(int id, string tipo, string operacao, double valor1, double valor2, bool ativo)
		{
			this.Id = id;
			this.Tipo = tipo;
			this.Operacao = operacao;
			this.Valor1 = valor1;
			this.Valor2 = valor2;
			this.Ativo = ativo;
		}
		public Alerta(string tipo, string operacao, double valor1, double valor2)
		{
			this.Tipo = tipo;
			this.Operacao = operacao;
			this.Valor1 = valor1;
			this.Valor2 = valor2;
			this.Ativo = true;
		}
		public Alerta(int id, string tipo, string operacao, double valor1)
		{
			this.Id = id;
			this.Tipo = tipo;
			this.Operacao = operacao;
			this.Valor1 = valor1;
			this.Ativo = true;
		}

		public Alerta()
		{
		}

		override
		public string ToString()
		{
			if (this.Operacao == "ENTRE")
			{
				return "Alert_ID:" + Id.ToString() + " Tipo: " + Tipo + " Condicao => " + Operacao + " " + Valor1.ToString() + " & " + Valor2.ToString();
			}
			return "Alert_ID:" + Id.ToString() + " Tipo: " + Tipo + " Condicao => " + Operacao + " a " + Valor1.ToString();
		}
	}
}
