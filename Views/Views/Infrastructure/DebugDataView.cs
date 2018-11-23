using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Views.Infrastructure
{
    public class DebugDataView : IView
    {
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            Write(writer, "---Routing Data---");
            foreach (var key in viewContext.RouteData.Values.Keys)
            {
                Write(writer, $"Key: {key}, Value: {viewContext.RouteData.Values[key]}");
            }
            Write(writer, "---View Data---");
            foreach (var key in viewContext.ViewData.Keys)
            {
                Write(writer, $"Key: {key}, Value: {viewContext.ViewData[key]}");
            }
        }

        private void Write(TextWriter writer,string template,params object[] values)
        {
            writer.Write(string.Format(template, values) + "<p/>");
        }
    }
}