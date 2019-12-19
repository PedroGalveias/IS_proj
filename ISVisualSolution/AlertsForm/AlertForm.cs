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
using Newtonsoft.Json.Linq;
using System.IO;
using System.Xml;
using System.Globalization;

namespace AlertsForm
{
    public partial class AlertForm : Form
    {
        
        MqttClient client = null;
        const string TOPIC = "alerts";      
        string[] topicsSubscriber = { "bridge"};
        //enum Operacoes{MAIOR,MENOR,IGUAL,ENTRE};
        string[] operacoes= { ">", "<", "=","ENTRE" };
        //**** TIPOS DEBERIAN SER TRAIDOS DE LA BD
        enum Tipos { HUMIDADE,TEMPERATURA,LUMINOSIDADE}
        //Dominio do broker
        const string BROKENDOMAIN = "127.0.0.1";
       
        //Connection string 
        static string connectionString = Properties.Settings.Default.ConnectionString;

        //List of alerts
        List<Alerta> alertasAtivas;
        List<Alerta> alertasDesativas;

        //File alerts
        const string FILE_ALERT = "alerts.xml";

        //contador para ID
        int contador;
        public AlertForm()
        {
            #region UpdateContador
            //contador para ID
            contador = 1;
            // expressao xpath = alerts/alert[not(../alert/id > id)]/id maior ID
            if (File.Exists(FILE_ALERT))
            {
                XmlDocument doc = new XmlDocument();

                try
                {
                    doc.Load(FILE_ALERT);

                    XmlNodeList xmlNodeList = doc.SelectNodes("alerts/alert[not(../alert/id > id)]/id");

                    //MessageBox.Show(xmlNodeList[0].OuterXml);

                    contador = Convert.ToInt16(xmlNodeList[0].InnerXml);

                    contador++; //para començar pelo seguinte ID
                }catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
        

            #endregion
            InitializeComponent();
            //comboBoxOperacao.DataSource = Enum.GetNames(typeof(Operacoes));
            comboBoxOperacao.DataSource = operacoes;
            numericUpDownValor2.Visible = false;
            comboBoxType.DataSource = Enum.GetNames(typeof(Tipos));

            #region atualizarListas
            GetListOfAlertasAtivas();
            GetListOfAlertasDesativas();
            listBoxCondicoesAtivas.DataSource = alertasAtivas;
            listBoxCondicoesDesativas.DataSource = alertasDesativas;
            AtualizarListaAlertas();
            #endregion

            #region Compare Alerts with msg
            client = new MqttClient(BROKENDOMAIN);

            client.Connect(Guid.NewGuid().ToString());
            if (!client.IsConnected)
            {
                MessageBox.Show("Error connecting to message broker ...");
                return;
            }
            else
            {
                MessageBox.Show("Connection to broke ok...");
                
            }


            byte[] qosLevels = {MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE};

            client.Subscribe(topicsSubscriber, qosLevels);

            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            #endregion
        }
        private void GetListOfAlertasAtivas()
        {
            alertasAtivas = new List<Alerta>();
            if (File.Exists(FILE_ALERT))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FILE_ALERT);

                XmlNodeList alertsA = doc.SelectNodes("/alerts/alert[@ativo ='True']");
                foreach (XmlNode item in alertsA)
                {
                    //criar instancia de book y adicionar a lista
                    Alerta a = new Alerta
                    {
                        Id = Convert.ToInt16(item["id"].InnerText),
                        Tipo = item["tipo"].InnerText,
                        Operacao = item["operacao"].InnerText,
                        Valor1 = double.Parse(item["valor1"].InnerText, NumberFormatInfo.InvariantInfo),
                        Valor2 = double.Parse(item["valor2"].InnerText, NumberFormatInfo.InvariantInfo),
                        Ativo = true
                    };
                    alertasAtivas.Add(a);
                }
            }
            return ;
        }
        private void GetListOfAlertasDesativas()
        {
            alertasDesativas = new List<Alerta>();
            if (File.Exists(FILE_ALERT))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FILE_ALERT);

