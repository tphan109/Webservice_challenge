using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Webservice_challenge
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuration et services API Web

            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "api/{controller}/{n}",
               defaults: new { id = RouteParameter.Optional }

          );

          
          config.Formatters.XmlFormatter.UseXmlSerializer = true;
            // log des requetes et des réponses            
            config.MessageHandlers.Add(new CustomLogHandler());
        }
    }

    public class LogMetadata
    {
        public string RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public string RequestContentType { get; set; }
        public string RequestUri { get; set; }
        public string RequestMethod { get; set; }
        public DateTime? RequestTimestamp { get; set; }
        public string ResponseContentType { get; set; }

        public string ResponseContent { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public DateTime? ResponseTimestamp { get; set; }

        public string referenceTransaction { get; set; }
    }

    public class CustomLogHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LogMetadata logMetadata;

            HttpResponseMessage response = null;
            try
            {

                logMetadata = BuildRequestMetadata(request);
                logMetadata.RequestBody = await request.Content.ReadAsStringAsync();

                response = await base.SendAsync(request, cancellationToken);

                if (response.Content != null)
                {
                    // récupération de la réponse
                    logMetadata.ResponseContent = await response.Content.ReadAsStringAsync();
                }

                logMetadata = BuildResponseMetadata(logMetadata, response);


                await SendToLog(logMetadata);

                return response;
            }
            catch
            {
                return response;
            }

            finally
            {

            }

        }

        private LogMetadata BuildRequestMetadata(HttpRequestMessage request)
        {

            LogMetadata log = new LogMetadata
            {
                RequestHeaders = request.Headers.ToString(),
                RequestMethod = request.Method.Method,
                RequestTimestamp = DateTime.Now,
                RequestUri = request.RequestUri.ToString(),
            };
            return log;
        }
        private LogMetadata BuildResponseMetadata(LogMetadata logMetadata, HttpResponseMessage response)
        {
            logMetadata.ResponseStatusCode = response.StatusCode;
            logMetadata.ResponseTimestamp = DateTime.Now;
            logMetadata.ResponseContentType = response.Content.Headers.ContentType.MediaType;
            return logMetadata;
        }
        private async Task<bool> SendToLog(LogMetadata logMetadata)
        {
            // TODO: Write code here to store the logMetadata instance to a pre-configured log store...
            //uniquement les erreurs log fichier
            ILog logger = log4net.LogManager.GetLogger("ErrorLog");
            logger.Info("appel au webservice: ");
            logger.Info(logMetadata.RequestUri + "\n"+ logMetadata.RequestBody);

            logger.Info("réponse de webservice: ");
            logger.Info(logMetadata.ResponseContent);

            return true;
        }


        private static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
