﻿using System;
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

        public Alert GetAlertsById(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Alerts WHERE UPPER(Id) = UPPER(@id)", sqlConnection);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            Alert alert = null;

            while (reader.Read())
            {


               
                string tipo = reader.GetString(1);
                string operacao = reader.GetString(2);
                float valor1 = reader.GetFloat(3);
                float valor2 = reader.GetFloat(4);
                int sensorId = reader.GetInt32(5);


                alert = new Alert
                {
                    Id = id,
                    Tipo = tipo,
                    Operacao = operacao,
                    Valor1 = valor1,
                    Valor2 = valor2,
                    SensorId = sensorId

                };


            }

            reader.Close();
            sqlConnection.Close();

            return alert;

        }

        public List<Alert> GetAllAlerts()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            // Sensor sensor = new Sensor();
            List<Alert> alerts = new List<Alert>();

            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Alerts", sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string tipo = reader.GetString(1);
                string operacao = reader.GetString(2);
                float valor1 = reader.GetFloat(3);
                float valor2 = reader.GetFloat(4);
                int sensorId = reader.GetInt32(5);
                


                Alert alert = new Alert
                {
                    Id = id,
                    Tipo = tipo,
                    Operacao = operacao,
                    Valor1 = valor1,
                    Valor2 = valor2,
                    SensorId = sensorId

                };

                alerts.Add(alert);
            }

            reader.Close();
            sqlConnection.Close();

            return alerts;
        }



     
    }
}
