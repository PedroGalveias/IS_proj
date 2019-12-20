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

        /**
         * *** TODO ***
         * GET SENSOR BY LOCATION
         * UPDATE SENSOR
         * PERSONAL SENSORS
         * *** MADE WITH ❤ BY PETER ***
         * *** UwU ***
         */

        public List<Reading> GetLast150Readings(short sensorId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            List<String> sensorTypes = new List<string>();
            List<Reading> readings = new List<Reading>();

            SqlCommand cmd = new SqlCommand("SELECT type FROM dbo.Sensor_Type", sqlConnection);

            sqlConnection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                sensorTypes.Add(reader.GetString(0));
            }

            reader.Close();

            if (sensorTypes.Count() == 0)
            {
                return null;
            }

            string attributes = "";
            string from = "";
            int counter = 1;

            foreach (string type in sensorTypes)
            {
                from += $"{ type } t{ counter }, ";
                attributes += $"t{ counter }.Reading, ";
                counter++;
            }

            string select = $"SELECT t1.Sensor_Id, { attributes } t1.Timestamp, t1.Status FROM { from } Sensores s WHERE t1.Sensor_Id = { sensorId }";

            cmd = new SqlCommand(select, sqlConnection);
            reader = cmd.ExecuteReader();

            int read_counter = 0;

            while(reader.Read())
            {
                read_counter++;

                short _sensorId = reader.GetInt16(0);
                Dictionary<string, string> _readings = new Dictionary<string, string>();

                counter = 1;
                foreach (string type in sensorTypes)
                {
                    float _float = (float)reader.GetSqlDouble(counter++).Value;
                    _readings.Add(type, _float.ToString());
                }

                long _timestamp = reader.GetInt64(counter++);

                // Int16 aux = (Int16)Convert.ToInt16((Int16)reader.GetSqlValue(counter++));

                short aux = (short)reader.GetSqlInt16(counter++).Value;

                bool _status = aux == 1 ? true : false; 

                Reading reading = new Reading
                {
                    SensorId = _sensorId,
                    Readings = _readings,
                    Timestamp = _timestamp,
                    Status = _status
                };

                readings.Add(reading);

                if (read_counter >= 150)
                {
                    break;
                }
            }

            reader.Close();

            return readings;
        }

        public List<Sensor> GetAllSensors()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            // Sensor sensor = new Sensor();
            List<Sensor> sensors = new List<Sensor>();

            sqlConnection.Open();

            List<String> sensorTypes = new List<string>();
            SqlCommand cmd = new SqlCommand("SELECT type FROM dbo.Sensor_Type", sqlConnection);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                sensorTypes.Add(reader.GetString(0));
            }

            reader.Close();

            if (sensorTypes.Count() == 0)
            {
                return null;
            }

            cmd = new SqlCommand("SELECT * FROM Sensores", sqlConnection);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                short id = reader.GetInt16(0);
                short battery = reader.GetInt16(1);
                long timestamp = reader.GetInt64(2);


                Sensor sensor = new Sensor
                {
                    Id = id,
                    Battery = battery,
                    Timestamp = timestamp,
                    SensorTypes = sensorTypes
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
            return GetReading(sensor.Id, sensor.Timestamp);
        }

        public Reading GetReading(short sensorId, long timestamp)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            List<String> sensorTypes = new List<string>();
            Reading reading = null;

            SqlCommand cmd = new SqlCommand("SELECT type FROM dbo.Sensor_Type", sqlConnection);

            sqlConnection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                sensorTypes.Add(reader.GetString(0));
            }

            reader.Close();

            if (sensorTypes.Count() == 0)
            {
                return null;
            }

            string attributes = "";
            string from = "";
            int counter = 1;

            foreach (string type in sensorTypes)
            {
                from += $"{ type } t{ counter }, ";
                attributes += $"t{ counter }.Reading, ";
                counter++;
            }

            string select = $"SELECT t1.Sensor_Id, { attributes } t1.Timestamp, t1.Status FROM { from } Sensores s WHERE t1.Sensor_Id = { sensorId } AND t1.Timestamp = { timestamp }";

            cmd = new SqlCommand(select, sqlConnection);
            reader = cmd.ExecuteReader();
            reader.Read();

            short _sensorId = reader.GetInt16(0);
            Dictionary<string, string> _readings = new Dictionary<string, string>();

            counter = 1;
            foreach (string type in sensorTypes)
            {
                float _float = (float) reader.GetSqlDouble(counter++).Value;
                _readings.Add(type, _float.ToString());
            }

            long _timestamp = reader.GetInt64(counter++);
            ValueType _status = reader.GetInt16(counter++) == 1 ? ValueType.VALID : ValueType.INVALID;

            reader.Close();

            reading = new Reading
            {
                SensorId = _sensorId,
                Readings = _readings,
                Timestamp = _timestamp,
                Status = _status
            };

            return reading;
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

            if (sensorTypes.Count() == 0)
            {
                return null;
            }

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

        public bool InvalidateSensorReading(short sensorId, long timestamp)
        {
            Reading reading = GetReading(sensorId, timestamp);

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            foreach (KeyValuePair<string, string> r in reading.Readings)
            {
                string select = $"UPDATE { r.Key } SET Status = 0 WHERE Sensor_Id = { reading.SensorId } AND Timestamp = { reading.Timestamp }";

                SqlCommand cmd = new SqlCommand(select, sqlConnection);
                int result = cmd.ExecuteNonQuery();

                if (result <= 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool UpdateSensor(short id, long timestamp, short battery, string location)
        {
            //long time = (long)response["time"];
            
           //primeir: pegar o timestamp que se pretende atu
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                
                Sensor sensor = GetSensorById(id);
                
                string select = $"UPDATE dbo.Sensores SET Timestamp = {timestamp}, Battery ={battery}, Location ={location} WHERE {sensor.Id}";

                    SqlCommand cmd = new SqlCommand(select, sqlConnection);
                    int result = cmd.ExecuteNonQuery();
                    if (result <= 0)
                    {
                        return false;
                    }
                return true;

            }

        }


    }
}