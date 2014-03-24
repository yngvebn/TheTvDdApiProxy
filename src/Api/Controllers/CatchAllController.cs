using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CatchAllController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Index()
        {
            return GetTvDbApiResponse(Request.RequestUri.PathAndQuery);
        }
        
        private IHttpActionResult GetTvDbApiResponse(string relativeUrl, params object[] args)
        {
            string url = string.Format(relativeUrl, args);
            if (url[0] != '/') url = '/' + url;
            WebClient client = new WebClient();
            var xmlResponse = client.DownloadString("http://thetvdb.com" + url);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlResponse);
            string json = JsonConvert.SerializeXmlNode(doc);
            return Ok(JsonConvert.DeserializeObject<dynamic>(json));

        }
    }
}
