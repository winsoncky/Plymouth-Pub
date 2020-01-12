using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plymouth_Pub.Models
{
    public class PartialClass
    {
    }
    public partial class Order
    {
        public string GetUrderName()
        {
            using (Models.UserEntities db = new UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == this.UserId
                              select s.UserName).FirstOrDefault();

                return result;
            }
        }

    }
}