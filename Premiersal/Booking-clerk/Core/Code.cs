using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Booking_clerk.Core
{
    public static class Code
    {
        public static byte[] GetHtml(string url)
        {
            using (var wc = new WebClient())
            {
                return wc.DownloadData(url);

            }
        }
        public static string PostRequest(string url, string data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url.ToString());
            request.Method = "POST";
            request.ContentType = "application/json";

            UTF8Encoding utfenc = new UTF8Encoding();
            byte[] bytes = utfenc.GetBytes(data.ToString());
            Stream os = null;

            request.ContentLength = bytes.Length;
            os = request.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);
            var result = string.Empty;
            try
            {
                var webResponse = (HttpWebResponse)request.GetResponse();
                var responseStream = webResponse.GetResponseStream();
                var st = (int) webResponse.StatusCode;
                using (var responseStreamReader = new StreamReader(responseStream))
                {
                    result = st.ToString();
                }
            }
            catch
            {
            }

            return result;

        }


    }
}
