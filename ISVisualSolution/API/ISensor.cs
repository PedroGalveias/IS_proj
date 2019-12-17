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
        void InvalidateSensor(short id);
              
        [OperationContract]
        void UpdateSensor(short id);


    }

	[DataContract]
	public class Sensor
	{
		[DataMember]
		public Dictionary<string, string> SensorType { get; }

		[DataMember]
		public short Id { get; set; }

		[DataMember]
		public short Battery { get; set; }

		[DataMember]
		public long Timestamp { get; set; }

        [DataMember]
        public ValueType Status { get; set; }
    }

    
    public enum ValueType
    {
        INVALID, VALID
    }
}
