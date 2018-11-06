using SportsStore.Domain.Entities;
using System.Web.Mvc;

namespace SportsStore.WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sesstionKey = "Cart";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            
            Cart cart = null;
            if(controllerContext.HttpContext.Session!=null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sesstionKey];
            }

            //若Sesstion中没有Cart,则创建一个
            if(cart==null)
            {
                cart = new Cart();
                if(controllerContext.HttpContext.Session!=null)
                {
                    controllerContext.HttpContext.Session[sesstionKey] = cart;
                }
            }

            return cart;
        }
    }
}