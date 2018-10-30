using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssentialTools.Models
{
   public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalParam);
    }

    public class DefaultDiscountHelper : IDiscountHelper
    {
        //public decimal DiscountSize { get; set; } //在设定AddBindings时候，属性用WithPropertyValue设定值


        public decimal discountSize;
        public DefaultDiscountHelper(decimal discountSizeParam)
        {
            discountSize = discountSizeParam;
        }

        public decimal ApplyDiscount(decimal totalParam)
        {
           // return (totalParam - (DiscountSize / 100m * totalParam));//用属性计算
           return (totalParam - (discountSize / 100m * totalParam));//用字段计算
        }
    }
}
