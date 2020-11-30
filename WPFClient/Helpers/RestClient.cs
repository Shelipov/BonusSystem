using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Helpers
{
    public class RestClient
    {
        private readonly SendParams param;

        public string Response { get; set; }

        public string ExeptionMessage { get; set; }

        public RestClient(SendParams _param)
        {
            param = _param;
        }

        public async Task Post()
        {
            ExeptionMessage = null;
            string url = param.Url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = param.Method;



            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(param.JsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = param.ContentType;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                var response = (HttpWebResponse)await request.GetResponseAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ExeptionMessage = ExeptionMessage + $"\n\n\n request.URL : {url},JsonContent: {param.JsonContent}  " +
                        $"\n\n\n StatusCode : {response.StatusCode},Server : {response.Server},Method : {response.Method} \n\n\n";
                }
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        Response = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                var stack = (new StackTrace(ex, true)).GetFrame(0);
                ExeptionMessage = ExeptionMessage + $"\n\n\n request.URL : {url},,JsonContent: {param.JsonContent}  " +
                $"\n\n\n Exeption in File : {stack.GetFileName()} ; \n\n\n Line : {stack.GetFileLineNumber()} ; \n\n\n Message : {ex.Message} \n\n\n";
            }
        }

        public async Task Get()
        {
            ExeptionMessage = null;
            string url = param.Url;

            if (param.Params != null && param.Params.Count > 0)
            {
                string _param = "?";
                var flag = true;
                foreach (var p in param.Params)
                {
                    if (flag)
                    {
                        _param = _param + $"{p.Key}={p.Value}";
                        flag = false;
                    }
                    else
                    {
                        _param = _param + $"&{p.Key}={p.Value}";
                    }
                }
                url = url + _param;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = param.Method;



            request.ContentType = param.ContentType;

            try
            {
                var response = (HttpWebResponse)await request.GetResponseAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ExeptionMessage = ExeptionMessage + $"\n\n\n request.URL : {url},JsonContent: {param.JsonContent}  " +
                        $"\n\n\n StatusCode : {response.StatusCode},Server : {response.Server},Method : {response.Method} \n\n\n";
                }
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        Response = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                var stack = (new StackTrace(ex, true)).GetFrame(0);
                ExeptionMessage = ExeptionMessage + $"\n\n\n request.URL : {url},JsonContent: {param.JsonContent}  " +
                    $"\n\n\n Exeption in File : {stack.GetFileName()} ; \n\n\n Line : {stack.GetFileLineNumber()} ; \n\n\n Message : {ex.Message} \n\n\n";
            }
        }
    }
}
