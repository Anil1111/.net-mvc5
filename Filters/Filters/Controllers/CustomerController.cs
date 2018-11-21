using Filters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Controllers
{
    [SimpleMessage(Message = "A")]
    public class CustomerController : Controller
    {
        //order越小越先执行，如果不写order默认是-1
        //如果order值相同，先执行全局，在执行controller的再执行action的
        //ExceptionFilter的Order相同时执行顺序相反
        //[SimpleMessage(Message ="A",Order =1)]
        //[SimpleMessage(Message = "B", Order = 2)]
        public string Index()
        {
            return "This is the Customer controller";
        }
        
        [SimpleMessage(Message = "B")]
        [CustomOverrideActionFilters]
        public string OtherAction()
        {
            return "This is the other Action in the Customer controller";
        }
    }
}