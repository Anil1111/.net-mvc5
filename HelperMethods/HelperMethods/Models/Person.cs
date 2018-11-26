using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperMethods.Models
{
    //[DisplayName("New Person")]
    [MetadataType(typeof(PersonMetadata))]//定义伙伴类
    public partial class Person
    {
        //[HiddenInput(DisplayValue = false)]//用支架时渲染成一个隐藏的input, DisplayValue决定是否显示这个属性
        //[ScaffoldColumn(false)]//是否使用基架
        public int PersonId { get; set; }

        //[Display(Name ="First")] //修改LabelFor生成的显示名称
        //[UIHint("MultilineText")]//指定该字段渲染的模板
        public string FirstName { get; set; }

        //[Display(Name = "Last")]
        public string LastName { get; set; }

        //[Display(Name = "Birth Date")]
        //[DataType(DataType.Date)]//渲染这个字段成某个类型的input
        public DateTime BirthDate { get; set; }


        public Address HomeAddress { get; set; }

        //[Display(Name = "Approved")]
        public bool IsApproved { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        Admin,
        User,
        Guest
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string  City {get;set;}
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}