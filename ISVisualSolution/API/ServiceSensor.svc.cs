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
            SqlCommand cmd = new SqlCommand("SELECT * FROM Sensores", sqlConnection);
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


            return reading;
        }

        public Reading GetReading(short sensorId, long timestamp)
        {
            throw new NotImplementedException();
        }

        public Sensor GetSensorById(short id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            Sensor sensor = null;

            sqlConnection.Open();

            List<String> sensorTypes = new List<string>();
            SqlCommand cmd = new SqlCommand("SELECT type FROM dbo.Sensor_Type", sqlConnection);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                sensorTypes.Add(reader.GetString(0));
            }

            reader.Close();
            cmd = new SqlCommand("SELECT * FROM dbo.Sensores WHERE Id = @id", sqlConnection);
            cmd.Parameters.AddWithValue("@id", id);

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                short battery = reader.GetInt16(1);
                long timestamp = reader.GetInt64(2);

                sensor = new Sensor
                {
                    Id = id,
                    Battery = battery,
                    Timestamp = timestamp,
                    SensorTypes = sensorTypes
                };
            }

            reader.Close();
            sqlConnection.Close();

            return sensor;
            
        }
       

        public void InvalidateSensorReading(short sensorId, long timeStamp)
        {
           SqlConnection sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           Sensor sensor = GetSensorById(sensorId);


           //SqlCommand cmd = new SqlCommand("SELECT status FROM Sensores WHERE UPPER(Id) = UPPER(@id)", sqlConnection);
           //cmd.Parameters.AddWithValue("@id", sensorId);
           //SqlDataReader reader = cmd.ExecuteReader();

           List<String> sensorTypes = new List<string>();
           Reading reading = null;

           SqlCommand cmd = new SqlCommand("SELECT type FROM dbo.Sensor_Type", sqlConnection);
           SqlDataReader reader = cmd.ExecuteReader();
           sqlConnection.Open();

           reader = cmd.ExecuteReader();

           while (reader.Read())
           {
               sensorTypes.Add(reader.GetString(0));
           }
           
           

           reader.Close();
           sqlConnection.Close();

        }

        public void UpdateSensor(short id)
        {
            throw new NotImplementedException();
        }
    }
}