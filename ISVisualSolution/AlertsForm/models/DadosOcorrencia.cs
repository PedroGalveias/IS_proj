using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertsForm.models
{
    class DadosOcorrencia
    {
        public int IdSensor { get; set; }
        public string Tipo { get; set; }
        public double ValorSensor { get; set; }
        public int IdAlerta { get; set; }       
        public string Operacao { get; set; }
        public double Valor1 { get; set; }
        public double Valor2 { get; set; }        

        override
       public string ToString()
        {
            if (this.Operacao == "ENTRE")
            {
                return "Sensor_ID: "+IdSensor.ToString() + " Tipo: " + Tipo +" ValorSensor: "+ValorSensor+ " Alert_ID:" + IdAlerta.ToString() + " Condicao => " + Operacao + " " + Valor1.ToString() + " & " + Valor2.ToString();
            }
            return "Sensor_ID: " + IdSensor.ToString() + " Tipo: " + Tipo + " ValorSensor: " + ValorSensor + " Alert_ID:" + IdAlerta.ToString() + " Condicao => " + Operacao + " a " + Valor1.ToString() ;
        }
    }

}
