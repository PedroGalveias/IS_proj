using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

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
            JObject response=JObject.Parse(msg);
           
            Console.WriteLine("json: " + response);

            //DADOS DO SENSORES
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //int id = Int32.Parse(response.id);
                int id = (int)response["id"];
                int battery = (int)response["batt"];
                long time = (long)response["time"];
                string[] arrayTypes = response["sensors"].ToObject<string[]>();
              // Console.WriteLine(response[arr]);
                

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
                //TRAER DADOS DOS TYPES
                cmd = new SqlCommand("SELECT UPPER(Type) as Result from Sensor_Type",connection);
                reader = cmd.ExecuteReader();
                List<string> listaTypes = new List<string>();
                while (reader.Read())
                {
                    listaTypes.Add((string)reader["Result"]);
                }
                reader.Close();
                connection.Close();
                Console.WriteLine($"->Sensores na BD:{listaIdSensores.Count} ");
                Console.WriteLine($"Tabelas: {listaTypes.Count}");
                #endregion
                //PERSISTIR UM NOVO SENSOR
                if (!listaIdSensores.Contains(id))
                {
                    Console.WriteLine("ENTRO");

                    connection.Open();
                    #region INSERT NOVOS SENSORES
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO SENSORES (Id,Battery,Timestamp)VALUES(@id,@battery,@timestamp)", connection);
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlCommand.Parameters.AddWithValue("@battery", battery);
                    sqlCommand.Parameters.AddWithValue("@timestamp", time);


                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine($"------------------SENSOR NOVO INSERIDO com Id:{id}-------------");
                        #region CREAR TABELAS DE SER NECEARIO
                        foreach (string item in arrayTypes)
                        {
                            if (!listaTypes.Contains(item.ToUpper()))
                            {
                                string comdStr = $"CREATE TABLE {item} ([Timestamp] BIGINT  NOT NULL PRIMARY KEY, [Sensor_Id] INT NOT NULL,[{item}] FLOAT NOT NULL)";
                                sqlCommand = new SqlCommand(comdStr, connection);
                               // sqlCommand.Parameters.AddWithValue("@name", item
                                result = sqlCommand.ExecuteNonQuery();
                                sqlCommand = new SqlCommand("INSERT INTO Sensor_Type (Type) VALUES (@type)", connection);
                                sqlCommand.Parameters.AddWithValue("@type", item);
                                sqlCommand.ExecuteNonQuery();
                            }
                            float dado =(float) response[item];
                            Console.WriteLine($"------------INSERIDA LEITURA NA TABELA {item.ToUpper()} COM SENSOR_ID:{id} TIMESTAMP:{time} {result}:{dado}----------------------");
                            sqlCommand = new SqlCommand($"INSERT INTO {item} (Sensor_Id,Timestamp,{item})VALUES(@id,@timestamp,@dado)", connection);
                            sqlCommand.Parameters.AddWithValue("@id", id);
                            sqlCommand.Parameters.AddWithValue("@timestamp", time);
                           // sqlCommand.Parameters.AddWithValue("@name", item);
                            sqlCommand.Parameters.AddWithValue("@dado", dado);
                            result = sqlCommand.ExecuteNonQuery();
                          

                        }
                        connection.Close();
                        return;
                        #endregion
                    }
                    else
                    {
                        connection.Close();
                        Console.WriteLine("ERRO dados nao inseridos em tabelas");
                        return;
                    }

                    #endregion
                }
                /*
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
                            List<string> tabelas=new List<string>();
                            tabelas.Add("Temperatura");
                           
                            
                            #region INSERT DADOS TABELAS POR ENTANTO HUM e TEMP
                            Console.WriteLine($"------------INSERIDA LEITURA NA TABELA TEMPERATURE COM SENSOR_ID:{id} TIMESTAMP:{time} TEMP:{temp}----------------------");
                            sqlCommand = new SqlCommand("INSERT INTO TEMPERATURE (Sensor_Id,Timestamp,Temp)VALUES(@id,@timestamp,@temp)", connection);
                            sqlCommand.Parameters.AddWithValue("@id", id);
                            sqlCommand.Parameters.AddWithValue("@timestamp", time);
                            sqlCommand.Parameters.AddWithValue("@temp", temp);
                            result = sqlCommand.ExecuteNonQuery();
                            if (result <= 0)
                            {
                                connection.Close();
                                return;
                            }
                            Console.WriteLine($"------------INSERIDA LEITURA NA TABELA HUMIDITY COM SENSOR_ID:{id} TIMESTAMP:{time} HUM:{hum}----------------------");
                            sqlCommand = new SqlCommand("INSERT INTO HUMIDITY (Sensor_Id,Timestamp,Hum)VALUES(@id,@timestamp,@hum)", connection);
                            sqlCommand.Parameters.AddWithValue("@id", id);
                            sqlCommand.Parameters.AddWithValue("@timestamp", time);
                            sqlCommand.Parameters.AddWithValue("@hum", hum);
                            result = sqlCommand.ExecuteNonQuery();
                            connection.Close();
                            #endregion
                            return;

                        }
                        #endregion

                    }
                    else
                    {
                        Console.WriteLine($"------------LEITURA JA EXISTE ----------------------");
                        return;
                    }
                }*/
                
            };
        }
    }
}
