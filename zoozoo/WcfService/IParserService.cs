using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    [ServiceContract]
    public interface IParserService
    {
        [WebInvoke(UriTemplate = "/getpage/{page}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "GET")]
        [OperationContract]
        IEnumerable<Moneta> GetPage(string page);

        [WebInvoke(UriTemplate = "/getcount", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "GET")]
        [OperationContract]
        int GetCount();

        [WebInvoke(UriTemplate = "/test/{value}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,Method="GET")]
        [OperationContract]
        IEnumerable<string> GetData(string value);

        // TODO: Add your service operations here
    }

}
