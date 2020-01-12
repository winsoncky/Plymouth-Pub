using Plymouth_Pub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Plymouth_Pub.Controllers
{
    public class ValuesController : ApiController
    {
        public String Get()
        {
            return "Value1";
        }

        public string Get(int id)
        {
            return "value";
        }

        public void Post([FromBody]string value)
        {
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }

    }
}
