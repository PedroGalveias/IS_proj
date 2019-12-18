using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceAlert" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceAlert.svc or ServiceAlert.svc.cs at the Solution Explorer and start debugging.
    public class ServiceAlert : IAlert
    {
        public static string connectionString = Properties.Settings.Default.ConnectionString;


        public Alert GetAlertsById(short id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            Alert alert = null;


            sqlConnection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Alerts WHERE WHERE Id=@id ", sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
               
                    int id =reader.GetInt32(0),
                    char tipo =reader.GetChars(1),
                    Operacao = char.Parse((reader["Operacao"]).ToString()),
                    Valor1 = float.Parse((reader["Valor1"]).ToString()),
                    Valor2 = float.Parse((reader["Valor2"]).ToString()),
                    SensorId = short.Parse((reader["SensorId"]).ToString())


               

            }
            reader.Close();
            sqlConnection.Close();
            return alert;

        }

        public List<Alert> GetAllAlerts()
        {
<<<<<<< Updated upstream
            throw new NotImplementedException();
        }

        public Sensor GetAlertsById(short id)
        {
            throw new NotImplementedException();
        }

        public List<Sensor> GetAllAlerts()
        {
            throw new NotImplementedException();
=======

            SqlConnection sqlConnection = new SqlConnection(connectionString);
          
            List<Alert> alerts = new List<Alert>();

            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Alerts", sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Alert alert = new Alert
                {
                    Id = short.Parse((reader["Id"]).ToString()),
                    Tipo = char.Parse((reader["Tipo"]).ToString()),
                    Operacao = char.Parse((reader["Operacao"]).ToString()),
                    Valor1 = float.Parse((reader["Valor1"]).ToString()),
                    Valor2 = float.Parse((reader["Valor2"]).ToString()),
                    SensorId = short.Parse((reader["SensorId"]).ToString())


                };
                alerts.Add(alert);

            }
            reader.Close();
            sqlConnection.Close();

            return alerts;


>>>>>>> Stashed changes
        }
      
    }
}
