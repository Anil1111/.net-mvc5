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

        public PartialViewResult GetPeopleData(string selectedRole="All")
        {
            IEnumerable<Person> data = personData;
            if(selectedRole!="All")
            {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = personData.Where(t => t.Role == selected);
            }

            return PartialView(data);
        }


  
        public ActionResult GetPeople(string selectedRole="All")
        {
            if(selectedRole==null||selectedRole=="All")
            {
                return View((object)selectedRole);
            }
            else
            {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                return View((object)selectedRole);
            }
        }
    }
}