using ModelValidation.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelValidation.Models
{
    //[NoJoeOnMonday] //模型验证注解属性
    //public class Appointment:IValidatableObject
    //{
    //    //[Required]
    //    public string ClientName { get; set; }

    //    [DataType(DataType.Date)]
    //    //[Required(ErrorMessage ="Please enter a date")]
    //    //[FutureDate(ErrorMessage ="Please enter a date in the future")]//使用重写的内建Requie验证注解属性
    //    public DateTime Date { get; set; }

    //    //[Range(typeof(bool),"true","true",ErrorMessage ="You must accept the terms")]//对于CheckBox的验证，用Range判断只能是True
    //    //[MustBeTrue(ErrorMessage ="You must accept the termssss")]//使用自定义的必须为True验证注解属性
    //    public bool TermsAccepted { get; set; }

    //    //自验证模型的验证方法，模型实现IValidatableObject接口的Validate(ValidationContext validationContext)方法，即可自定义验证
    //    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //    {
    //        List<ValidationResult> errors = new List<ValidationResult>();

    //        if(string.IsNullOrEmpty(ClientName))
    //        {
    //            errors.Add(new ValidationResult("Please enter your name( ⊙ o ⊙ )啊！"));
    //        }

    //        if(DateTime.Now>Date)
    //        {
    //            errors.Add(new ValidationResult("Please enter a date in the future ( ⊙ o ⊙ )啊！"));
    //        }

    //        if(errors.Count==0 && ClientName=="Joe" &&Date.DayOfWeek==DayOfWeek.Monday)
    //        {
    //            errors.Add(new ValidationResult("Joe cannot book appointments on Mondays( ⊙ o ⊙ )啊！"));
    //        }

    //        if(!TermsAccepted)
    //        {
    //            errors.Add(new ValidationResult("You must accept the terms( ⊙ o ⊙ )啊！"));
    //        }

    //        return errors;
    //    }


    public class Appointment 
    {
        [Required]
        [StringLength(10,MinimumLength =3)]
        public string ClientName { get; set; }

        [DataType(DataType.Date)]
        [Remote("ValidateDate","Home")]//远程验证注解。远程验证及用Ajax调用服务端Action进行验证，传输格式需要用Json，控制器和Action即为两个参数
        public DateTime Date { get; set; }


        public bool TermsAccepted { get; set; }

    }
}