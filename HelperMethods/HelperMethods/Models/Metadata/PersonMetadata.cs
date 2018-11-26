using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperMethods.Models
{
    //Person的伙伴类，如果Person类是自定生成的，可以在这里定义注解。伙伴类必须和原类在同一命名空间
    [DisplayName("New Person")]
    public partial class PersonMetadata
    {
        [HiddenInput(DisplayValue = false)]//用支架时渲染成一个隐藏的input, DisplayValue决定是否显示这个属性
        //[ScaffoldColumn(false)]//是否使用基架
        public int PersonId { get; set; }

        [Display(Name = "First")] //修改LabelFor生成的显示名称
        //[UIHint("MultilineText")]//指定该字段渲染的模板
        public string FirstName { get; set; }

        [Display(Name = "Last")]
        public string LastName { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]//渲染这个字段成某个类型的input
        public DateTime BirthDate { get; set; }


        public Address HomeAddress { get; set; }

        [Display(Name = "Approved")]
        public bool IsApproved { get; set; }

        [UIHint("Enum")] //利用公用枚举模板
        public Role Role { get; set; }
    }
}