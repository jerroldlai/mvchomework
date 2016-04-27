using Money.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace Money.Views.ViewModel
{
    public class MoneyViewModel
    {
        [Display(Name = "類別")]
        public string category { get; set; }
        public string id { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "請輸入正整數")]
        [Display(Name = "金額")]
        public string Money { get; set; }

        [Required]
        [Display(Name = "日期")]
        [QualifiedDate()]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "備註")]
        [StringLength(100, ErrorMessage = "最多輸入100個字")]
        public string Remark { get; set; }
       

    }
}