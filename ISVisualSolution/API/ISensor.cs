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
		bool UpdateSensor(short id);

		[OperationContract]
		List<Reading> GetLast150Readings(short sensorId);

		[OperationContract]
		Reading GetLatestReading(short sensorId);

		[OperationContract]
		Reading GetReading(short sensorId, long timestamp);
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
	}

	public enum ValueType
    {
        INVALID, VALID
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
