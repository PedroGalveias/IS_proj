using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace API
{
	[ServiceContract]
	public interface ISensor
	{
		[OperationContract]
		List<Sensor> GetAllSensors();

		[OperationContract]
		Sensor GetSensorById(short id);

        [OperationContract]
		bool InvalidateSensorReading(short sensorId, long timestamp);
              
        [OperationContract]
		bool UpdateSensor(short id, long timestamp, short battery, string location);

		[OperationContract]
		List<Reading> GetLast164Readings(short sensorId);

		[OperationContract]
		Reading GetLatestReading(short sensorId);

		[OperationContract]
		Reading GetReading(short sensorId, long timestamp);

		[OperationContract]
		bool CreatePersonalSensor(short battery, long timestamp, string location);

		[OperationContract]
		bool DeletePersonalSensor(short id);

		[OperationContract]
		List<Sensor> GetPersonalSensors();

		[OperationContract]
		bool AddReading(Reading reading);

		[OperationContract]
		List<Reading> GetReadingsBetween(short sensorId, long timestamp1, long timestamp2);

		[OperationContract]
		bool AddTempFromMobile(float temp, long timestamp, short battery, string location);

		[OperationContract]
		List<Sensor> GetSensorsByLocation(string location);
	}

	[DataContract]
	public class Sensor
	{
		[DataMember]
		public List<string> SensorTypes { get; set; }

		[DataMember]
		public short Id { get; set; }

		[DataMember]
		public short Battery { get; set; }

		[DataMember]
		public long Timestamp { get; set; }

		[DataMember]
		public string Location { get; set; }

		[DataMember]
		public SensorType Personal { get; set; }
	}

	public enum ValueType
    {
        INVALID, VALID
    }

	public enum SensorType
	{
		PERSONAL, NON_PERSONAL
	}

	public class Reading
	{
		[DataMember]
		public short SensorId { get; set; }

		[DataMember]
		public Dictionary<string, string> Readings { get; set; }

		[DataMember]
		public long Timestamp { get; set; }

		[DataMember]
		public ValueType Status { get; set; }
	}
}
