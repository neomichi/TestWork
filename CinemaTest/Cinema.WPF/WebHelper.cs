using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Cinema.WPF
{
    public static class WebHelper
    {
        private static WebResponse GetResult(RestRequest restRequest)
        {
            var client=new RestClient("http://localhost:5000/api/");
            var response = client.Execute(restRequest);
          
            HttpStatusCode statusCode = response.StatusCode;


            return new WebResponse()
            {
                ErrorMessage = response.ErrorMessage,
                StatusCode = (int)statusCode,
                Response = response.Content,
            };
        }

        public static WebResponse GetData()
        {
            return GetResult(new RestRequest("spectators", Method.GET));
        }

        public static WebResponse SetPutData(Guid SeancesId,int QuantityTickets)
        {
           
            var request = new RestRequest("spectators", Method.PUT);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new { SeancesId, QuantityTickets });
            return GetResult(request);
        }
    }
}