                XmlNodeList alertsD = doc.SelectNodes("/alerts/alert[@ativo ='False']");
                foreach (XmlNode item in alertsD)
                {
                    //criar instancia de book y adicionar a lista
                    Alerta a = new Alerta
                    {
                        Id = Convert.ToInt16(item["id"].InnerText),
                        Tipo = item["tipo"].InnerText,
                        Operacao = item["operacao"].InnerText,
                        Valor1 = double.Parse(item["valor1"].InnerText, NumberFormatInfo.InvariantInfo),
                        Valor2 = double.Parse(item["valor2"].InnerText, NumberFormatInfo.InvariantInfo),
                        Ativo = false
                    };
                    alertasDesativas.Add(a);
                }
            }
            return;
        }
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //*** so para testar (eliminar)
           string msg = Encoding.UTF8.GetString(e.Message);

            this.Invoke((MethodInvoker)delegate () {
                MessageBox.Show($"Received : {msg} on topic {e.Topic}\n");
                //codigo tiene que ser hecho aqui ?
                Sensor sensor = new Sensor(msg);                
            });
           
            //***
            //ex: msg
            /*{"id": 1, "temp": 22.489999771118164, "hum": 47.290000915527344, "batt": 100, "time": 1575058625, "sensors": ["temp", "hum"]}*/
            //comparar msg com alerta 
            

        }
        private void compararDados(Sensor sensor)
        {

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
            double valor1Alerta = (double)numericUpDownValor1.Value;
            double valor2Alerta = (double)numericUpDownValor2.Value;
            String operacaoAlerta;
            switch (comboBoxOperacao.SelectedItem.ToString())
            {
                case ">":
                    operacaoAlerta = "MAIOR";
                    break;
                case "<":
                    operacaoAlerta = "MENOR";
                    break;
                case "=":
                    operacaoAlerta = "IGUAL";
                    break;
                default:
                    operacaoAlerta = "ENTRE";
                    break;
            }

            if(valor1Alerta == valor2Alerta && operacaoAlerta == "ENTRE")
            {
                MessageBox.Show("Error: escolha 2 valores diferentes para a operação *ENTRE*");
                return;
            }
            
            String tipoSensor = comboBoxType.SelectedItem.ToString();
            Alerta novaAlerta;
            if (operacaoAlerta == "ENTRE")
            {
            novaAlerta= new Alerta(contador,tipoSensor, operacaoAlerta, valor1Alerta, valor2Alerta, true);             
            }
            else
            {
            novaAlerta = new Alerta(contador,tipoSensor, operacaoAlerta, valor1Alerta);
            }
            contador++;

            #region adicionar alert num ficheiro XML
            /*string json = JsonConvert.SerializeObject(novaAlerta);

            StreamWriter streamWriter = File.AppendText(FILE_ALERT);
            streamWriter.WriteLine(json);
            streamWriter.Close();*/

            XmlDocument doc = new XmlDocument();
            if (!File.Exists(FILE_ALERT))
            {
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);

                XmlNode alertsNode = doc.CreateElement("alerts");
                doc.AppendChild(alertsNode);

                doc.Save(FILE_ALERT);
            }
            doc.Load(FILE_ALERT);

            XmlNode root = doc.SelectSingleNode("/alerts");

            //criar a <alert> fazer append child
            XmlElement alertElement = doc.CreateElement("alert");
            alertElement.SetAttribute("ativo", novaAlerta.Ativo.ToString());

            XmlElement id = doc.CreateElement("id");
            id.InnerText = novaAlerta.Id.ToString();

            XmlElement tipo = doc.CreateElement("tipo");
            tipo.InnerText = novaAlerta.Tipo;

            XmlElement operacao = doc.CreateElement("operacao");
            operacao.InnerText = novaAlerta.Operacao;

            XmlElement valor1 = doc.CreateElement("valor1");
            valor1.InnerText = novaAlerta.Valor1.ToString();

            XmlElement valor2 = doc.CreateElement("valor2");
            valor2.InnerText = novaAlerta.Valor2.ToString();

            alertElement.AppendChild(id);
            alertElement.AppendChild(tipo);
            alertElement.AppendChild(operacao);
            alertElement.AppendChild(valor1);
            alertElement.AppendChild(valor2);
            root.AppendChild(alertElement);

            doc.Save(FILE_ALERT);
            #endregion

            alertasAtivas.Add(novaAlerta);
            AtualizarListaAlertas();
        }

        private void comboBoxOperacao_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxOperacao.SelectedItem.ToString() == "ENTRE")
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
        #region ATIVAR e DESATIVAR ALERTAS
        private void buttonDesativar_Click(object sender, EventArgs e)
        {
            if (listBoxCondicoesAtivas.SelectedValue == null)
            {
                MessageBox.Show("Error Nenhum elemento selecionado ...");
                return;
            }

            mudarEstado(0);
            AtualizarListaAlertas();

        }
        
        private void buttonAtivar_Click(object sender, EventArgs e)
        {
            if (listBoxCondicoesDesativas.SelectedValue == null)
            {
                MessageBox.Show("Error Nenhum elemento selecionado ...");
                return;
            }

            mudarEstado(1);
            AtualizarListaAlertas();
        }
        #endregion
        private void mudarEstado(int aux)
        {
            try
            {
                Alerta alertaOld;
                if (aux == 0)
                {
                alertaOld = (Alerta)listBoxCondicoesAtivas.SelectedValue;
                }
                else
                {
                alertaOld = (Alerta)listBoxCondicoesDesativas.SelectedValue;
                }           
                Alerta alerta = alertaOld;
                alerta.Ativo = !alerta.Ativo;
                #region UpdateXMLAlert
                
                    XmlDocument doc = new XmlDocument();
                    doc.Load(FILE_ALERT);

                    XmlNode nodeAlerta = doc.SelectSingleNode($"/alerts/alert[id='{alerta.Id.ToString()}']");

                if (nodeAlerta != null)
                {
                    nodeAlerta.Attributes[0].Value = alerta.Ativo.ToString();
                        doc.Save(FILE_ALERT);
                    }

                #endregion

                if(alerta.Ativo == false)
                {
                alertasAtivas.Remove(alertaOld);
                alertasDesativas.Add(alerta);            
                }
                else
                {
                    alertasAtivas.Add(alerta);
                    alertasDesativas.Remove(alertaOld);
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: "+ e.Message);
                return;
            }
        }        

        private void buttonAtualizar_Click(object sender, EventArgs e)
        {
            AtualizarListaAlertas();
        }

        private void AtualizarListaAlertas()
        {
            listBoxCondicoesAtivas.DataSource = null;
            listBoxCondicoesDesativas.DataSource = null;
            listBoxCondicoesAtivas.DataSource = alertasAtivas;
            listBoxCondicoesDesativas.DataSource = alertasDesativas;
        }
       
    }
}
