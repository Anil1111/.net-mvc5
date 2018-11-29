using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Controllers
{
    public class HomeController : Controller
    {
        private Person[] personData = {
            new Person {PersonId=1, FirstName = "Adam", LastName = "Freeman", Role = Role.Admin},
            new Person {PersonId=2,FirstName = "Jacqui", LastName = "Griffyth", Role = Role.User},
            new Person {PersonId=3,FirstName = "John", LastName = "Smith", Role = Role.User},
            new Person {PersonId=4,FirstName = "Anne", LastName = "Jones", Role = Role.Guest}
        };
        // GET: Home
        public ActionResult Index(int id=1)
        {
            Person dataItem = personData.Where(t => t.PersonId == id).First();
            return View(dataItem);
        }

        public ActionResult CreatePerson()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson(Person model)
        {
            return View("Index", model);
        }

        
        //将符合类型的对象属性绑定到一个单独的对象
        //[Bind(Prefix ="HomeAddress")标明绑定的字段有HomeAddress前缀
        //Exclude是设置模型绑定器不要绑定Country属性
        //可以直接设置在模型上
        public ActionResult DisplaySummary([Bind(Prefix ="HomeAddress", Exclude ="Country")]AddressSummary summary)
        {
            return View(summary);
        }

        //绑定到集合和字符串数组
        public ActionResult Names(IList<string> names)
        {
            names = names ?? new List<string>();
            return View(names);
        }

        //绑定自定义模型类型集合
        //public ActionResult Address(IList<AddressSummary> addresses)
        //{
        //    addresses=addresses?? new List<AddressSummary>();
        //    return View(addresses);
        //}


        //手工调用绑定过程
        public ActionResult Address(FormCollection formData)//FormCollection实现了IValueProvider接口
        {
            IList<AddressSummary> addresses = new List<AddressSummary>();
            UpdateModel(addresses);
            //try
            //{
            //    UpdateModel(addresses, formData);//设定绑定模型的唯一数据源为表单
            //}
            //catch(InvalidOperationException ex)
            //{
            //    //给用户提供反馈
            //}

            //或者用TryUpdateModel，成功返回True，不成功返回False
            //if(TryUpdateModel(addresses, formData))
            //{
            //    //正常处理
            //}
            //else
            //{
            //    //给用户反馈
            //}
            return View(addresses);
        }
    }
}