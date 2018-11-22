using ControllerExtensibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers
{
    public class RemoteDataController : Controller
    {
        // GET: RemoteData
        public async Task<ActionResult> Data()
        {
            //在客户端用awai异步调用普通方法
            RemoteService service = new RemoteService();
            string data = await Task<string>.Factory.StartNew(() =>
            {
                return new RemoteService().GetRemoteData();
            });
            return View((object)data);
        }

        //在客户端用await异步调用async方法
        public async Task<ActionResult> ConsumeAsyncMethod()
        {
            string data = await new RemoteService().GetRemoteDataAsync();
            return View("Data", (object)data);
        }
    }
}