using ModelValidation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ModelValidation.Infrastructure
{
    public class NoJoeOnMondayAttribute : ValidationAttribute
    {
        public NoJoeOnMondayAttribute()
        {
            ErrorMessage = "Joe cannot book appointments on Monday";
        }

        public override bool IsValid(object value)
        {
            Appointment app = value as Appointment;
            if(app==null || string.IsNullOrEmpty(app.ClientName) || app.Date==null)
            {
                //还没有正确的模型要验证或者还没有所需要的ClientName和Date的属性值
                return true;
            }
            else
            {
                return !(app.ClientName == "Joe" && app.Date.DayOfWeek == DayOfWeek.Monday);
            }
            
        }
    }
}