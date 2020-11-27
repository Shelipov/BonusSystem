using ElasticSearch.Helpers;
using ElasticSearch.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Interfaces
{
    public interface IElasticSearch
    {
        Task Execute(Logger logger);
        Task Execute(TypeEvent _TypeEvent, string _MessageEvent, string _ExeptionFile = null, int? _ExeptionFileLine = null, int? _ClusterID = null, string _UserEmail = null, Guid? _ActionQuery = null, dynamic _InputObject = null);
        Task Execute(Exception ex, int? _ClusterID = null, string _UserEmail = null, dynamic _InputObject = null);
        Task Execute(HttpResponseMessage response);
        Task Execute(HttpResponse response);
    }
}
