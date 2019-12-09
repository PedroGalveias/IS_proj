using AlertsForm.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


namespace AlertsForm
{
    public partial class AlertForm : Form
    {
        MqttClient client = null;
        const string TOPIC = "alertas";
        //enum Operacoes{MAIOR,MENOR,IGUAL,ENTRE};
        string[] operacoes= { ">", "<", "=","Entre" };
        //**** TIPOS DEBERIAN SER TRAIDOS DE LA BD
        enum Tipos { HUMIDADE,TEMPERATURA,LUMINOSIDADE}
        //Dominio do broker
        const string BROKENDOMAIN = "127.0.0.1";

        public AlertForm()
        {
            InitializeComponent();
            //comboBoxOperacao.DataSource = Enum.GetNames(typeof(Operacoes));
            comboBoxOperacao.DataSource = operacoes;
            numericUpDownValor2.Visible = false;
            comboBoxType.DataSource = Enum.GetNames(typeof(Tipos));
        }

        private void ButtonLimpar_Click(object sender, EventArgs e)
        {
            numericUpDownValor1.Value = 0;
            numericUpDownValor2.Value = 0;
            numericUpDownValor2.Visible = false;
            comboBoxOperacao.SelectedIndex = 0;
            comboBoxType.SelectedIndex = 0;
        }

        private void buttonAdicionar_Click(object sender, EventArgs e)
        {
            double valor1 = (double)numericUpDownValor1.Value;
            double valor2 = (double)numericUpDownValor2.Value;
            String operacao = comboBoxOperacao.SelectedItem.ToString();
            String tipo = comboBoxType.SelectedItem.ToString();
            Alerta novaAlerta;
            if (operacao == "Entre")
            {
                 novaAlerta= new Alerta(tipo, operacao, valor1, valor2);
            }
            else
            {
            novaAlerta = new Alerta(tipo, operacao, valor1);
            }

            //codigo para publicar alerta no broker 

            client = new MqttClient(BROKENDOMAIN);

            client.Connect(Guid.NewGuid().ToString());
            if (!client.IsConnected)
            {
                MessageBox.Show("Error connecting to message broker ...");
                return;
            }
            else
            {
                string msg = novaAlerta.ToString();
                
                MessageBox.Show("Connection to broke ok...");
                //envio da alerta 
                client.Publish(TOPIC, Encoding.UTF8.GetBytes(msg));
            }

        }

        private void comboBoxOperacao_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxOperacao.SelectedItem.ToString() == "Entre")
            {
                numericUpDownValor2.Visible = true;
                return;
            }
            numericUpDownValor2.Visible = false;
        }


        /*
                private Boolean ValidateAlertInfo()
                {
                    bool valido = true;
                    double valor1 = (double) numericUpDownValor1.Value;
                    if (comboBoxOperacao.SelectedItem == "Entre")
                    {
                    double valor2 = (double) numericUpDownValor2.Value;
                    }            
                    return true;
                }*/
    }
}
