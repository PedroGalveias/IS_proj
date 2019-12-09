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
using Newtonsoft.Json;
using System.Data.SqlClient;


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
       
        //Connection string 
        static string connectionString = Properties.Settings.Default.ConnectionString;
        
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
                string msg = JsonConvert.SerializeObject(novaAlerta);
                
                MessageBox.Show("Connection to broke ok...");
                //envio da alerta 
                client.Publish(TOPIC, Encoding.UTF8.GetBytes(msg));
                MessageBox.Show("Sending Alert to topic: "+TOPIC);
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

        private void atualizarAlertas()
        {
            List<Alerta> listaAlertasAtivas = new List<Alerta>();
            List<Alerta> listaAlertasDesativas = new List<Alerta>();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Alerts", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Alerta alerta = new Alerta
                    {
                        Id = (int)reader["Id"],
                        Tipo = (string)reader["Tipo"],
                        Operacao = (string)reader["Operacao"],
                        Valor1 = (reader["Valor1"] == DBNull.Value) ? 0 : Convert.ToDouble(reader["Valor1"]),
                        Valor2 = (reader["Valor2"] == DBNull.Value) ? 0 : Convert.ToDouble(reader["Valor2"]),
                        Ativo = (bool)reader["Ativo"]
                    };
                    if (alerta.Ativo)
                    {
                    listaAlertasAtivas.Add(alerta);
                    }
                    else
                    {
                        listaAlertasDesativas.Add(alerta);
                    }

                    listBoxCondicoesAtivas.DataSource = listaAlertasAtivas;
                    listBoxCondicoesDesativas.DataSource = listaAlertasDesativas;
                }
                reader.Close();
                conn.Close();
                return;
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }

                return;
            }
        }

        private void buttonDesativar_Click(object sender, EventArgs e)
        {
            if (listBoxCondicoesAtivas.SelectedValue == null)
            {
                MessageBox.Show("Error Nenhum elemento selecionado ...");
                return;
            }

            mudarEstado();
           
        }

        private void mudarEstado()
        {
            try
            {
                Alerta alerta = (Alerta)listBoxCondicoesAtivas.SelectedValue;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Alerts SET Ativo=@ativo WHERE Id=@idAlert", conn);
                    cmd.Parameters.AddWithValue("@ativo", !alerta.Ativo);
                    cmd.Parameters.AddWithValue("@idAlert", alerta.Id);

                    int result = cmd.ExecuteNonQuery();//devuelve cuantos registros fueron afectados
                    conn.Close();
                    if (result > 0)
                    {
                        MessageBox.Show("OK ...");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Error Alert Not found ...");
                        return;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error ...");
                return;
            }
        }

        private void buttonAtivar_Click(object sender, EventArgs e)
        {
            if (listBoxCondicoesAtivas.SelectedValue == null)
            {
                MessageBox.Show("Error Nenhum elemento selecionado ...");
                return;
            }

            mudarEstado();
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
