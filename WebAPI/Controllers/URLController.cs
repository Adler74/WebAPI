using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPIDataAcess;

namespace WebAPI.Controllers
{
    public class URLController : ApiController
    {
        private static List<URL> urls = new List<URL>();

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
                return entities.URLShorts.FirstOrDefault(x => x.urlid == id);
            }
        }


        //public List<URL> Get()
        //{
        //    return urls;
        //}

        public HttpResponseMessage Post([FromBody] URLShort url)
        {
            using (TestesEntities entities = new TestesEntities())
            {
                entities.URLShorts.Add(url);
                entities.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, url);
                message.Headers.Location = new Uri(Request.RequestUri + url.urlid.ToString());
                return message;
            }
        }
            //public void Post(string text)
            //{
            //    if(!String.IsNullOrEmpty(text))
            //        urls.Add(new URL(text));
            //}

        //public void Delete(string text)
        //{
        //    urls.RemoveAt(urls.IndexOf(urls.First(x => x.url.Equals(text))));
        //}

        public HttpResponseMessage Delete(int id)
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
    }
}
