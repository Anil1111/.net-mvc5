using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Infrastructure
{
    //自定义值提供器
    public class CountryValueProvider : IValueProvider
    {
        //该值提供器只请求Country属性的值进行响应，而且总是返回USA
        public bool ContainsPrefix(string prefix)
        {
            return prefix.ToLower().IndexOf("country") > -1;
        }

        public ValueProviderResult GetValue(string key)
        {
            if (ContainsPrefix(key))
            {
                return new ValueProviderResult("USA", "USA", CultureInfo.InvariantCulture);
            }
            else
            {
                return null;
            }
        }
    }
}