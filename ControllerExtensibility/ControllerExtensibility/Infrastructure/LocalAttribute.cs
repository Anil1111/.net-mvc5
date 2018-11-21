using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ControllerExtensibility.Infrastructure
{
    //使用内建动作调用器，可以自定义动作方法选择器
    public class LocalAttribute : ActionMethodSelectorAttribute
    {
        //调用器根据名称丢掉一些方法
        //然后调用器丢弃选择器注解属性对当前请求返回false的动作方法
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.IsLocal;
        }
    }
}