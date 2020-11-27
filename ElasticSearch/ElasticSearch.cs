using BonusSystem.Configuration;
using Elasticsearch.Net;
using ElasticSearch.Helpers;
using ElasticSearch.Interfaces;
using ElasticSearch.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch
{
    public class ElasticSearch : IElasticSearch
    {
        readonly Configuration configuration;
        public ElasticSearch(Configuration _configuration)
        {
            configuration = _configuration;
            _Index = Index.Core.GetValueAttribute();
            if (configuration != null)
            {
                if (configuration.ElasticSearchUrl != null)
                {
                    ElasticDomain = configuration.ElasticSearchUrl;
                    ElasticLogin = configuration.ElasticSearchLogin;
                    ElasticPassword = configuration.ElasticSearchPassword;
                    Node = new Uri(ElasticDomain);
                    Config = new ConnectionConfiguration(Node);
                    Config.DisableDirectStreaming(true);
                    Envirment = configuration.ElasticSearchEnvirment;
                    Domain = LocalIPAddress().ToString();
                    Check = bool.Parse(configuration.ElasticSearchCheck);
                    if (ElasticLogin != null && ElasticLogin != "" && ElasticPassword != null && ElasticPassword != "")
                    {
                        Config.BasicAuthentication(ElasticLogin, ElasticPassword);
                    }
                    Client = new ElasticLowLevelClient(Config);
                }
            }
        }
        string _Index { get; set; }
        string ElasticDomain { get; set; }
        string Domain { get; set; }
        string ElasticLogin { get; set; }
        string ElasticPassword { get; set; }
        string Envirment { get; set; }
        Uri Node { get; set; }
        ConnectionConfiguration Config { get; set; }
        public ElasticLowLevelClient Client { get; private set; }
        bool Check { get; set; } = false;

        public async Task Execute(Logger logger)
        {
            logger.Envirment = Envirment; logger.Domain = Domain;
            var index = _Index + "-" + $"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}";
            var response = await Client.IndexAsync<BytesResponse>(index, GenerateID(), PostData.Serializable(logger));
        }

        public async Task Execute(TypeEvent _TypeEvent, string _MessageEvent,
            string _ExeptionFile = null, int? _ExeptionFileLine = null, int? _ClusterID = null, string _UserEmail = null, Guid? _ActionQuery = null, dynamic _InputObject = null)
        {
            if (Check)
            {
                var logger = new Logger(_TypeEvent.GetValueAttribute(), _MessageEvent, _InputObject, _ExeptionFile, _ExeptionFileLine, _UserEmail, _ActionQuery);
                logger.Envirment = Envirment; logger.Domain = Domain;

                var index = _Index + "-" + $"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}";
                var response = await Client.IndexAsync<BytesResponse>(index, GenerateID(), PostData.Serializable(logger));
            }
        }

        public async Task Execute(Exception ex, int? _ClusterID = null, string _UserEmail = null, dynamic _InputObject = null)
        {
            if (Check)
            {
                var stack = (new StackTrace(ex, true)).GetFrame(0);
                var logger = new Logger("Error", ex.Message, _InputObject, stack.GetFileName(), stack.GetFileLineNumber(), _UserEmail);
                logger.Envirment = Envirment; logger.Domain = Domain;

                var index = _Index + "-" + $"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}";
                var response = await Client.IndexAsync<BytesResponse>(index, GenerateID(), PostData.Serializable(logger));
            }
        }

        public async Task Execute(HttpResponseMessage response)
        {
            if (Check)
            {
                var index = _Index + "-" + $"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}";

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var _Exception = new ExceptionInfo(response.RequestMessage.RequestUri.AbsolutePath, response.StatusCode.GetHashCode(), response.ReasonPhrase);
                    var logger = new Logger(TypeEvent.Warning.GetValueAttribute(), response.RequestMessage.Method.Method, response, response.RequestMessage.RequestUri.ToString(), response.StatusCode.GetHashCode());
                    logger.Exception = _Exception;
                    logger.Envirment = Envirment; logger.Domain = Domain;
                    await Client.IndexAsync<BytesResponse>(index, GenerateID(), PostData.Serializable(logger));
                }
                else
                {
                    var logger = new Logger(TypeEvent.Information.GetValueAttribute(), response.RequestMessage.Method.Method, response, response.RequestMessage.RequestUri.ToString(), response.StatusCode.GetHashCode());
                    logger.Envirment = logger.Envirment = Envirment; logger.Domain = Domain;
                    await Client.IndexAsync<BytesResponse>(index, GenerateID(), PostData.Serializable(logger));
                }
            }
        }

        public async Task Execute(HttpResponse response)
        {
            if (Check)
            {
                var index = _Index + "-" + $"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}";

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var _Exception = new ExceptionInfo(response.Content.GetValueAttribute(), response.StatusCode.GetHashCode(), response.ResponseUri.ToString());
                    var logger = new Logger(TypeEvent.Warning.GetValueAttribute(), response.Content.GetValueAttribute(), response, response.Server, response.StatusCode.GetHashCode());
                    logger.Exception = _Exception;
                    logger.Envirment = logger.Envirment = Envirment; logger.Domain = Domain;
                    await Client.IndexAsync<BytesResponse>(index, GenerateID(), PostData.Serializable(logger));
                }
                else
                {
                    var logger = new Logger(TypeEvent.Information.GetValueAttribute(), response.Content.GetValueAttribute(), response, response.Server, response.StatusCode.GetHashCode());
                    logger.Envirment = logger.Envirment = Envirment; logger.Domain = Domain;
                    await Client.IndexAsync<BytesResponse>(index, GenerateID(), PostData.Serializable(logger));
                }
            }
        }

        string GenerateID()
        {
            return Guid.NewGuid().ToString();
        }

        IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        public void ChangeIndex(Index index)
        {
            _Index = index.ToString();
        }
    }
}
