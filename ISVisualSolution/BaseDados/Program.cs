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

            //DADOS DO SENSORES
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //int id = Int32.Parse(response.id);
                int id = (int)response.id;
                int battery = (int)response.batt;
                long time = (long)response.time;



                //TRAER LOS SENSORES EXISTENTES Y DATOS
                #region SELECT SENSORES 
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Sensores", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                List<int> listaIdSensores = new List<int>();
                while (reader.Read())
                {
                    listaIdSensores.Add((int)reader["Id"]);
                }
                reader.Close();
                connection.Close();
                Console.WriteLine($"->Sensores na BD:{listaIdSensores.Count} ");
                #endregion
                //PERSISTIR UM NOVO SENSOR
                if (!listaIdSensores.Contains(id))
                {
                    connection.Open();
                    #region INSERT NOVOS SENSORES
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO SENSORES (Id,Battery,Timestamp)VALUES(@id,@battery,@timestamp)", connection);
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlCommand.Parameters.AddWithValue("@battery", battery);
                    sqlCommand.Parameters.AddWithValue("@timestamp", time);


                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        connection.Close();
                        Console.WriteLine($"------------------SENSOR NOVO INSERIDO com Id:{id}-------------");
                        return;
                    }
                    else
                    {
                        connection.Close();
                        Console.WriteLine("ERRO dados nao inseridos em tabelas");
                        return;
                    }

                    #endregion
                }
                else
                {
                    //TRAER ULTIMA LEITURA DO SENSOR
                    #region TRAER ULTIMA LEITURA TIMESTAMP
                    Console.WriteLine($"TIMESTAMP DO BROKER: {time}");
                    connection.Open();
                    cmd = new SqlCommand("SELECT Timestamp FROM Sensores WHERE Id=@id", connection);
                    cmd.Parameters.AddWithValue("id", id);
                    long timeMax = (long)cmd.ExecuteScalar();
                    Console.WriteLine($"TIMESTAMP DA BD: {timeMax}");
                    #endregion
                    if (time > timeMax)
                    {
                        //ATUALIZAR TIMESTAMP da tabela
                        #region ATUALIZAR TIMESTAMP da tabela Sensores
                        SqlCommand sqlCommand = new SqlCommand("UPDATE Sensores set Timestamp = @time WHERE id=@id", connection);
                        sqlCommand.Parameters.AddWithValue("@time", time);
                        sqlCommand.Parameters.AddWithValue("@id", id);
                        int result = sqlCommand.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Console.WriteLine($"------------ATUALIZADO COLUNA TIMESTAMP DO SENSOR ID:{id} TIMESTAMP:{time}----------------------");
                            return;
                        }
                        #endregion

                    }
                    else
                    {
                        Console.WriteLine($"------------LEITURA JA EXISTE ----------------------");
                        return;
                    }
                }
            };
        }
    }
}
