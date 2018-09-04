using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace Webservice_challenge.Controllers
{
    public class XmlToJSonController : ApiController
    {
        [HttpPost, Route("api/xmltojson")]
        public string XmlToJson([FromBody] string xml)
        {
            string jsonText = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(xml))
                {

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);
                    jsonText = JsonConvert.SerializeXmlNode(xmlDoc);
                }
                else
                {
                    jsonText = "Bad Xml format";
                }
            }
            catch (System.Xml.XmlException)
            {
                jsonText = "Bad Xml format";
            }

            return jsonText;
        }

        //[HttpPost, Route("api/xmltojson")]
        //public string XmlToJson(HttpRequestMessage request)
        //{
        //    var doc = new XmlDocument();
        //    doc.Load(request.Content.ReadAsStreamAsync().Result);
        //    //return doc.DocumentElement.InnerText.ToString();

        //    string jsonText = string.Empty;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(doc.InnerXml.ToString()))
        //        {

        //            XmlDocument xmlDoc = new XmlDocument();
        //            xmlDoc.LoadXml(doc.InnerXml.ToString());
        //            jsonText = JsonConvert.SerializeXmlNode(xmlDoc);
        //        }
        //        else
        //        {
        //            jsonText = "Bad Xml format";
        //        }
        //    }
        //    catch (System.Xml.XmlException)
        //    {
        //        jsonText = "Bad Xml format";
        //    }

        //    return jsonText;
        //}

    }
}
