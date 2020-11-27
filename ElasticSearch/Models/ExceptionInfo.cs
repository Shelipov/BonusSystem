using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Models
{
    public class ExceptionInfo
    {
        public ExceptionInfo(string _ExceptionFile, int _ExceptionFileLine, string _ExceptionMessage)
        {
            ExceptionFile = _ExceptionFile;
            ExceptionFileLine = _ExceptionFileLine;
            ExceptionMessage = _ExceptionMessage;
        }
        public string ExceptionFile { get; set; }
        public int ExceptionFileLine { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
