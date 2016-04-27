using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Money.Models
{
    public class QualifiedDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime Money_Date = (DateTime)value;
            if (Money_Date.CompareTo(DateTime.Now)>=0)
            {
                // valid
                return ValidationResult.Success;
            }
            else
            {
                // invalid
                var errorMsg = string.Format(validationContext.DisplayName+"必須大於今日");
                return new ValidationResult(errorMsg);
            }
        }

    }
}