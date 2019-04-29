using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace TestWebAPI.Controllers
{
    public class Article
    {
        public string Title { get; set;  }
        public string Content { get; set; }
    }
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public string Get(int id)
        {
            var newArticles = new List<Article> { new Article { Content = "Content", Title = "Title" } };
            var newArticle = new Article{Content = "Content", Title = "Title" };
            string yourJson = Newtonsoft.Json.JsonConvert.SerializeObject(newArticle);

            //            return Json(newArticles.ToList());
            return "Article " + id.ToString();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
