using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace API
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
	public class ServiceSensor : ISensor
	{
		public static string connectionString = Properties.Settings.Default.ConnectionString;

		public List<Reading> GetLast164Readings(short sensorId)
		{
			List<Reading> readings = new List<Reading>();

			List<string> sensorTypes = GetSensorTypes();

			if (sensorTypes == null)
			{
				return null;
			}

			string attributes = "t1.Reading, ";
			string from = $"{ sensorTypes[0] } t1";
			string where = "t1.Status = 1";
			string join = "";
			int counter = 1;

			foreach (string type in sensorTypes)
			{
				if (counter == 1)
				{
					counter++;
					continue;
				}

				from += $" JOIN { type } t{ counter } ON  (t{ counter - 1 }.Sensor_Id = t{ counter }.Sensor_Id AND t{ counter - 1 }.Timestamp = t{ counter }.Timestamp)";
				attributes += $"t{ counter }.Reading, ";
				where += $" AND t{ counter }.Status = 1";
				counter++;
			}

			string select = $"SELECT t1.Sensor_Id, { attributes } t1.Timestamp FROM { from } WHERE { where } AND t1.Sensor_Id = @sensorId";

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				SqlCommand cmd = new SqlCommand(select, sqlConnection);
				cmd.Parameters.AddWithValue("@sensorId", sensorId);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					int read_counter = 0;

					while (reader.Read())
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

						Reading reading = new Reading
						{
							SensorId = _sensorId,
							Readings = _readings,
							Timestamp = _timestamp,
							Status = ValueType.VALID
						};

						readings.Add(reading);

						if (read_counter >= 164)
						{
							break;
						}
					}
				}
			}

			return readings;
		}

		public List<Sensor> GetAllSensors()
		{
			List<Sensor> sensors = new List<Sensor>();

			List<string> sensorTypes = GetSensorTypes();

			if (sensorTypes == null)
			{
				return null;
			}

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				SqlCommand cmd = new SqlCommand("SELECT * FROM Sensores", sqlConnection);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						short id = reader.GetInt16(0);
						short battery = reader.GetInt16(1);
						long timestamp = reader.GetInt64(2);
						string location = reader.GetSqlString(3).ToString();
						short _personal = reader.GetInt16(4);

						if (location == "Null")
						{
							location = "";
						}

						SensorType personal;
						if (_personal == 1)
						{
							personal = SensorType.PERSONAL;
						}
						else
						{
							personal = SensorType.NON_PERSONAL;
						}

						Sensor sensor = new Sensor
						{
							Id = id,
							Battery = battery,
							Timestamp = timestamp,
							SensorTypes = sensorTypes,
							Location = location,
							Personal = personal
						};
						sensors.Add(sensor);
					}
				}
			}

			return sensors;
		}

		public Reading GetLatestReading(short sensorId)
		{
			Sensor sensor = GetSensorById(sensorId);

			return GetReading(sensor.Id, sensor.Timestamp);
		}

		public Reading GetReading(short sensorId, long timestamp)
		{
			Reading reading = null;
			List<string> sensorTypes = GetSensorTypes();

			if (sensorTypes == null)
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

			string select = $"SELECT t1.Sensor_Id, { attributes } t1.Timestamp, t1.Status FROM { from } Sensores s WHERE t1.Sensor_Id = @sensorId AND t1.Timestamp = @timestamp";

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				SqlCommand cmd = new SqlCommand(select, sqlConnection);
				cmd.Parameters.AddWithValue("@sensorId", sensorId);
				cmd.Parameters.AddWithValue("@timestamp", timestamp);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					reader.Read();

					short _sensorId = reader.GetInt16(0);
					Dictionary<string, string> _readings = new Dictionary<string, string>();

					counter = 1;
					foreach (string type in sensorTypes)
					{
						float _float = (float)reader.GetSqlDouble(counter++).Value;
						_readings.Add(type, _float.ToString());
					}

					long _timestamp = reader.GetInt64(counter++);
					ValueType _status = reader.GetInt16(counter++) == 1 ? ValueType.VALID : ValueType.INVALID;

					reading = new Reading
					{
						SensorId = _sensorId,
						Readings = _readings,
						Timestamp = _timestamp,
						Status = _status
					};
				}
			}

			return reading;
		}

		private List<string> GetSensorTypes()
		{
			List<string> sensorTypes = new List<string>();

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				SqlCommand cmd = new SqlCommand("SELECT type FROM dbo.Sensor_Type", sqlConnection);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						sensorTypes.Add(reader.GetString(0));
					}
				}
			}

			if (sensorTypes.Count() == 0)
			{
				return null;
			}

			return sensorTypes;
		}

		public Sensor GetSensorById(short id)
		{
			Sensor sensor = null;

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				List<string> sensorTypes = GetSensorTypes();

				if (sensorTypes == null)
				{
					return null;
				}

				SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Sensores WHERE Id = @id", sqlConnection);
				cmd.Parameters.AddWithValue("@id", id);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					reader.Read();

					short battery = reader.GetInt16(1);
					long timestamp = reader.GetInt64(2);
					string location = reader.GetSqlString(3).ToString();
					short _personal = reader.GetInt16(4);

					if (location == "Null")
					{
						location = "";
					}

					SensorType personal;
					if (_personal == 1)
					{
						personal = SensorType.PERSONAL;
					}
					else
					{
						personal = SensorType.NON_PERSONAL;
					}

					sensor = new Sensor
					{
						Id = id,
						Battery = battery,
						Timestamp = timestamp,
						SensorTypes = sensorTypes,
						Location = location,
						Personal = personal
					};
				}
			}

			return sensor;
		}

		public bool InvalidateSensorReading(short sensorId, long timestamp)
		{
			Reading reading = GetReading(sensorId, timestamp);

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				foreach (KeyValuePair<string, string> r in reading.Readings)
				{
					SqlCommand cmd = new SqlCommand("UPDATE @Key SET Status = 0 WHERE Sensor_Id = @SensorId AND Timestamp = @Timestamp", sqlConnection);
					cmd.Parameters.AddWithValue("@Key", r.Key);
					cmd.Parameters.AddWithValue("@SensorId", reading.SensorId);
					cmd.Parameters.AddWithValue("@Timestamp", reading.Timestamp);

					int result = cmd.ExecuteNonQuery();

					if (result <= 0)
					{
						return false;
					}
				}

				return true;
			}
		}

		public bool UpdateSensor(short id, long timestamp, short battery, string location)
		{
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				Sensor sensor = GetSensorById(id);

				SqlCommand cmd = new SqlCommand("UPDATE dbo.Sensores SET Timestamp = @timestamp, Battery = @battery, Location = @location WHERE Id = @sensorId", sqlConnection);
				cmd.Parameters.AddWithValue("@timestamp", timestamp);
				cmd.Parameters.AddWithValue("@battery", battery);
				cmd.Parameters.AddWithValue("@location", location);
				cmd.Parameters.AddWithValue("@sensorId", sensor.Id);

				int result = cmd.ExecuteNonQuery();

				if (result <= 0)
				{
					return false;
				}

				return true;
			}

		}

		public bool CreatePersonalSensor(short battery, long timestamp, string location)
		{
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("SELECT MAX(Id) FROM Sensores", sqlConnection);

				short id = 0;

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					reader.Read();

					id = reader.GetInt16(0);
				}

				if (id <= 0)
				{
					return false;
				}

				id++;

				cmd = new SqlCommand("INSERT INTO SENSORES (Id,Battery,Timestamp,Location,Personal)VALUES(@id,@battery,@timestamp,@location,1)", sqlConnection);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.Parameters.AddWithValue("@battery", battery);
				cmd.Parameters.AddWithValue("@timestamp", timestamp);
				cmd.Parameters.AddWithValue("@location", location);

				int result = cmd.ExecuteNonQuery();
				if (result > 0)
				{
					return false;
				}

				return true;
			}
		}

		public bool DeletePersonalSensor(short id)
		{
			Sensor sensor = GetSensorById(id);

			if (sensor.Personal == SensorType.NON_PERSONAL)
			{
				return false;
			}

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				SqlCommand cmd = new SqlCommand("DELETE FROM Sensores WHERE Id = @Id AND Personal = 1", sqlConnection);
				cmd.Parameters.AddWithValue("@Id", sensor.Id);

				int result = cmd.ExecuteNonQuery();

				if (result > 0)
				{
					return false;
				}
			}

			return true;
		}

		public List<Sensor> GetPersonalSensors()
		{
			List<Sensor> sensors = new List<Sensor>();

			List<string> sensorTypes = GetSensorTypes();

			if (sensorTypes == null)
			{
				return null;
			}

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				SqlCommand cmd = new SqlCommand("SELECT * FROM Sensores WHERE Personal = 1", sqlConnection);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						short id = reader.GetInt16(0);
						short battery = reader.GetInt16(1);
						long timestamp = reader.GetInt64(2);
						string location = reader.GetSqlString(3).ToString();
						short _personal = reader.GetInt16(4);

						if (location == "Null")
						{
							location = "";
						}

						SensorType personal;
						if (_personal == 1)
						{
							personal = SensorType.PERSONAL;
						}
						else
						{
							personal = SensorType.NON_PERSONAL;
						}

						Sensor sensor = new Sensor
						{
							Id = id,
							Battery = battery,
							Timestamp = timestamp,
							SensorTypes = sensorTypes,
							Location = location,
							Personal = personal
						};

						sensors.Add(sensor);
					}
				}
			}

			return sensors;
		}

		public bool AddReading(Reading reading)
		{
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				foreach (KeyValuePair<string, string> r in reading.Readings)
				{
					SqlCommand cmd = new SqlCommand("SELECT MAX(Id) FROM @table", sqlConnection);
					cmd.Parameters.AddWithValue("@table", r.Key);

					short id = 0;

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						reader.Read();

						id = reader.GetInt16(0);
					}

					if (id <= 0)
					{
						return false;
					}

					id++;

					cmd = new SqlCommand($"INSERT INTO @table (Sensor_Id,Timestamp,Reading)VALUES(@id,@timestamp,@dado)", sqlConnection);
					cmd.Parameters.AddWithValue("@table", r.Key);
					cmd.Parameters.AddWithValue("@id", id);
					cmd.Parameters.AddWithValue("@timestamp", reading.Timestamp);
					cmd.Parameters.AddWithValue("@dado", r.Value);

					int result = cmd.ExecuteNonQuery();

					if (result > 0)
					{
						return false;
					}
				}
			}

			return true;
		}

		public List<Reading> GetReadingsBetween(short sensorId, long timestamp1, long timestamp2)
		{
			List<Reading> readings = new List<Reading>();

			List<string> sensorTypes = GetSensorTypes();

			if (sensorTypes == null)
			{
				return null;
			}

			string attributes = "t1.Reading, ";
			string from = $"{ sensorTypes[0] } t1";
			string where = "t1.Status = 1";
			int counter = 1;

			foreach (string type in sensorTypes)
			{
				if (counter == 1)
				{
					counter++;
					continue;
				}

				from += $" JOIN { type } t{ counter } ON  (t{ counter - 1 }.Sensor_Id = t{ counter }.Sensor_Id AND t{ counter - 1 }.Timestamp = t{ counter }.Timestamp)";
				attributes += $"t{ counter }.Reading, ";
				where += $" AND t{ counter }.Status = 1";
				counter++;
			}

			string select = $"SELECT t1.Sensor_Id, { attributes } t1.Timestamp FROM { from } WHERE { where } AND t1.Sensor_Id = @sensorId AND (t1.Timestamp > @timestamp1 AND t1.Timestamp < @timestamp2)";

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				SqlCommand cmd = new SqlCommand(select, sqlConnection);
				cmd.Parameters.AddWithValue("@sensorId", sensorId);
				cmd.Parameters.AddWithValue("@timestamp1", timestamp1);
				cmd.Parameters.AddWithValue("@timestamp2", timestamp2);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					int read_counter = 0;

					while (reader.Read())
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

						Reading reading = new Reading
						{
							SensorId = _sensorId,
							Readings = _readings,
							Timestamp = _timestamp,
							Status = ValueType.VALID
						};

						readings.Add(reading);

						if (read_counter >= 164)
						{
							break;
						}
					}
				}
			}

			return readings;
		}

		public bool AddTempFromMobile(float temp, long timestamp, short battery, string location)
		{
			if (!CreatePersonalSensor(battery, timestamp, location))
			{
				return false;
			}

			List<Sensor> sensors = GetSensorsByLocation(location);

			Sensor sensor = null;

			foreach (Sensor s in sensors)
			{
				if (s.Battery == battery && s.Timestamp == timestamp)
				{
					sensor = s;
					break;
				}
			}

			if (sensor == null)
			{
				return false;
			}

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				SqlCommand cmd = new SqlCommand("SELECT MAX(Sensor_Id) FROM temp", sqlConnection);

				short id = 0;

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					reader.Read();

					id = reader.GetInt16(0);
				}

				if (id <= 0)
				{
					return false;
				}

				id++;

				cmd = new SqlCommand("INSERT INTO temp (Sensor_Id, Timestamp, Reading)VALUES(@id, @timestamp, @dado)", sqlConnection);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.Parameters.AddWithValue("@timestamp", timestamp);
				cmd.Parameters.AddWithValue("@dado", temp);

				int result = cmd.ExecuteNonQuery();

				if (result > 0)
				{
					return false;
				}
			}

			return true;
		}

		public List<Sensor> GetSensorsByLocation(string location)
		{
			List<Sensor> sensors = new List<Sensor>();

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();

				List<string> sensorTypes = GetSensorTypes();

				if (sensorTypes == null)
				{
					return null;
				}

				SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Sensores WHERE UPPER(Location) = UPPER(@location)", sqlConnection);
				cmd.Parameters.AddWithValue("@location", location);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						short id = reader.GetInt16(0);
						short battery = reader.GetInt16(1);
						long timestamp = reader.GetInt64(2);
						short _personal = reader.GetInt16(4);

						if (location == "Null")
						{
							location = "";
						}

						SensorType personal;
						if (_personal == 1)
						{
							personal = SensorType.PERSONAL;
						}
						else
						{
							personal = SensorType.NON_PERSONAL;
						}

						Sensor sensor = new Sensor
						{
							Id = id,
							Battery = battery,
							Timestamp = timestamp,
							SensorTypes = sensorTypes,
							Location = location,
							Personal = personal
						};

						sensors.Add(sensor);
					}
				}
			}

			return sensors;
		}
	}
}