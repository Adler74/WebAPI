using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebAPI.Models;
using WebAPIDataAcess;
using System.Xml;
using System.Web;

namespace WebAPI.Controllers
{
    public class URLController : ApiController
    {
        public IEnumerable<URLShort> Get()
        {
            using (TestesEntities entities = new TestesEntities())
            {
                return entities.URLShorts.ToList();
            }
        }

        public URLShort Get(int id)
        {
            using (TestesEntities entities = new TestesEntities())
            {
                var url = entities.URLShorts.FirstOrDefault(x => x.urlid == id);
                if (url == null)
                {
                    return null;
                }
                return entities.URLShorts.FirstOrDefault(x => x.urlid == id);
            }
        }

        public HttpResponseMessage Post([FromBody] URLShort url)
        {
            try
            {
                using (TestesEntities entities = new TestesEntities())
                {
                    //Faz uma solicitação ao bitly
                    WebRequest request = WebRequest.Create("http://api.bitly.com/v3/shorten");
                    XmlDocument xmlDoc = new XmlDocument();
                    //passa os dados do usuário, a chave da API e a url original
                    byte[] data = Encoding.UTF8.GetBytes(string.Format("login={0}&apiKey={1}&longUrl={2}&format={3}",
                    "adler74",                                            
                    "R_7ea37adab2cf402980a6cbd7a5761229",                 
                     System.Web.HttpUtility.UrlEncode(url.url),           
                    "xml"));                                              
                                                                          
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (Stream ds = request.GetRequestStream())
                    {
                        ds.Write(data, 0, data.Length);
                    }
                    //lê o arquivo XML obtido do servidor
                    using (WebResponse response = request.GetResponse())
                    {
                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            xmlDoc.LoadXml(sr.ReadToEnd());
                        }
                    }
                    // Extrai as informações do arquivo XML resposta obtido do servidor
                    string CodigoStatus = xmlDoc.GetElementsByTagName("status_code")[0].InnerText;
                    string TextoStatus = xmlDoc.GetElementsByTagName("status_txt")[0].InnerText;
                    string Data = xmlDoc.GetElementsByTagName("data")[0].InnerText;
                    if (CodigoStatus == "200")
                    {
                        string urlEncurtada = xmlDoc.GetElementsByTagName("url")[0].InnerText;
                        string urlOriginal = xmlDoc.GetElementsByTagName("long_url")[0].InnerText;

                        url.url = urlOriginal;
                        url.urlshort = urlEncurtada;
                        entities.URLShorts.Add(url);
                        entities.SaveChanges();

                        var message = Request.CreateResponse(HttpStatusCode.Created, url);
                        message.Headers.Location = new Uri(Request.RequestUri + url.urlshort.ToString());
                        return message;
                    }
                    else
                    {
                        var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, TextoStatus);
                        return message;
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (TestesEntities entities = new TestesEntities())
                {
                    var entity = entities.URLShorts.FirstOrDefault(x => x.urlid == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "URL com ID = " + entity.urlid + " não encontrada.");
                    }
                    else
                    {
                        entities.URLShorts.Remove(entity);
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
