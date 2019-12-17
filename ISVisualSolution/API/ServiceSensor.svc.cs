using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceSensor : ISensor
    {
        public static string connectionString = Properties.Settings.Default.ConnectionString;

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
                Sensor sensor = new Sensor
                {
                    Id = (short)reader["Id"],
                    Battery = (short)reader["Battery"],
                    Timestamp = (long)reader["Timestamp"]

                };
                sensors.Add(sensor);
            }
            reader.Close();
            sqlConnection.Close();


            return sensors;

        }

        public Sensor GetSensorById(short id)
        {

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            Sensor sensor = null;


            sqlConnection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Sensores WHERE UPPER(Id) = UPPER(@id)", sqlConnection);
            cmd.Parameters.AddWithValue("@id", id);
          
            SqlDataReader reader = cmd.ExecuteReader();
            

            while (reader.Read())
            {
                sensor = new Sensor
                {
                    Id = (short)reader["Id"],
                    Battery = (short)reader["Battery"],
                    Timestamp = (long)reader["Timestamp"],
                    Status = (ValueType)reader["Status"]

                };
               
            }
            reader.Close();
            sqlConnection.Close();

            return sensor;
            

        }

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
                    Status = (ValueType)reader["Status"]

                };

            }


            if (sensor.Status == ValueType.VALID)
            {
                sensor.Status = ValueType.INVALID;
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