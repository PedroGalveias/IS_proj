using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace API
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
	[ServiceContract]
	public interface IAlert
	{
		[OperationContract]
		List<Alert> GetAllAlerts();

		[OperationContract]
		Alert GetAlertsById(int id);
	}

	[DataContract]
	public class Alert
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public string Tipo { get; set; }

		[DataMember]
		public string Operacao { get; set; }

		[DataMember]
		public float Valor1 { get; set; }

		[DataMember]
		public float Valor2 { get; set; }

		[DataMember]
		public int SensorId { get; set; }
	}
}
