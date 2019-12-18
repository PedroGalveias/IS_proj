using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json.Linq;

namespace API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceSensor : ISensor
    {
        public static string connectionString = Properties.Settings.Default.ConnectionString;

        public List<Reading> GetAllReadings(short sensorId)
        {
            throw new NotImplementedException();
        }

        public List<Sensor> GetAllSensors()
        {

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            // Sensor sensor = new Sensor();
            List<Sensor> sensors = new List<Sensor>();


            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Sensores", sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                int battery = reader.GetInt32(1);
                long timestamp = reader.GetInt64(2);


                Sensor sensor = new Sensor
                {
                    Id = id,
                    Battery = battery,
                    Timestamp = timestamp

                };
                sensors.Add(sensor);
            }
           
            reader.Close();
            sqlConnection.Close();

            return sensors;
        }

        public Reading GetLatestReading(short sensorId)
        {
            Sensor sensor = GetSensorById(sensorId);

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            List<String> sensorTypes = new List<string>();
            Reading reading = null;

            SqlCommand cmd = new SqlCommand("SELECT type FROM dbo.Sensor_Type", sqlConnection);

            sqlConnection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                sensorTypes.Add(reader.GetString(0));
            }

            cmd = new SqlCommand("SELECT Sensor_Id, reading FROM @table", sqlConnection);

            throw new NotImplementedException();
        }

        public Sensor GetSensorById(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            Sensor sensor = null;

            sqlConnection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Sensores WHERE Id = @id", sqlConnection);
            cmd.Parameters.AddWithValue("@id", id);
            
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

               
                int battery = reader.GetInt32(1);
                long timestamp = reader.GetInt64(2);


                sensor = new Sensor
                {
                    Id = id,
                    Battery = battery,
                    Timestamp = timestamp

                };


            }

            reader.Close();
            sqlConnection.Close();

            return sensor;
            
        }

        public void InvalidateSensor(int id)
        {
            throw new NotImplementedException();
        }

        /*
        public void InvalidateSensor(short id)
        {
           SqlConnection sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           Sensor sensor = null;


           SqlCommand cmd = new SqlCommand("SELECT status FROM Sensores WHERE UPPER(Id) = UPPER(@id)", sqlConnection);
           cmd.Parameters.AddWithValue("@id", id);
           SqlDataReader reader = cmd.ExecuteReader();


           while (reader.Read())
           {
               sensor = new Sensor
               {
                   Id = (short)reader["Id"],
                   Battery = (short)reader["Battery"],
                   Timestamp = (long)reader["Timestamp"],
                   // Status = (ValueType)reader["Status"]

               };

           }


           if (sensor.Status == ValueType.VALID)
           {
               sensor.Status = ValueType.INVALID;
           }

           reader.Close();
           sqlConnection.Close();


        }
        */

        public void UpdateSensor(int id)
        {
            throw new NotImplementedException();
        }
    }
}