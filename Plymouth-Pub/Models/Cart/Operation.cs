using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Plymouth_Pub.Models.Cart
{
    public static class Operation
    {
        [WebMethod(EnableSession = true)] 
        public static Models.Cart.Cart GetCurrentCart() 
        {
            if (System.Web.HttpContext.Current != null) 
            {
                if (System.Web.HttpContext.Current.Session["Cart"] == null)
                {
                    var order = new Cart();
                    System.Web.HttpContext.Current.Session["Cart"] = order;
                }

                return (Cart)System.Web.HttpContext.Current.Session["Cart"];
            }
            else
            {
                throw new InvalidOperationException("System.Web.HttpContext.Current為空，請檢查");
            }
        }
    }
}