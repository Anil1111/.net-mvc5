using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ModelValidation.Infrastructure
{
    // 重写一个内建Require验证的注解属性
    public class FutureDateAttribute: RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            return base.IsValid(value) &&((DateTime)value) > DateTime.Now;
        }
    }
}