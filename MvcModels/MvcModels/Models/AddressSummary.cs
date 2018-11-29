using MvcModels.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Models
{
    //将符合类型的对象属性绑定到一个单独的对象
    //[Bind(Prefix ="HomeAddress")标明绑定的字段有HomeAddress前缀
    //Exclude是设置模型绑定器不要绑定Country属性
    //[Bind(Prefix = "HomeAddress", Exclude = "Country")]
    [ModelBinder(typeof(AddressSummaryBinder))]//可以在模型上用注解直接调用自定义模型绑定器
    public class AddressSummary
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}