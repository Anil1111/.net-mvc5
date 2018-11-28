using HelperMethods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperMethods.Controllers
{
    public class PeopleController : Controller
    {
        private Person[] personData = {
            new Person {FirstName = "Adam", LastName = "Freeman", Role = Role.Admin},
            new Person {FirstName = "Jacqui", LastName = "Griffyth", Role = Role.User},
            new Person {FirstName = "John", LastName = "Smith", Role = Role.User},
            new Person {FirstName = "Anne", LastName = "Jones", Role = Role.Guest}
        };
        // GET: People
        public ActionResult Index()
        {
            return View();
        }

        private IEnumerable<Person> GetData(string selectedRole)
        {
            IEnumerable < Person > data = personData;
            if (selectedRole != "All")
            {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = personData.Where(t => t.Role == selected);
            }
            return data;
        }

        public JsonResult GetPeopleDataJson(string selectedRole = "All")
        {
            //处理集合的结果并转换成Json
            var data = GetData(selectedRole).Select(t => new {
                FirstName = t.FirstName,
                LastName = t.LastName,
                Role = Enum.GetName(typeof(Role), t.Role)
           });
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetPeopleData(string selectedRole = "All")
        {
            IEnumerable<Person> data = personData;
            if (selectedRole != "All")
            {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = personData.Where(t => t.Role == selected);
            }

            //根据请求是否是Ajax类型返回Json或者Html
            if( Request.IsAjaxRequest())
            {
                //只返回FirstName，LastName和Role字段，并且把Role字段关联到内容而不是枚举数字
                var formattedData = data.Select(t => new
                {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Role = Enum.GetName(typeof(Role), t.Role)
                });
                return Json(formattedData, JsonRequestBehavior.AllowGet);
            }

            return PartialView(data);
        }


  
        public ActionResult GetPeople(string selectedRole="All")
        {
            //if(selectedRole==null||selectedRole=="All")
            //{
            //    return View((object)selectedRole);
            //}
            //else
            //{
            //    Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
            //    return View((object)selectedRole);
            //}
            return View((object)selectedRole);
        }
    }
}