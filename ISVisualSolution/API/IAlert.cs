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
    public interface IAlert
    {

        [OperationContract]
        List<Alert> GetAllAlerts();

        [OperationContract]
        Alert GetAlertsById(short id);
    }


    [DataContract]
    public class Alert
    {

        [DataMember]
        public short Id { get; set; }

        [DataMember]
        public Char Tipo { get; set; }
        [DataMember]
        public Char Operacao { get; set; }
        [DataMember]
     
        public float Valor1 { get; set; }
        [DataMember]
        public float Valor2 { get; set; }
        [DataMember]
        public short SensorId { get; set; }

    }

}