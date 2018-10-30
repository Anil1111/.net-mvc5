using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace EssentialTools.Models
{
    public class LinqValueCaculator: IValueCalculator
    {
        private IDiscountHelper discounter;
        private static int counter = 0;
        public LinqValueCaculator(IDiscountHelper discountParam)
        {
            discounter = discountParam;
            Debug.WriteLine($"Instance {++counter} created");
        }
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            // return products.Sum(t => t.Price);
            return discounter.ApplyDiscount(products.Sum(t => t.Price));
        }
    }
}