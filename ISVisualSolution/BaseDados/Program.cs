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
        public static string[] topics = { "bridge","alerts" };
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
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
            mClient.Subscribe(topics, qosLevels);

            mClient.MqttMsgPublishReceived += MClient_MqttMsgPublishReceived;

            Console.ReadKey();
        }

        private static void MClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string msg = Encoding.UTF8.GetString(e.Message);
            Console.WriteLine($"Topico:{e.Topic}|Msg:{msg}");
            JObject response = JObject.Parse(msg);

            Console.WriteLine("json: " + response);
       
                //DADOS DO SENSORES
            switch (e.Topic)
            {
                case "bridge":
                    #region DADOS SENSORES Y LEITURAS 
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
                        cmd = new SqlCommand("SELECT UPPER(Type) as Result from Sensor_Type", connection);
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
                        SqlCommand sqlComd;
                        int resultado;
                        connection.Open();
                        #region CREAR TABELAS DE SER NECEARIO
                        int contador = 0;
                        foreach (string item in arrayTypes)
                        {
                            if (!listaTypes.Contains(item.ToUpper()))
                            {
                                string comdStr = $"CREATE TABLE {item} ([Id] INT   IDENTITY (1, 1) NOT NULL PRIMARY KEY,[Timestamp] BIGINT  NOT NULL, [Sensor_Id] INT NOT NULL,[{item}] FLOAT NOT NULL)";
                                sqlComd = new SqlCommand(comdStr, connection);
                                // sqlCommand.Parameters.AddWithValue("@name", item
                                resultado = sqlComd.ExecuteNonQuery();
                                sqlComd = new SqlCommand("INSERT INTO Sensor_Type (Type) VALUES (@type)", connection);
                                sqlComd.Parameters.AddWithValue("@type", item);
                                sqlComd.ExecuteNonQuery();
                                contador++;
                            }
                        }
                        Console.WriteLine($"TABELAS CRIADAS: {contador}");
                        #endregion
                        connection.Close();

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

                                foreach (string item in arrayTypes)
                                {
                                    float dado = (float)response[item];
                                    Console.WriteLine($"------------INSERIDA LEITURA NA TABELA {item.ToUpper()} COM SENSOR_ID:{id} TIMESTAMP:{time} {item}:{dado}----------------------");
                                    sqlComd = new SqlCommand($"INSERT INTO {item} (Sensor_Id,Timestamp,{item})VALUES(@id,@timestamp,@dado)", connection);
                                    sqlComd.Parameters.AddWithValue("@id", id);
                                    sqlComd.Parameters.AddWithValue("@timestamp", time);
                                    // sqlCommand.Parameters.AddWithValue("@name", item);
                                    sqlComd.Parameters.AddWithValue("@dado", dado);
                                    resultado = sqlComd.ExecuteNonQuery();
                                }
                                connection.Close();
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


                        //TRAER ULTIMA LEITURA DO SENSOR
                        connection.Open();
                        #region TRAER ULTIMA LEITURA TIMESTAMP
                        Console.WriteLine($"TIMESTAMP DO BROKER: {time}");
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
                            #endregion
                            int result = sqlCommand.ExecuteNonQuery();
                            if (result > 0)
                            {
                                Console.WriteLine($"------------ATUALIZADO COLUNA TIMESTAMP DO SENSOR ID:{id} TIMESTAMP:{time}----------------------");
                                #region INSERIR LEITURA
                                foreach (string item in arrayTypes)
                                {
                                    float dado = (float)response[item];
                                    Console.WriteLine($"------------INSERIDA LEITURA NA TABELA {item.ToUpper()} COM SENSOR_ID:{id} TIMESTAMP:{time} {item}:{dado}----------------------");
                                    sqlComd = new SqlCommand($"INSERT INTO {item} (Sensor_Id,Timestamp,{item})VALUES(@id,@timestamp,@dado)", connection);
                                    sqlComd.Parameters.AddWithValue("@id", id);
                                    sqlComd.Parameters.AddWithValue("@timestamp", time);
                                    // sqlCommand.Parameters.AddWithValue("@name", item);
                                    sqlComd.Parameters.AddWithValue("@dado", dado);
                                    resultado = sqlComd.ExecuteNonQuery();
                                }
                                #endregion
                                connection.Close();
                            }

                        }
                        else
                        {
                            Console.WriteLine($"------------LEITURA JA EXISTE ----------------------");
                            connection.Close();
                            return;
                        }

                    };
                    #endregion
                    break;
                case "alerts":
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        Console.WriteLine("-------------------------A ESCREVER NA TABELA ALERTS -----------------------------");
                        string tipo = (string)response["Tipo"];
                        string operacao = (string)response["Operacao"];
                        double valor1 = (double)response["Valor1"];
                        double valor2 = (double)response["Valor2"];
                        bool ativo = (bool)response["Ativo"];
                        int sensor_id=(int)response["Sensor_Id"];

                        SqlCommand sqlCommand = new SqlCommand("INSERT INTO Alerts (Tipo,Operacao,Valor1,Valor2,Ativo,Sensor_Id) VALUES(@Tipo,@Operacao,@Valor1,@Valor2,@Ativo,@sensor_id)", connection);
                        sqlCommand.Parameters.AddWithValue("@Tipo",tipo);
                        sqlCommand.Parameters.AddWithValue("@Operacao", operacao);
                        sqlCommand.Parameters.AddWithValue("@Valor1", valor1);
                        sqlCommand.Parameters.AddWithValue("@Valor2", valor2);
                        sqlCommand.Parameters.AddWithValue("@Ativo", ativo);
                        sqlCommand.Parameters.AddWithValue("@sensor_id", sensor_id);

                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    };
                        break;
                default:
                    break;
            }   
             
        }
    }
}
