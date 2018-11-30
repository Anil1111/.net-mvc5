using System;
using System.Web.Mvc;

namespace MvcModels.Infrastructure
{
    //自定义值提供器工厂
    public class CustomValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return new CountryValueProvider();
        }
    }
}