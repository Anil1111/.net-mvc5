using EssentialTools.Models;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EssentialTools.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();//不能丢
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IValueCalculator>().To<LinqValueCaculator>().InRequestScope();
            //kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>()
            //                              .WithPropertyValue("DiscountSize", 50M);//属性用WithPropertyValue设定值
            kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>()
                                          .WithConstructorArgument("discountSizeParam", 30m);//构造函数参数用WithConstructorArgument设定值
            kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>()
                                          .WhenInjectedInto<LinqValueCaculator>();//条件绑定
        }
    }
}