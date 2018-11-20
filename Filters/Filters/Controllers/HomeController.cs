using Filters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Filters.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Users="admin")]
        public string Index()
        {
            return "This is the Index action on the Home controller";
        }

        [GoogleAuth]
        [Authorize(Users ="bob@google.com")]
        public string List()
        {
            return "This is the list action on the Home controller";
        }

        //[RangeException]
        [HandleError(ExceptionType =typeof(ArgumentOutOfRangeException),View ="RangeError")]
        public string RanngeTest(int id)
        {
            if(id>100)
            {
                return $"The id value is: {id}";
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", id, "");
            }
        }

        //[CustomAction]
        [ProfileAction]
        public string FilterTest()
        {
            Thread.Sleep(2000);
            return "This is the FilterTest action";
        }
    }
}