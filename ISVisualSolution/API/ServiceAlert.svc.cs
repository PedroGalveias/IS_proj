using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceAlert" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceAlert.svc or ServiceAlert.svc.cs at the Solution Explorer and start debugging.
    public class ServiceAlert : IAlert
    {
        public void DoWork()
        {
            throw new NotImplementedException();
        }

        public Sensor GetAlertsById(short id)
        {
            throw new NotImplementedException();
        }

        public List<Sensor> GetAllAlerts()
        {
            throw new NotImplementedException();
        }
    }
}
