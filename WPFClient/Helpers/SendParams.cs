using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Helpers
{
    public class SendParams : AuthenticateParams
    {
        public SendParams(string url, string jsonContent, string method
            , string contentType = "application/json")
        {
            Url = url;
            JsonContent = jsonContent;
            Method = method;
            ContentType = contentType;
        }

        public string Url { get; set; }

        public string JsonContent { get; set; }

        public string Method { get; set; }

        public string ContentType { get; set; }


    }
    public class AuthenticateParams
    {
        public List<Params> Params { get; set; }
    }
    public class Params
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
