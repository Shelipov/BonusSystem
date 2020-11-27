using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Models
{
    public class Logger
    {

        public Logger(string _TypeEvent, string _MessageEvent, dynamic _InputObject = null,
            string _ExeptionFile = null, int? _ExeptionFileLine = null,
            string _UserEmail = null,
            Guid? _ActionQuery = null)
        {
            Program = AppDomain.CurrentDomain.BaseDirectory;
            TypeEvent = _TypeEvent;
            DateEvent = DateTime.Now;
            MessageEvent = _MessageEvent;
            UserEmail = _UserEmail;
            ActionQuery = _ActionQuery;
            InputObject = _InputObject;
            if (_ExeptionFile != null && _ExeptionFileLine != null)
            {
                Exception = new ExceptionInfo(_ExeptionFile, (int)_ExeptionFileLine, _MessageEvent);
            }

        }

        public string UserEmail { get; set; }
        public Guid? ActionQuery { get; set; }
        public dynamic InputObject { get; set; }
        public string Envirment { get; set; }
        public string Program { get; set; }
        public string TypeEvent { get; set; }
        public DateTime DateEvent { get; set; }
        public string MessageEvent { get; set; }
        public ExceptionInfo Exception { get; set; }
        public string Domain { get; set; }
    }
}
