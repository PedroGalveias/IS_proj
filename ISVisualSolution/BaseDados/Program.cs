using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace BaseDados
{

    class Program
    {
        public static string connectionString = Properties.Settings.Default.ConnectionString;
        public static string[] topics = { "news" };
        static void Main(string[] args)
        {
            Console.Write("Escreva um ip: ");
            string ip = Console.ReadLine();
            ip.Trim();
            ip = "127.0.0.1";
            Console.WriteLine("Seu ip é: " + ip);
            MqttClient mClient = new MqttClient(ip);
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            else
            {
                Console.WriteLine("Broker OK...");
            }
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
            mClient.Subscribe(topics, qosLevels);

            mClient.MqttMsgPublishReceived += MClient_MqttMsgPublishReceived;
          
            Console.ReadKey();
        }

        private static void MClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string msg = Encoding.UTF8.GetString(e.Message);
            Console.WriteLine($"Topico:{e.Topic}|Msg:{msg}");
            dynamic response=JsonConvert.DeserializeObject(msg);
            Console.WriteLine("json: " + response);
         
               using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                //int id = Int32.Parse(response.id);
                    int id = (int)response.id;
                    int battery = (int)response.batt;
                    long time = (long)response.time;
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO SENSORES (Id,Battery,Timestemp)VALUES(@id,@battery,@timestemp)", conn);
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlCommand.Parameters.AddWithValue("@battery", battery);
                    sqlCommand.Parameters.AddWithValue("@timestemp", time);


                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        conn.Close();
                        Console.WriteLine("------------------Dados inseridos nas tabelas-------------");
                        return;
                    }
                    else
                    {
                        conn.Close();
                        Console.WriteLine("ERRO dados nao inseridos em tabelas");
                        return;
                    }
                };
       

        }
    }
}
