using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperMethods.Infrastructure
{
    //自定义外部辅助器方法
    public static class CustomHelpers
    {
        public static MvcHtmlString ListArrayItems(this HtmlHelper html, string[] list)
        {
            TagBuilder tag = new TagBuilder("ul");
            foreach (var item in list)
            {
                TagBuilder itemTag = new TagBuilder("li");
                itemTag.SetInnerText(item);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString DisplayMessage(this HtmlHelper html, string msg)
        {
            string encodeMessage = html.Encode(msg);//对显示的内容进行编码
            string result = $"This is the message: <p>{encodeMessage}</p>";
            return new MvcHtmlString(result);
            //return result;//只返回编码的string时，直接用string类型

        }
    }
}