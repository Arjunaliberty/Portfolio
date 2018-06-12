using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfGisService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGisService" in both code and config file together.
    [ServiceContract]
    public interface IGisService
    {
        [OperationContract]
        string GetAllCity();

        [OperationContract]
        string GetWeatherCity(int id);
    }
}
